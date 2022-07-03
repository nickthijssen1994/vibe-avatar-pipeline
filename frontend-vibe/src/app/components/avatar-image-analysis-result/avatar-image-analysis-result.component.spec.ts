import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvatarImageAnalysisResultComponent } from './avatar-image-analysis-result.component';

describe('AvatarImageAnalysisResultComponent', () => {
  let component: AvatarImageAnalysisResultComponent;
  let fixture: ComponentFixture<AvatarImageAnalysisResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AvatarImageAnalysisResultComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AvatarImageAnalysisResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
