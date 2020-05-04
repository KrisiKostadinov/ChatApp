import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAllRequestsComponent } from './list-all-requests.component';

describe('ListAllRequestsComponent', () => {
  let component: ListAllRequestsComponent;
  let fixture: ComponentFixture<ListAllRequestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListAllRequestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAllRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
