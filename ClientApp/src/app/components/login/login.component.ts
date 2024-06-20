import { Component, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  type: string = "password";
  isText: boolean = false;
  ojo: string = "fa-eye-slash";
  loginForm!: FormGroup;
  logged = false;
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required, Validators.minLength(8)]);
  correo = this.email.toString();
  contra = this.password.toString();

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router, 
    @Inject('BASE_URL') private baseUrl: string,
    private authService : AuthService,
  ) {
    
  }
  
  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required, Validators.minLength(8)]
    });
  }

  ocultarMostrarContra() {
    this.isText = !this.isText;
    this.isText ? this.ojo = "fa-eye" : this.ojo = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  onSubmit() {
    if (this.loginForm.valid){
      this.login(this.loginForm.value).subscribe(result => {
        localStorage.setItem("user", JSON.stringify(result));
        this.authService.login(this.loginForm.value.email, this.loginForm.value.password);
        this.router.navigate(['/home']);
      }, error => {alert("Credenciales incorrectas")});
      
    } else {
      alert('Campos invalidos');
    }
  }

  removeErrorClass(controlName: FormControl) {
    controlName.markAsUntouched({onlySelf: true});
    controlName.markAsPristine({onlySelf: true});
  }
  
  login(loginObj: any) {
    const user = {
      correo: this.loginForm.value.email, 
      contrasenha: this.loginForm.value.password
    };
    return this.http.post<number>(this.baseUrl + 'usuario', user);
  }
}
