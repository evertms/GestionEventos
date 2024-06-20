import { AbstractControl, FormControl, ValidationErrors, ValidatorFn } from '@angular/forms';

/*export function MustMatch(password: FormControl, confirmPassword: FormControl) {
    if (confirmPassword.errors && !confirmPassword.errors.mustMatch) {
      // Si hay otros errores, no sobrescribimos el error de mustMatch
      return;
    }

    // Establecer el error en matchingControl si la validación falla
    if (password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ mustMatch: true });
    } else {
      confirmPassword.setErrors(null);
    }
}*/

// Validador personalizado para comparar dos campos
export function mustMatch(matchTo: AbstractControl): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    return control.value === matchTo.value ? null : { mustMatch: true };
  };
}

export function bolivianPhoneValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const valid = /^[0-9]{8}$/.test(control.value);
    return valid === true ? null : { bolivianPhoneValidator: true };
  };
}

export function ageValidator(minAge: number) {
  return (control: AbstractControl) => {
    const birthDate = new Date(control.value);
    const today = new Date();
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDifference = today.getMonth() - birthDate.getMonth();

    if (
      monthDifference < 0 ||
      (monthDifference === 0 && today.getDate() < birthDate.getDate())
    ) {
      age--;
    }

    return age >= minAge ? null : { ageValidator: true };
  };
}