import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProjectService } from '../project/services/project/project.service';
import { Router } from '@angular/router';
import { Project } from '../project/models/project.model';
import { Subscription } from 'rxjs/Subscription';
import { MessageBusService } from '../core/message-bus.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  public projects: Project[];
  private serviceInscription: Subscription;
  private paramSubscription: Subscription;
  private messageBusServiceSubs: Subscription;

  constructor(
    private projectService: ProjectService,
    private messageBusService: MessageBusService,
    private router: Router,
  ) {
    this.messageBusServiceSubs = this.messageBusService
      .getMessage()
      .subscribe((message: any) => {
        this.load(message.message);
      });
  }

  ngOnInit() {
    this.load();
  }

  load(projectId?: number) {
    this.serviceInscription = this.projectService.getAll().subscribe(resp => {
      this.projects = resp;
      if (!this.projects || this.projects.length == 0) {
        this.router.navigate(['project/novo']);
      } else {
        let id = projectId || this.projects[0].id;
        this.router.navigate(['project', id]);
      }
    });
  }

  ngOnDestroy() {
    this.serviceInscription.unsubscribe();
    this.messageBusServiceSubs.unsubscribe();
    if (this.paramSubscription) {
      this.paramSubscription.unsubscribe();
    }
  }
}
