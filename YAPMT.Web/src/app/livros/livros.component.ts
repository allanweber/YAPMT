import { Component, OnInit, OnDestroy } from '@angular/core';
import { Livro } from './shared/models/livro.model';
import { LivroService } from './shared/services/livro.service';
import { Subscription } from 'rxjs/Subscription';
import { MessageService } from '../core/message.service';

@Component({
  selector: 'app-livros',
  templateUrl: './livros.component.html',
  styleUrls: ['./livros.component.css']
})
export class LivrosComponent implements OnInit, OnDestroy {
  public livros: Livro[];
  private serviceInscription: Subscription;
  constructor(private service: LivroService) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.serviceInscription = this.service
      .getAll()
      .subscribe(books => (this.livros = books));
  }

  ngOnDestroy() {
    this.serviceInscription.unsubscribe();
  }

  delete(livro: Livro) {
    MessageService.Confirm(
      `Tem certeza que deseja remover o livro ${livro.title}?`
    ).then(result => {
      if (result) {
        this.service.delete(livro.id).subscribe(res => {
          MessageService.SuccessToaster(
            `Livro ${livro.title} removido com sucesso.`
          );
          this.load();
        });
      }
    });
  }
}
