import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAllGroupsComponent } from './list-all-groups.component';

describe('ListAllGroupsComponent', () => {
  let component: ListAllGroupsComponent;
  let fixture: ComponentFixture<ListAllGroupsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListAllGroupsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAllGroupsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
