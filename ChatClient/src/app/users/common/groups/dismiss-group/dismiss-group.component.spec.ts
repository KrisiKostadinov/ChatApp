import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DismissGroupComponent } from './dismiss-group.component';

describe('DismissGroupComponent', () => {
  let component: DismissGroupComponent;
  let fixture: ComponentFixture<DismissGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DismissGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DismissGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
