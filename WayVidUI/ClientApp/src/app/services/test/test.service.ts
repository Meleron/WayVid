import { Injectable, Injector } from '@angular/core';
import { ConfigService } from '../appconfig.service';
import { HttpRequestService } from '../http-request.service';

@Injectable({
  providedIn: 'root'
})
export class TestService extends HttpRequestService<string> {

  constructor(protected injector: Injector, configService: ConfigService) {
    super(injector, configService);
  }
}
