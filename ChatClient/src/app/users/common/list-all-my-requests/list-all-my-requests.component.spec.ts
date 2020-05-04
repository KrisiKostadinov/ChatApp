import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAllMyRequestsComponent } from './list-all-my-requests.component';

describe('ListAllMyRequestsComponent', () => {
  let component: ListAllMyRequestsComponent;
  let fixture: ComponentFixture<ListAllMyRequestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListAllMyRequestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAllMyRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
