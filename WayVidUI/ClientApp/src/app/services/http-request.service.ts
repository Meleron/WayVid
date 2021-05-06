import { Injectable, Injector } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ConfigService } from "./appconfig.service";
import { flatMap, map } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export abstract class BaseHttpService<TModel> {
  protected http: HttpClient;
  protected configService: ConfigService;

  protected constructor(
    protected injector: Injector,
  ) {
    this.http = injector.get(HttpClient);
    this.configService = injector.get(ConfigService);
  }

  public getRequest(route: string): Observable<TModel> {
    return this.configService.loadConfig().pipe(
      flatMap((data) => {
        return this.http.get<TModel>(`${data.apiURL}/${route}`);
      })
    );
  }

  public postRequest(route: string, model: TModel): Observable<TModel> {
    return this.configService.loadConfig().pipe(
      flatMap((data) => {
        return this.http.get<TModel>(`${data.apiURL}/${route}`, model);
      })
    );
  }
}
