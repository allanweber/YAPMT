import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/Rx';
import { ProjectStatus } from '../../models/project-status.model';
import { Project } from '../../models/project.model';
import { ProjectService } from '../../services/project/project.service';
import { MessageService } from '../../../core/message.service';
import { MessageBusService } from '../../../core/message-bus.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-project-status',
  templateUrl: './project-status.component.html',
  styleUrls: ['./project-status.component.css'],
})
export class ProjectStatusComponent implements OnInit, OnDestroy {
  private serviceProjectSubs: Subscription;
  private paramSubscription: Subscription;
  public projectStatus: ProjectStatus;
  public isDeleting: boolean;
  private projectId: number;

  constructor(
    private projectService: ProjectService,
    private router: Router,
    private messageBusService: MessageBusService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.paramSubscription = this.route.params.subscribe((params: any) => {
      this.projectId = params['id'];
      if (this.projectId) {
        this.serviceProjectSubs = this.projectService
          .getStatus(this.projectId)
          .subscribe(res => (this.projectStatus = res));
      }
    });
  }

  delete() {
    this.isDeleting = true;
    MessageService.Confirm(
      `Are you sure you want to remove this project?`,
    ).then(result => {
      if (result) {
        this.serviceProjectSubs = this.projectService
          .delete(this.projectId)
          .subscribe(res => {
            MessageService.SuccessToaster(`Project successfully removed.`);
            this.isDeleting = false;
            this.messageBusService.sendMessage(undefined);
          }, error => (this.isDeleting = false));
      } else {
        this.isDeleting = false;
      }
    });
  }

  ngOnDestroy() {
    if (this.serviceProjectSubs) {
      this.serviceProjectSubs.unsubscribe();
    }
    if (this.paramSubscription) {
      this.paramSubscription.unsubscribe();
    }
  }
}
