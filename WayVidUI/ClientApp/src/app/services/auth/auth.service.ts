import {Injectable, Injector} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {SignInModel} from '../../models/auth/sign-in-model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly apiUrl: string;
  private http: HttpClient;

  constructor(protected injector: Injector) {
    this.apiUrl = 'https://0.0.0.0:5011';
    this.http = injector.get(HttpClient);
  }

  public login(loginModel: SignInModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/identity/login`, loginModel);
  }

}
