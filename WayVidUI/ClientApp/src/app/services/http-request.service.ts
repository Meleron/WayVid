import { Injectable, Injector } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ConfigService } from "./appconfig.service";
import { flatMap, map } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export abstract class HttpRequestService<TModel> {
  // private readonly apiUrl: string;
  private http: HttpClient;

  protected constructor(protected injector: Injector, private configService: ConfigService) {
    // this.apiUrl = "https://0.0.0.0:5011";
    this.http = injector.get(HttpClient);
  }

  public getRequest(route: string): Observable<TModel> {
    return this.configService.loadConfig().pipe(
      flatMap(data =>{
        return this.http.get<TModel>(`${data.apiURL}/${route}`);
      })
    );
    // return this.http.get<TModel>(`${this.apiUrl}/${route}`);
  }

  public postRequest(route: string, model: TModel): Observable<TModel> {
    return this.configService.loadConfig().pipe(
      flatMap(data =>{
        return this.http.get<TModel>(`${data.apiURL}/${route}`, model);
      })
    );
  }
}
