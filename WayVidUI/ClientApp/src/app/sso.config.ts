import { AuthConfig } from "angular-oauth2-oidc";

export const authConfig: AuthConfig = {
  // issuer: 'https://0.0.0.0:5011',
  // tokenEndpoint: "https://0.0.0.0:5011/connect/token",
  redirectUri: window.location.origin + "/index.html",
  scope: "offline_access",
  showDebugInformation: true,
};
