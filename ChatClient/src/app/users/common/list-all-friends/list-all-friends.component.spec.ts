import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAllFriendsComponent } from './list-all-friends.component';

describe('ListAllFriendsComponent', () => {
  let component: ListAllFriendsComponent;
  let fixture: ComponentFixture<ListAllFriendsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListAllFriendsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAllFriendsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
