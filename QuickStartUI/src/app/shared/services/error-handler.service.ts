import { HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpInterceptor } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {
  
  constructor(private router: Router) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = this.handleError(error);
        return throwError(() => new Error(errorMessage));
      })
    )
  }

  private handleUnauthorized = (error: HttpErrorResponse) => {
    if(this.router.url.startsWith('/authentication/login')) {
      return error.error.errorMessage;
    }
    else {
      this.router.navigate(['/authentication/login']);
      return error.message;
    }
  }

  private handleError = (error: HttpErrorResponse) : string|any => {
    if(error.status === 404){
      return this.handleNotFound(error);
    }
    else if(error.status === 400){
      return this.handleBadRequest(error);
    }
    else if(error.status === 401) {
      return this.handleUnauthorized(error);
    }  
  }
  private handleNotFound = (error: HttpErrorResponse): string => {
    this.router.navigate(['/404']);
    return error.message;
  }
  private handleBadRequest = (error: HttpErrorResponse): string => {
    if (error && error.error) {
      if (error.error.errors) {
        const values = Object.values(error.error.errors);
        let message = '';
        values.forEach((m: string | any) => {
          message += m + ' ';
        });
        return message.slice(0, -1); 
      } else if (error.error.Message) {
        return error.error.Message;
      }
    }
    return error.message || 'An unknown error occurred';
  };


}