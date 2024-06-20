import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl, FormBuilder } from '@angular/forms';
import { bolivianPhoneValidator, mustMatch, ageValidator } from '../../helpers/validator-util';
import ValidateForm from '../../helpers/validateForm'
import { HttpClient } from '@angular/common/http';
import { Usuario } from 'src/app/models/usuario';
import { UsuarioService } from 'src/app/services/usuario.service';
import { Router } from '@angular/router';
//import { SignUpService, Usuario } from 'src/app/services/sign-up.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  public date = new Date('dd/mm/yyyy');
  nombreCompleto = new FormControl('', Validators.required);
  direccion = new FormControl('', Validators.required);
  fechaNacimiento = new FormControl('', Validators.required);
  telefono = new FormControl('', Validators.required);
  organizacion = new FormControl('', Validators.required);
  cargo = new FormControl('', Validators.required);
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required, Validators.minLength(8)]);
  confirmPassword = new FormControl('', Validators.required);

  type: string = "password";
  signUpForm!: FormGroup;

  constructor(
    private fb: FormBuilder, 
    private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string, 
    private usuarioService: UsuarioService, 
    private router: Router
  ) {
    
  }

  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      nombreCompleto: new FormControl('', Validators.required),
      direccion: new FormControl('', Validators.required),
      fechaNacimiento: new FormControl('', Validators.required),
      telefono: new FormControl('', Validators.required),
      organizacion: new FormControl('', Validators.required),
      cargo: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      confirmPassword: new FormControl('', Validators.required)
    });
    this.fechaNacimiento.setValidators([
      Validators.required,
      ageValidator(18)
    ]);
    this.telefono.setValidators([
      Validators.required,
      bolivianPhoneValidator()
    ]);
    this.signUpForm.controls['fechaNacimiento'].setValidators([
      Validators.required,
      ageValidator(18)
    ]);
    this.signUpForm.controls['telefono'].setValidators([
      Validators.required,
      bolivianPhoneValidator()
    ]);
    this.signUpForm.controls['confirmPassword'].setValidators([
      Validators.required,
      mustMatch(this.signUpForm.controls['password'])
    ]);
  }

  removeErrorClass(controlName: FormControl) {
    controlName.markAsUntouched({onlySelf: true});
    controlName.markAsPristine({onlySelf: true});
  }

  /*registrarUsuario(){
    if(!this.signUpForm.valid){
      alert("Campos invalidos")
    }
    else{
      this.usuarioService.registrarUsuario(this.signUpForm).subscribe( resultado =>{
        alert("Usuario Registrado")
        this.signUpForm.reset();
        this.router.navigate(['/login']);
      }, error => console.log(error));
    }
  }*/
}
