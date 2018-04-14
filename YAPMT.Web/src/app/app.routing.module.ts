import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LivrosComponent } from './livros/livros.component';
import { LivroDetailComponent } from './livros/livro-detail/livro-detail.component';
import { PageNotFoundedComponent } from './page-not-founded/page-not-founded.component';

const appRoutes: Routes = [
  {
    path: 'livros',
    component: LivrosComponent
  },
  { path: 'livros/:livro', component: LivroDetailComponent },
  { path: 'livros/novo', component: LivroDetailComponent },
  { path: '', redirectTo: '/livros', pathMatch: 'full' },
  { path: '**', component: PageNotFoundedComponent }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes, {
  useHash: true
});
