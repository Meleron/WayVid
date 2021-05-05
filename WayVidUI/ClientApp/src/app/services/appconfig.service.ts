import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { map } from "rxjs/operators";
import { ConfigModel } from "../models/client/config-model";

@Injectable()
export class ConfigService {
  private controller = "https://0.0.0.0:5001/config";

  constructor(private httpClient: HttpClient) {}

  public loadConfig(forcedUpdate: boolean = false): Observable<ConfigModel> {
    if (!this.config || forcedUpdate)
      return this.httpClient.get<ConfigModel>(this.controller).pipe(
        map((resp) => {
          this.config = resp;
          return resp;
        })
      );
    return of(this.config);
  }

  private get config(): ConfigModel {
    const cachedSessionConfigStr = sessionStorage.getItem("app-config");
    const cachedSessionConfig: ConfigModel = JSON.parse(cachedSessionConfigStr);

    return cachedSessionConfig;
  }

  private set config(value: ConfigModel) {
    sessionStorage.setItem("app-config", JSON.stringify(value));
  }
}
