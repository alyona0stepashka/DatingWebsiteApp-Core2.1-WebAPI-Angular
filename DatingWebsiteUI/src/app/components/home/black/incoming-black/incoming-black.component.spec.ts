import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomingBlackComponent } from './incoming-black.component';

describe('IncomingBlackComponent', () => {
  let component: IncomingBlackComponent;
  let fixture: ComponentFixture<IncomingBlackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncomingBlackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomingBlackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
