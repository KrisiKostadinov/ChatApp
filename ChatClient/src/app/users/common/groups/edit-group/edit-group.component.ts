import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Group } from '../models/group.model';
import { GroupsService } from '../groups.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.css']
})
export class EditGroupComponent implements OnInit {

  editForm: FormGroup;
  group: Group;
  id: number;

  constructor(
    private fb: FormBuilder,
    private groupService: GroupsService,
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService) {
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.getData().then(data => {
      this.editForm = this.fb.group({
        'subject': [data.subject, [Validators.required]],
        'description': [data.description],
      });

      this.group = data;
    });
  }

  getData() {
    return this.groupService.byId(this.id).toPromise();
  }

  edit() {
    if(this.editForm.valid) {
      this.groupService.edit(this.id, this.editForm.value).subscribe(data => {
        this.router.navigate(['groups/details', this.id]);
        this.toastrService.success('The updating was successfully!', 'Updated!', { closeButton: true, progressBar: true, extendedTimeOut: 2000, timeOut: 2000 });
      });
    }
  }

  cancel() {
    window.history.back();
  }

  get subject() {
    return this.editForm.get('subject');
  }
}
