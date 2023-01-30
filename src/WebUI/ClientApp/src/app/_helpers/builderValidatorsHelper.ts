import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function noEmptyValidator(): ValidatorFn {
    return (control:AbstractControl) : ValidationErrors | null => {
        const value: string = control.value;
        if (!value) {
            return null;
        }
        
        const isEmpty = value.trim().length === 0;
        if(isEmpty){
            control.reset();
        }

        return isEmpty ? {isEmpty:true} : null;
    }
}
