import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/AuthService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  
  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router, 
    @Inject('BASE_URL') private baseUrl: string,
    private authService : AuthService,
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]], 
      password: ['', Validators.required]
    });
  }

  onLogin() {
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

  login(loginObj: any) {
    const user = {
      correo: this.loginForm.value.email, 
      password: this.loginForm.value.password
    };
    return this.http.post<number>(this.baseUrl + 'usuario', user);
  }
}