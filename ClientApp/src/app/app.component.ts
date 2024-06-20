import { Component } from '@angular/core';
import { Usuario } from './models/usuario';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  usuarios: Usuario[] = [];

  constructor() {}
  
  ngOnInit(): void {
    console.log(this.usuarios); 
  }
}
