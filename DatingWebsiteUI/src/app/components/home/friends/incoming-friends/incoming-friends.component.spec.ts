import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomingFriendsComponent } from './incoming-friends.component';

describe('IncomingFriendsComponent', () => {
  let component: IncomingFriendsComponent;
  let fixture: ComponentFixture<IncomingFriendsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncomingFriendsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomingFriendsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
