import {
  Headers,
  Http,
  RequestOptions,
  RequestOptionsArgs,
  Response,
  ResponseContentType
} from '@angular/http';
import { MessageService } from './message.service';
import { Observable } from 'rxjs/Observable';
declare var swal: any;

export class BaseRequests {
  getHeaders(): Headers {
    const headers = new Headers();
    headers.append('Access-Control-Allow-Origin', '*');
    return headers;
  }

  getOptionsHeader(): RequestOptions {
    return new RequestOptions({ headers: this.getHeaders() });
  }

  handleError(error): any {
    const errorObj = error.json();
    console.error('Ocorreu um erro', errorObj);

    if (errorObj.result) {
      MessageService.ErrorToaster(errorObj.result);
    } else {
      MessageService.fatalError(errorObj.Message);
    }

    return Observable.throw(error);
  }
}
