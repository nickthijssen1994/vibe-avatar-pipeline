import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvatarAnalysisListComponent } from './avatar-analysis-list.component';

describe('AvatarAnalysisListComponent', () => {
  let component: AvatarAnalysisListComponent;
  let fixture: ComponentFixture<AvatarAnalysisListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AvatarAnalysisListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AvatarAnalysisListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
