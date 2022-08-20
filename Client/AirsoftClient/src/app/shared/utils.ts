import { AbstractControl, ValidatorFn } from "@angular/forms";

export function passwordMatch(passwordFormControl: AbstractControl) {
    const validatorFn: ValidatorFn = (repeatPasswordFormControl: AbstractControl) => {
        if (passwordFormControl.value !== repeatPasswordFormControl.value) {
            return {
                passwordMissmatch: true
            }
        }

        return null;
    }

    return validatorFn;
}