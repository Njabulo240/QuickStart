import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  ForgetPasswordResponseDto,
  ForgotPasswordDto,
} from 'src/app/_interface/user';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { environment } from 'src/environments/environment';



@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  successMessage: string;

  constructor(
    private _authService: AuthenticationService,
    private dialogserve: DialogService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.forgotPasswordForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }

  get email() {
    return this.forgotPasswordForm.get('email');
  }
  public validateControl = (controlName: string) => {
    return (
      this.forgotPasswordForm.get(controlName)?.invalid &&
      this.forgotPasswordForm.get(controlName)?.touched
    );
  };
  public hasError = (controlName: string, errorName: string) => {
    return this.forgotPasswordForm.get(controlName)?.hasError(errorName);
  };
  public forgotPassword = (forgotPasswordFormValue: any) => {
    const forgotPass = { ...forgotPasswordFormValue };
    const forgotPassDto: ForgotPasswordDto = {
      email: forgotPass.email,
      clientURI: environment.clientUrl+'/authentication/reset-password',
    };
    this._authService
      .forgotPassword('api/authentication/ForgotPassword', forgotPassDto)
      .subscribe({
        next: (_) => {
          this.dialogserve
            .openSuccessDialog(
              'The link has been sent, please check your email to reset your password.'
            )
            .afterClosed()
            .subscribe((res) => {
              this.router.navigate(['authentication/login']);
            });
        },
        error: (err) => {
          this.dialogserve
            .openErrorDialog(err.message)
            .afterClosed()
            .subscribe((res) => {
              this.router.navigate(['authentication/forgot-password']);
            });
        },
      });
  };
}
