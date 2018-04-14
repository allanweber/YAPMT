import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Livro } from '../shared/models/livro.model';
import { LivroService } from '../shared/services/livro.service';
import { Subscription } from 'rxjs/Subscription';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from '../../core/message.service';

@Component({
  selector: 'app-livro-detail',
  templateUrl: './livro-detail.component.html',
  styleUrls: ['./livro-detail.component.css']
})
export class LivroDetailComponent implements OnInit, OnDestroy {
  public form: FormGroup;
  public livro: Livro = new Livro();
  private serviceInscription: Subscription;
  private routeInscription: Subscription;
  public isSaving;

  constructor(
    private service: LivroService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.routeInscription = this.route.params.subscribe((params: any) => {
      if (params['livro'] && params['livro'] !== 'novo') {
        const id = params['livro'];
        this.serviceInscription = this.service
          .getById(id)
          .subscribe(book => (this.livro = book));
      }
    });

    this.initForm();
  }

  private initForm() {
    this.form = new FormGroup({
      title: new FormControl('', [Validators.required]),
      author: new FormControl("", [Validators.required]),
      year: new FormControl('', [Validators.required])
    });
  }

  onSave() {
    this.isSaving = true;
    this.service.save(this.livro).subscribe(res => {
      MessageService.SuccessToaster(`Livro ${this.livro.title} salvo com sucesso.`);
      this.router.navigate(['/livros']);
      this.isSaving = false;
    }, error => this.isSaving = false);
  }

  ngOnDestroy() {
    if (this.serviceInscription != null) {
      this.serviceInscription.unsubscribe();
    }
    this.routeInscription.unsubscribe();
  }
}
