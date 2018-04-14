import { Inject, Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class AppConfig {
  private config: any = null;

  constructor(private http: Http) {}

  public get backendApi(): string {
    return this.getConfig('backendApi');
  }

  public getConfig(key: any) {
    return this.config[key];
  }

  public load() {
    return new Promise((resolve, reject) => {
      this.http
        .get('assets/config.json')
        .map(res => res.json())
        .catch((error: any): any => {
          resolve(true);
          return Observable.throw(error || 'Server error');
        })
        .subscribe(response => {
          this.config = response;
          resolve(true);
        });
    });
  }
}

export function AppConfigFactory(appConfig: AppConfig) {
  return () => appConfig.load();
}
