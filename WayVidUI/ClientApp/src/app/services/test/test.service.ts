import { Injectable, Injector } from '@angular/core';
import { ConfigService } from '../appconfig.service';
import { BaseHttpService } from '../http-request.service';

@Injectable({
  providedIn: 'root'
})
export class TestService extends BaseHttpService<string> {

  constructor(protected injector: Injector) {
    super(injector);
  }
}
