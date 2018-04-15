import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseRequests } from '../../../core/base-requests';
import { AppConfig } from '../../../core/app-config.service';
import { Observable } from 'rxjs/Observable';
import { Project } from '../../models/project.model';
import { ProjectStatus } from '../../models/project-status.model';
import { CommandResult } from '../../../core/command-result.model';

@Injectable()
export class ProjectService extends BaseRequests {
  private path: string = 'api/v1/project/';
  constructor(private http: Http, private appConfig: AppConfig) {
    super();
  }

  getAll(): Observable<Project[]> {
    return this.http
      .get(`${this.appConfig.backendApi}${this.path}`, this.getOptionsHeader())
      .map(result => result.json())
      .catch(this.handleError);
  }

  getById(id: number): Observable<Project> {
    return this.http
      .get(
        `${this.appConfig.backendApi}${this.path}${id}`,
        this.getOptionsHeader(),
      )
      .map(result => result.json())
      .catch(this.handleError);
  }

  create(name: string): Observable<number> {
    return this.http
      .post(
        `${this.appConfig.backendApi}${this.path}`,
        { name },
        this.getOptionsHeader(),
      )
      .map(result => {
        const command: CommandResult = result.json();
        return command.result;
      })
      .catch(this.handleError);
  }

  getStatus(id: number): Observable<ProjectStatus> {
    return this.http
      .get(
        `${this.appConfig.backendApi}${this.path}${id}/status`,
        this.getOptionsHeader(),
      )
      .map(result => result.json())
      .catch(this.handleError);
  }

  delete(id: number): Observable<any> {
    return this.http
      .delete(
        `${this.appConfig.backendApi}${this.path}${id}`,
        this.getOptionsHeader(),
      )
      .map(result => result.json())
      .catch(this.handleError);
  }

  getFirst(): Observable<Project> {
    return this.http
      .get(
        `${this.appConfig.backendApi}/api/v1/project/first`,
        this.getOptionsHeader(),
      )
      .map(result => result.json())
      .catch(this.handleError);
  }
}
