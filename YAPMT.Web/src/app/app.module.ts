import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppComponent } from './app.component';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppConfig, AppConfigFactory } from './core/app-config.service';
import { NavbarComponent } from './navbar/navbar.component';
import { PageNotFoundedComponent } from './page-not-founded/page-not-founded.component';
import { NgxMaskModule } from 'ngx-mask';

import { routing } from './app.routing.module';
import { ProjectService } from './project/services/project/project.service';
import { AssignmentService } from './project/services/assignment/assignment.service';
import { ProjectComponent } from './project/project.component';
import { AssignmentListComponent } from './project/components/assignment-list/assignment-list.component';
import { MessageBusService } from './core/message-bus.service';
import { ProjectStatusComponent } from './project/components/project-status/project-status.component';
import { AssignmentDetailComponent } from './project/components/assignment-detail/assignment-detail.component';
import { AssignmentEditComponent } from './project/components/assignment-edit/assignment-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    PageNotFoundedComponent,
    ProjectComponent,
    AssignmentListComponent,
    ProjectStatusComponent,
    AssignmentDetailComponent,
    AssignmentEditComponent,
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    routing,
    NgxMaskModule.forRoot(),
  ],
  providers: [
    AppConfig,
    {
      provide: APP_INITIALIZER,
      useFactory: AppConfigFactory,
      deps: [AppConfig],
      multi: true,
    },
    ProjectService,
    MessageBusService,
    AssignmentService,
  ],

  bootstrap: [AppComponent],
})
export class AppModule {}
