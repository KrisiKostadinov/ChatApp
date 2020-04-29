import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-dismiss-group',
  templateUrl: './dismiss-group.component.html',
  styleUrls: ['./dismiss-group.component.css']
})
export class DismissGroupComponent implements OnInit {

  @Output() onDismiss: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit(): void {
  }

  dismiss() {
    this.onDismiss.emit(true);
  }

  back() {
    this.onDismiss.emit(false);
  }
}
