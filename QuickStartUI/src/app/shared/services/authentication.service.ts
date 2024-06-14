import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import {
  UserForRegistrationDto,
  RegistrationResponseDto,
  UserForAuthenticationDto, TokenDto,
  ChangePasswordDto, DefaultPasswordDto,
  ForgetPasswordResponseDto,
  ForgotPasswordDto,
  ResetPasswordDto
} from 'src/app/_interface/user';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  private _updateuser = new Subject<void>();

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  get updateuser() {
    return this._updateuser;
  }
  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public registerUser = (route: string, body: UserForRegistrationDto) => {
    return this.http.post<RegistrationResponseDto>(this.createCompleteRoute(route, environment.apiUrl), body);
  }

  public loginUser = (route: string, body: UserForAuthenticationDto) => {
    return this.http.post<TokenDto>(this.createCompleteRoute(route, environment.apiUrl), body);
  }

  public isUserAuthenticated = (): boolean | any => {
    const token = localStorage.getItem("token");
    return token;
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public changePassword = (route: string, body: ChangePasswordDto) => {
    return this.http.post(this.createCompleteRoute(route, environment.apiUrl), body);
  }

  public changeDefaultPassword = (route: string, body: DefaultPasswordDto) => {
    return this.http.post(this.createCompleteRoute(route, environment.apiUrl), body);
  }

  public forgotPassword = (route: string, body: ForgotPasswordDto) => {
    return this.http.post<ForgetPasswordResponseDto>(this.createCompleteRoute(route, environment.apiUrl), body);
  }
  public resetPassword = (route: string, body: ResetPasswordDto) => {
    return this.http.post(this.createCompleteRoute(route, environment.apiUrl), body);
  }


  public loadCurrentUserEmail() {
    const token: any = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token);
    const name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    return name;
  }
  public loadCurrentUserName() {
    const token: any = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token);
    const name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    return name;
  }

  public loadCurrentUserRole() {
    const token: any = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token);
    const name = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return name;
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
