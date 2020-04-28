import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GroupsService } from '../groups.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {

  addForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private groupService: GroupsService,
    private router: Router,
    private toastrService: ToastrService) {
    this.addForm = this.fb.group({
      'subject': ['', [Validators.required]],
      'description': [''],
    });
  }

  ngOnInit(): void {
  }
  
  add() {
    if(this.addForm.valid) {
      this.toastrService.info('Creating a group!', 'Creating!', { closeButton: true, progressBar: true, extendedTimeOut: 2000, timeOut: 2000 });
      this.groupService.add(this.addForm.value).subscribe(id => {
        this.toastrService.success('The creation was successfully!', 'Created!', { closeButton: true, progressBar: true, extendedTimeOut: 2000, timeOut: 2000 });
        this.router.navigate(['groups/details', id]);
      }, errors => console.log(errors));
    }
  }

  cancel() {
    window.history.back();
  }

  get subject() {
    return this.addForm.get('subject');
  }

}
