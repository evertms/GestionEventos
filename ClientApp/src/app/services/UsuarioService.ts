import { HttpClient } from "@angular/common/http";
import { Usuario } from "../models/usuario";
import { Inject, Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";

@Injectable()
export class UsuarioService{

    http: HttpClient;
    baseUrl: string;
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string){
        this.http = http;
        this.baseUrl = baseUrl;
    }

    registerUsuario(registerForm: FormGroup){
        const usuario = {
            nombre: registerForm.value.nombre,
            direccion: registerForm.value.direccion,
            fechaNacimiento: registerForm.value.fechaNacimiento,
            correo: registerForm.value.correo,
            telefono: registerForm.value.telefono,
            organizacion: registerForm.value.organizacion,
            cargo: registerForm.value.cargo,
            password: registerForm.value.password
        }

        return this.http.post<Usuario>(this.baseUrl + 'usuario/signup', usuario)
    }

}