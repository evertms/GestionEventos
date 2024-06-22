import { Component, Inject } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UsuarioService } from 'src/app/services/UsuarioService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  registerForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder, 
    //private usuarioService: UsuarioService,
    private router: Router
  ) { }

  ngOnInit(): void{
    this.registerForm = this.formBuilder.group({
      nombre: ['', Validators.required],
      direccion: ['', Validators.required],
      fechaNacimiento: ['', Validators.required],
      correo: ['', [Validators.required, Validators.email]],
      telefono: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(8)]],
      organizacion: ['', Validators.required],
      cargo: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(8)]]
    })
  }

  registrarUsuario(){
    if (!this.registerForm.valid){
      alert("Campos invalidos")
    }
    else if (this.registerForm.value.password != this.registerForm.value.confirmPassword){
      alert("Contraseñas diferentes")
    }
    else {
      //this.usuarioService.registerUsuario(this.registerForm).subscribe( resultado =>{
        alert("Usuario Registrado")
        this.registerForm.reset();
        this.router.navigate(['/login']);
      //}, error => console.log(error));
    }
  }


}
/*{
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

  registrarUsuario(){
    if(!this.signUpForm.valid){
      alert("Campos invalidos")
    }
    else{
      this.usuarioService.registrarUsuario(
        this.nombreCompleto,
        this.direccion,
        this.fechaNacimiento,
        this.telefono,
        this.organizacion,
        this.cargo,
        this.email,
        this.password).subscribe( resultado =>{
        alert("Usuario Registrado")
        this.signUpForm.reset();
        this.router.navigate(['/login']);
      }, error => console.log(error));
    }
  }
}*/
