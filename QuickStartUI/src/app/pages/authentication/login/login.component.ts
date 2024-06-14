import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { UserForAuthenticationDto, TokenDto } from 'src/app/_interface/user';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ChangeDefualtPasswordComponent } from '../change-defualt-password/change-defualt-password.component';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class AppSideLoginComponent {
  private returnUrl?: string;
  loginForm?: FormGroup | any;
  hidePassword = true;

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute,
    private dialogserve: DialogService,
    private dialog: MatDialog,
  ) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
  }

  togglePasswordVisibility() {
    this.hidePassword = !this.hidePassword;
  }
  validateControl = (controlName: string) => {
    return (
      this.loginForm?.get(controlName)?.invalid &&
      this.loginForm.get(controlName)?.touched
    );
  };
  hasError = (controlName: string, errorName: string) => {
    return this.loginForm?.get(controlName)?.hasError(errorName);
  };

  loginUser = (loginFormValue: any) => {
    const defaultPassword = 'Password.321';
    const login = { ...loginFormValue };
    const userForAuth: UserForAuthenticationDto = {
      userName: login.username,
      password: login.password,
      clientURI: environment.clientUrl+'/authentication/forgot-password'
    };
    this.authService
      .loginUser('api/authentication/login', userForAuth)
      .subscribe({
        next: (res: TokenDto) => {
          if (login.password === defaultPassword) {
            this.authService.logout();
            let url: string = `/authentication/change-password/${login.username}`;
            this.router.navigate([url]);
          } else {
            localStorage.setItem("token", res.token);
            this.authService.sendAuthStateChangeNotification(
              res.isAuthSuccessful
            );
            this.authService.updateuser.next();
            this.router.navigate(['/']);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.dialogserve
            .openErrorDialog(err.message)
            .afterClosed()
            .subscribe((res) => {
              this.router.navigate(['authentication/login']);
            });
        },
      });
  };

  Update(user: string) {
    const popup = this.dialog.open(ChangeDefualtPasswordComponent, {
      width: '400px',
      height: '440px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
      data: {
        user: user,
      },
    });
  }

}
