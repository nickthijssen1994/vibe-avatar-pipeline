import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessagesLogComponent } from './messages-log.component';

describe('MessagesLogComponent', () => {
  let component: MessagesLogComponent;
  let fixture: ComponentFixture<MessagesLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MessagesLogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MessagesLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
