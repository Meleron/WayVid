import { Injectable, Injector } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { SignInModel } from "../../models/auth/sign-in-model";
import { SignUpModel } from "../../models/auth/sign-up-model";
import { from, Observable, of } from "rxjs";
import { OAuthService } from "angular-oauth2-oidc";
import { authConfig } from "src/app/sso.config";
import { catchError, map } from "rxjs/operators";
import { ServerResponseModel } from "src/app/models/client/server-reponse";
import { UserModel } from "src/app/models/app/user-model";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private readonly apiUrl: string;
  private http: HttpClient;
  // private ssoService: OAuthService;

  constructor(protected injector: Injector, private ssoService: OAuthService) {
    this.apiUrl = "https://0.0.0.0:5011";
    this.http = injector.get(HttpClient);
    // this.ssoService = ssoService;
    // this.configureSSO();
  }

  private configureSSO() {
    // this.ssoService.configure(authConfig);
    // this.ssoService.setStorage(localStorage);
  }

  public login(loginModel: SignInModel): Observable<ServerResponseModel> {
    let response: ServerResponseModel = new ServerResponseModel();
    return from(
      this.ssoService.fetchTokenUsingPasswordFlow(
        loginModel.username,
        loginModel.password
      )
    ).pipe(
      map((resp) => {
        response.message = "Login succeeded";
        response.success = true;
        this.getPrimaryUserInfo().subscribe((userInfo) => {
          this.setUserInfo(userInfo);
        });
        return response;
      }),
      catchError((err) => {
        console.log(err);
        response.errorMessage = err.error.errorDescription;
        response.success = false;
        return of(response);
      })
    );
  }

  public signUp(signUpModel: SignUpModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/identity/register`, signUpModel);
  }

  private getPrimaryUserInfo(): Observable<UserModel> {
    return this.http.get<UserModel>(
      `${this.apiUrl}/user/getuserinfofromcontext`
    );
  }

  public getUserId(): string {
    return localStorage.getItem("id");
  }

  public getUserRole(): string {
    return localStorage.getItem("role");
  }

  public getUsername(): string {
    return localStorage.getItem("userName");
  }

  public test(): Observable<any> {
    console.log("qweqweqwe");
    // return this.http.get(`${this.apiUrl}/identity/WithAuth`);
    return this.http.get(`${this.apiUrl}/user/getuserinfofromcontext`);
  }

  public clearLocalStorage(){
    localStorage.clear();
  }

  private setUserInfo(response: UserModel) {
    localStorage.setItem("id", response.id ? response.id : null);
    localStorage.setItem(
      "role",
      response.role ? response.role.toString() : null
    );
    localStorage.setItem(
      "userName",
      response.userName ? response.userName : null
    );
  }
}
