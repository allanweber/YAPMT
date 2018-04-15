import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Rx';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectService } from './services/project/project.service';
import { Project } from './models/project.model';
import { MessageService } from '../core/message.service';
import { MessageBusService } from '../core/message-bus.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css'],
})
export class ProjectComponent implements OnInit {
  private paramSubscription: Subscription;
  private serviceProjectSubs: Subscription;

  public project: Project;

  constructor(
    private projectService: ProjectService,
    private route: ActivatedRoute,
    private router: Router,
    private messageBusService: MessageBusService,
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.paramSubscription = this.route.params.subscribe((params: any) => {
      let id: number = params['id'];
      if (id) {
        this.loadProject(id);
      }
    });
  }

  loadProject(id: number) {
    this.serviceProjectSubs = this.projectService.getById(id).subscribe(res => {
      this.project = res;
      if (this.project == null) {
        this.router.navigate(['naoEncontrado']);
      }
    });
  }

  create(projectName: string) {
    if (!projectName) {
      MessageService.ErrorToaster('Type the project name.');
    } else {
      this.serviceProjectSubs = this.projectService
        .create(projectName)
        .subscribe(res => {
          MessageService.SuccessToaster('Project created.');
          this.messageBusService.sendMessage(res);
          this.loadProject(res);
        });
    }
  }

  ngOnDestroy() {
    this.paramSubscription.unsubscribe();
    if (this.serviceProjectSubs) {
      this.serviceProjectSubs.unsubscribe();
    }
  }
}
