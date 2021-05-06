import { Injectable, Injector } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { SignInModel } from "../../models/auth/sign-in-model";
import { SignUpModel } from "../../models/auth/sign-up-model";
import { BehaviorSubject, from, Observable, of, ReplaySubject } from "rxjs";
import { OAuthService } from "angular-oauth2-oidc";
import { authConfig } from "src/app/sso.config";
import { catchError, map, switchMap } from "rxjs/operators";
import { ServerResponseModel } from "src/app/models/client/server-reponse";
import { UserModel } from "src/app/models/app/user-model";
import { RoleType } from "src/app/enums/role-types";
import { Router } from "@angular/router";
import { EventEmitter } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private readonly apiUrl: string = localStorage.getItem("apiUrl");
  private http: HttpClient;
  // Using ReplaySubject instead of BehaviourSubject because the second one return unnecessary false value ob initial subscription
  private loggedInSubject: ReplaySubject<boolean> = new ReplaySubject<boolean>(
    1
  );
  public authConfigLoaded: EventEmitter<boolean> = new EventEmitter<boolean>();
  public isLoggedIn: Observable<boolean>;

  constructor(
    protected injector: Injector,
    private ssoService: OAuthService,
    private router: Router
  ) {
    // this.apiUrl = "https://0.0.0.0:5011";
    this.http = injector.get(HttpClient);
    this.isLoggedIn = this.loggedInSubject.asObservable();
    // this.isLoggedIn = this.isLoggedIn.pipe(
    //   map((resp) => {
    //     return resp;
    //   })
    // );
    this.authConfigLoaded.subscribe(() => {
      this.loggedInSubject.next(this.ssoService.hasValidAccessToken());
    });
  }

  public login(loginModel: SignInModel): Observable<ServerResponseModel> {
    let response: ServerResponseModel = new ServerResponseModel();
    return from(
      this.ssoService.fetchTokenUsingPasswordFlow(
        loginModel.username,
        loginModel.password
      )
    ).pipe(
      // switchMap switches one observable to another
      switchMap((resp) => {
        return this.getPrimaryUserInfo().pipe(
          map((userInfo) => {
            console.log(`Received UserInfo:`);
            console.log(userInfo);
            response.success = true;
            this.setUserInfo(userInfo);
            this.loggedInSubject.next(this.ssoService.hasValidAccessToken());
            return response;
          })
        );
      }),
      catchError((err) => {
        console.log(err);
        response.errorMessage = err.error.errorDescription;
        response.success = false;
        return of(response);
      })
    );
  }

  public logOut() {
    this.ssoService.logOut();
    this.clearAuthLocalStorage();
    this.loggedInSubject.next(false);
    this.router.navigateByUrl("/");
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

  public getUserRoles(): number[] {
    return JSON.parse(localStorage.getItem("roles"));
  }

  public getUsername(): string {
    return localStorage.getItem("userName");
  }

  public isInRole(role: RoleType): boolean {
    const userRoles = this.getUserRoles();
    return userRoles ? userRoles.includes(+role) : false;
  }

  public test(): Observable<any> {
    return this.http.get(`${this.apiUrl}/user/getuserinfofromcontext`);
  }

  public clearAuthLocalStorage() {
    localStorage.removeItem("id");
    localStorage.removeItem("roles");
    localStorage.removeItem("userName");
  }

  private setUserInfo(response: UserModel) {
    localStorage.setItem("id", response.id ? response.id : null);
    localStorage.setItem(
      "roles",
      response.roleList ? JSON.stringify(response.roleList) : null
    );
    localStorage.setItem(
      "userName",
      response.userName ? response.userName : null
    );
  }

  public navigateToUserPage() {
    switch (this.getUserRoles()[0]) {
      case RoleType.Visitor: {
        this.router.navigateByUrl("/visitor");
        break;
      }
      case RoleType.Owner: {
        this.router.navigateByUrl("/owner");
        break;
      }
      case RoleType.Admin: {
        this.router.navigateByUrl("/admin");
        break;
      }
    }
  }
}
