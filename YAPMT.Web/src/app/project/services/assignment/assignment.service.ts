import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseRequests } from '../../../core/base-requests';
import { AppConfig } from '../../../core/app-config.service';
import { Observable } from 'rxjs/Observable';
import { Assignment } from '../../models/assignment.model';
import { AssignmentInsert } from '../../models/assignment-insert-model';
import { CommandResult } from '../../../core/command-result.model';

@Injectable()
export class AssignmentService extends BaseRequests {
  private path: string = 'api/v1/Assignment/';
  constructor(private http: Http, private appConfig: AppConfig) {
    super();
  }

  getByProjectId(projectId: number): Observable<Assignment[]> {
    return this.http
      .get(
        `${this.appConfig.backendApi}${this.path}project/${projectId}`,
        this.getOptionsHeader(),
      )
      .map(result => result.json())
      .catch(this.handleError);
  }

  doneTask(assignmentId: number) {
    return this.http
      .get(
        `${this.appConfig.backendApi}${this.path}${assignmentId}/done`,
        this.getOptionsHeader(),
      )
      .map(result => result.json())
      .catch(this.handleError);
  }

  save(assignment: AssignmentInsert): Observable<number> {
    return this.http
      .post(
        `${this.appConfig.backendApi}${this.path}`,
        assignment,
        this.getOptionsHeader(),
      )
      .map(result => {
        const command: CommandResult = result.json();
        return command.result;
      })
      .catch(this.handleError);
  }
}
