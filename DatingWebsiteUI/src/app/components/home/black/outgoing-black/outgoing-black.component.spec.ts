import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OutgoingBlackComponent } from './outgoing-black.component';

describe('OutgoingBlackComponent', () => {
  let component: OutgoingBlackComponent;
  let fixture: ComponentFixture<OutgoingBlackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OutgoingBlackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OutgoingBlackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
