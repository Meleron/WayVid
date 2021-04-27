import { Component } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";
import { ConfigService } from "./services/appconfig.service";
import { authConfig } from "./sso.config";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
})
export class AppComponent {
  title = "app";

  constructor(private configService: ConfigService, ssoService: OAuthService) {
    const oauthConf = Object.assign({}, authConfig);
    configService.loadConfig().subscribe((resp) => {
      oauthConf.issuer = resp.apiURL;
      oauthConf.tokenEndpoint = resp.apiURL + "/connect/token";
      ssoService.configure(oauthConf);
    });
  }
}
