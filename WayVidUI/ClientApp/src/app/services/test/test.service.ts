import { Injectable, Injector } from '@angular/core';
import { HttpRequestService } from '../http-request.service';

@Injectable({
  providedIn: 'root'
})
export class TestService extends HttpRequestService<string> {

  constructor(protected injector: Injector) {
    super(injector);
  }
}
