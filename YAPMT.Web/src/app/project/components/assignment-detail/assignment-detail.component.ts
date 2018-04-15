import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Rx';
import { Assignment } from '../../models/assignment.model';
import { AssignmentService } from '../../services/assignment/assignment.service';

@Component({
  selector: 'app-assignment-detail',
  templateUrl: './assignment-detail.component.html',
  styleUrls: ['./assignment-detail.component.css'],
})
export class AssignmentDetailComponent implements OnInit, OnDestroy {
  @Input() assignment: Assignment;
  private serviceAssignSubs: Subscription;
  constructor(private assignmentService: AssignmentService) {}

  ngOnInit() {}

  ngOnDestroy() {
    if (this.serviceAssignSubs) {
      this.serviceAssignSubs.unsubscribe();
    }
  }

  done() {
    this.serviceAssignSubs = this.assignmentService
      .doneTask(this.assignment.id)
      .subscribe();
  }
}
