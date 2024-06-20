import { FormControl, FormGroup } from "@angular/forms";

export default class ValidateForm {
    static validarCamposFormulario(formulario: FormGroup) {
        Object.keys(formulario.controls).forEach(field=> {
          const control = formulario.get(field);
          if (control instanceof FormControl) {
            control.markAsDirty({onlySelf: true});
          } else if (control instanceof FormGroup) {
            this.validarCamposFormulario(control)
          }
        })
    }
}