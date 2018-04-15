import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundedComponent } from './page-not-founded/page-not-founded.component';

import { ProjectComponent } from './project/project.component';

const appRoutes: Routes = [
  {
    path: 'project',
    children: [
      { path: 'novo', component: ProjectComponent },
      { path: ':id', component: ProjectComponent },
    ],
  },
  { path: '', redirectTo: '/project/novo', pathMatch: 'full' },
  { path: '**', component: PageNotFoundedComponent },
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes, {
  useHash: true,
});
