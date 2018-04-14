import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppComponent } from './app.component';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppConfig, AppConfigFactory } from './core/app-config.service';
import { NavbarComponent } from './navbar/navbar.component';
import { PageNotFoundedComponent } from './page-not-founded/page-not-founded.component';
import { LivrosComponent } from './livros/livros.component';
import { LivroDetailComponent } from './livros/livro-detail/livro-detail.component';

import { routing } from './app.routing.module';
import { LivroService } from './livros/shared/services/livro.service';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    PageNotFoundedComponent,
    LivrosComponent,
    LivroDetailComponent,
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    routing,
  ],
  providers: [
    AppConfig,
    {
      provide: APP_INITIALIZER,
      useFactory: AppConfigFactory,
      deps: [AppConfig],
      multi: true,
    },
    LivroService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
