import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordDto } from 'src/app/_interface/user';
import { PasswordConfirmationValidatorService } from 'src/app/core/password-confirmation-validator.service';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { DialogService } from 'src/app/shared/services/dialog.service';


@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup | any;
  private token: string;
  private email: string;
  hidePassword = true;

  constructor(
    private authService: AuthenticationService,
    private passConfValidator: PasswordConfirmationValidatorService,
    private route: ActivatedRoute,
    private router: Router,
    private dialogserve: DialogService
  ) { }

  ngOnInit(): void {
    this.resetPasswordForm = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl(''),
    });

    this.resetPasswordForm
      .get('confirm')
      .setValidators([
        Validators.required,
        this.passConfValidator.validateConfirmPassword(
          this.resetPasswordForm.get('password')
        ),
      ]);

    this.token = this.route.snapshot.queryParams['token'];
    this.email = this.route.snapshot.queryParams['email'];
  }

  togglePasswordVisibility() {
    this.hidePassword = !this.hidePassword;
  }

  public validateControl = (controlName: string) => {
    return (
      this.resetPasswordForm.get(controlName).invalid &&
      this.resetPasswordForm.get(controlName).touched
    );
  };
  public hasError = (controlName: string, errorName: string) => {
    return this.resetPasswordForm.get(controlName).hasError(errorName);
  };
  public resetPassword = (resetPasswordFormValue: any) => {
    const resetPass = { ...resetPasswordFormValue };
    const resetPassDto: ResetPasswordDto = {
      password: resetPass.password,
      confirmPassword: resetPass.confirm,
      token: this.token,
      email: this.email,
    };
    this.authService
      .resetPassword('api/authentication/resetpassword', resetPassDto)
      .subscribe({
        next: (_) => {
          this.dialogserve
            .openSuccessDialog('Password changed successfully, please login.')
            .afterClosed()
            .subscribe((res) => {
              this.router.navigate(['authentication/login']);
            });
        },
        error: (err: HttpErrorResponse) => {
          this.dialogserve
            .openErrorDialog(err.message)
            .afterClosed()
            .subscribe((res) => {
              this.router.navigate(['authentication/resetpassword']);
            });
        },
      });
  };
}
