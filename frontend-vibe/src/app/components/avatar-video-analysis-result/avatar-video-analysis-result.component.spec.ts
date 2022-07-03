import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvatarVideoAnalysisResultComponent } from './avatar-video-analysis-result.component';

describe('AvatarVideoAnalysisResultComponent', () => {
  let component: AvatarVideoAnalysisResultComponent;
  let fixture: ComponentFixture<AvatarVideoAnalysisResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AvatarVideoAnalysisResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AvatarVideoAnalysisResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
