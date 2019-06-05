import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OutgoingFriendsComponent } from './outgoing-friends.component';

describe('OutgoingFriendsComponent', () => {
  let component: OutgoingFriendsComponent;
  let fixture: ComponentFixture<OutgoingFriendsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OutgoingFriendsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OutgoingFriendsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
