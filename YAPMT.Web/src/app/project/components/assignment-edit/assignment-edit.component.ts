import {
  Component,
  OnInit,
  Input,
  OnDestroy,
  EventEmitter,
  Output,
} from '@angular/core';
import { Subscription } from 'rxjs/Rx';
import { AssignmentInsert } from '../../models/assignment-insert-model';
import { AssignmentService } from '../../services/assignment/assignment.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from '../../../core/message.service';

@Component({
  selector: 'app-assignment-edit',
  templateUrl: './assignment-edit.component.html',
  styleUrls: ['./assignment-edit.component.css'],
})
export class AssignmentEditComponent implements OnInit, OnDestroy {
  @Input() projectId: number;
  @Output() taskSaved = new EventEmitter();
  private serviceAssignSubs: Subscription;
  public form: FormGroup;
  public assignment: AssignmentInsert = new AssignmentInsert();
  constructor(private assignmentService: AssignmentService) {}

  ngOnInit() {
    this.initForm();
  }

  private initForm() {
    this.form = new FormGroup({
      description: new FormControl('', [Validators.required]),
      owner: new FormControl('', [Validators.required]),
      dueDate: new FormControl('', [Validators.required]),
    });
  }

  onSave() {
    if (!this.form.valid) {
      MessageService.ErrorToaster('Fill all fields.');
    } else {
      this.assignment.projectId = this.projectId;
      this.assignment.completed = false;
      this.serviceAssignSubs = this.assignmentService
        .save(this.assignment)
        .subscribe(res => {
          MessageService.SuccessToaster('New task succesfully saved.');
          this.taskSaved.emit({ assignId: res });
        });
    }
  }

  ngOnDestroy() {
    if (this.serviceAssignSubs) {
      this.serviceAssignSubs.unsubscribe();
    }
  }
}
