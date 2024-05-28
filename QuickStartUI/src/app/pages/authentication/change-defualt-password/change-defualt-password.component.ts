import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DefaultPasswordDto } from 'src/app/_interface/user';
import { PasswordConfirmationValidatorService } from 'src/app/core/password-confirmation-validator.service';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { DialogService } from 'src/app/shared/services/dialog.service';

@Component({
  selector: 'app-change-defualt-password',
  templateUrl: './change-defualt-password.component.html',
})
export class ChangeDefualtPasswordComponent implements OnInit {
  passwordForm: FormGroup | any;
  hidePassword = true;

  constructor(
    private authService: AuthenticationService,
    private passConfValidator: PasswordConfirmationValidatorService,
    private route: ActivatedRoute,
    private router: Router,
    private dialogserve: DialogService,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.passwordForm = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('', [Validators.required]),
    });

    this.passwordForm
      .get('confirm')
      .setValidators([
        Validators.required,
        this.passConfValidator.validateConfirmPassword(
          this.passwordForm.get('password')
        ),
      ]);
  }

  public validateControl = (controlName: string) => {
    return (
      this.passwordForm.get(controlName).invalid &&
      this.passwordForm.get(controlName).touched
    );
  };
  public hasError = (controlName: string, errorName: string) => {
    return this.passwordForm.get(controlName).hasError(errorName);
  };
  public resetPassword = (passwordFormValue: any) => {
    const change = { ...passwordFormValue };

    const data: DefaultPasswordDto = {
      password: change.password,
      confirmPassword: change.confirm,
    };
    let user: string = this.activeRoute.snapshot.params['user'];
    const apiUri: string = `api/authentication/${user}/ChangeDefulatPassword`;
    this.authService.changeDefaultPassword(apiUri, data).subscribe({
      next: (_) => {
        this.dialogserve
          .openSuccessDialog('Password changed successfully, please login.')
          .afterClosed()
          .subscribe((res) => {
            this.logout();
          });
      },
      error: (err: HttpErrorResponse) => {
        console.log(err.message);
        this.dialogserve
          .openErrorDialog(err.message)
          .afterClosed()
          .subscribe((res) => {});
      },
    });
  };

  public logout = () => {
    this.authService.logout();
    this.router.navigate(['/authentication/login']);
  };

  togglePasswordVisibility() {
    this.hidePassword = !this.hidePassword;
  }
}
