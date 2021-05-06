import { Component } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";
import { ConfigService } from "./services/appconfig.service";
import { AuthService } from "./services/auth/auth.service";
import { authConfig } from "./sso.config";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
})
export class AppComponent {
  title = "app";

  constructor(
    private configService: ConfigService,
    ssoService: OAuthService,
    private authService: AuthService
  ) {
    const oauthConf = Object.assign({}, authConfig);

    // setting up ssoService, filling localStorage
    configService.loadConfig(true).subscribe((resp) => {
      // setting received config values to localStorage
      localStorage.setItem("apiUrl", resp.apiURL);

      // configuring ssoService
      oauthConf.issuer = resp.apiURL + "/";
      oauthConf.tokenEndpoint = resp.apiURL + "/connect/token";
      ssoService.configure(oauthConf);
      ssoService.setStorage(localStorage);

      // trying to login using stored token
      ssoService.loadDiscoveryDocumentAndTryLogin().then(() => {
        authService.authConfigLoaded.emit(true);
      });

      // after login process is completed printing user information
      authService.isLoggedIn.subscribe((isLoggedInResp) => {
        console.log(`Issuer: ${oauthConf.issuer}`);
        console.log(`Login status: ${isLoggedInResp}`);
        console.log(`UserName: ${authService.getUsername()}`);
      });
    });
  }
}
