import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Rx';
import { Assignment } from '../../models/assignment.model';
import { AssignmentService } from '../../services/assignment/assignment.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-assignment-list',
  templateUrl: './assignment-list.component.html',
  styleUrls: ['./assignment-list.component.css'],
})
export class AssignmentListComponent implements OnInit, OnDestroy {
  private serviceAssignSubs: Subscription;
  private paramSubscription: Subscription;
  public assignments: Assignment[];
  public projectId: number;
  constructor(
    private assignmentService: AssignmentService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.paramSubscription = this.route.params.subscribe((params: any) => {
      this.projectId = params['id'];
      if (this.projectId) {
        this.serviceAssignSubs = this.assignmentService
          .getByProjectId(this.projectId)
          .subscribe(res => {
            this.assignments = res;
          });
      }
    });
  }

  ngOnDestroy() {
    if (this.serviceAssignSubs) {
      this.serviceAssignSubs.unsubscribe();
    }
    if (this.paramSubscription) {
      this.paramSubscription.unsubscribe();
    }
  }

  addTask() {
    let assign = new Assignment();
    assign.isNew = true;
    this.assignments.push(assign);
  }

  hasAnyNew(): boolean {
    if (this.assignments) {
      const news = this.assignments.filter(as => as.isNew);
      return news.length > 0;
    } else {
      return false;
    }
  }

  taskSaved(event) {
    this.load();
  }
}
