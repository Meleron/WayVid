import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { flatMap } from "rxjs/operators";
import { ConfigService } from "./appconfig.service";
import { AuthService } from "./auth/auth.service";
import { BaseHttpService } from "./http-request.service";

@Injectable({
  providedIn: "root",
})
export class VisitorService extends BaseHttpService<string> {
  constructor(protected injector: Injector, private authService: AuthService) {
    super(injector);
  }

  protected get controllerName(): string {
    return '/visitor';
  }

  public onOwnerRoleRequest(): Observable<any> {
    return this.configService.loadConfig().pipe(flatMap(resp => {
      return this.http.post(`${resp.apiURL}${this.controllerName}/OwnerRoleRequest`, null);
    }));
  }
}
