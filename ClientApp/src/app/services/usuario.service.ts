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
}