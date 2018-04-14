import { Injectable } from '@angular/core';
import { Livro } from '../models/livro.model';
import { Http } from '@angular/http';
import { BaseRequests } from '../../../core/base-requests';
import { AppConfig } from '../../../core/app-config.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class LivroService extends BaseRequests {
  constructor(private http: Http, private appConfig: AppConfig) {
    super();
  }

  getAll(): Observable<Livro[]> {
    return this.http
      .get(
        `${this.appConfig.backendApi}/api/v1/book`,
        this.getOptionsHeader()
      )
      .map(result => result.json())
      .catch(this.handleError);
  }

  getById(id: number): Observable<Livro> {
    return this.http
      .get(
        `${this.appConfig.backendApi}/api/v1/book/${id}`,
        this.getOptionsHeader()
      )
      .map(result => result.json())
      .catch(this.handleError);
  }

  save(livro: Livro) {
    if (livro.id === undefined || livro.id === 0) {
      return this.http
        .post(
          `${this.appConfig.backendApi}/api/v1/book`,
          livro,
          this.getOptionsHeader()
        )
        .map(result => result.json())
        .catch(this.handleError);
    } else {
      return this.http
        .put(
          `${this.appConfig.backendApi}/api/v1/book`,
          livro,
          this.getOptionsHeader()
        )
        .map(result => result.json())
        .catch(this.handleError);
    }
  }

  delete(id: number) {
    return this.http
      .delete(
        `${this.appConfig.backendApi}/api/v1/book/${id}`,
        this.getOptionsHeader()
      )
      .map(result => result.json())
      .catch(this.handleError);
  }
}
