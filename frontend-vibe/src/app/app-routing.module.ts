import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileListComponent } from "./components/file-list/file-list.component";
import { AvatarListComponent } from './components/avatar-list/avatar-list.component'
import { AvatarVideoAnalysisComponent } from './components/avatar-video-analysis/avatar-video-analysis.component';
import { AvatarImageAnalysisComponent } from './components/avatar-image-analysis/avatar-image-analysis.component';
import { AvatarAnalysisListComponent } from "./components/avatar-analysis-list/avatar-analysis-list.component";
import { AvatarAnalysisInfoComponent } from "./components/avatar-analysis-info/avatar-analysis-info";
import { AvatarEditorComponent } from "./components/avatar-editor/avatar-editor.component";
import { AvatarDetailsComponent } from "./components/avatar-details/avatar-details.component";
import { AvatarImageAnalysisResultComponent } from './components/avatar-image-analysis-result/avatar-image-analysis-result.component';
import { AvatarVideoAnalysisResultComponent } from './components/avatar-video-analysis-result/avatar-video-analysis-result.component';
import { NewAnalysisComponent } from './components/new-analysis/new-analysis.component';
import { FileManagerComponent } from "./components/file-manager/file-manager.component";

const routes: Routes = [
  { path: '', redirectTo: '/avatars', pathMatch: 'full' },
  { path: 'avatars', component: AvatarListComponent, },
  { path: 'avatars/:avatar', component: AvatarDetailsComponent, data: { avatarId: ':avatar' }, },
  { path: '', redirectTo: '/avatars', pathMatch: 'full' },
  { path: 'new-analysis', component: NewAnalysisComponent, },
  { path: 'file-manager', component: FileManagerComponent, },
  { path: 'avatars', component: AvatarListComponent, },
  { path: 'avatars/:avatar', component: AvatarDetailsComponent, data: { avatarId: ':avatar' }, },
  {
    path: 'avatars/:avatar/analysis/:analysis',
    component: AvatarAnalysisInfoComponent,
    data: { avatarId: ':avatar', analysisId: ':analysis' },
  },
  { path: 'avatar-editor', component: AvatarEditorComponent, },
  {
    path: 'avatars/:avatar/image-analysis-result/:analysis', component: AvatarImageAnalysisResultComponent,
    data: { avatarId: ':avatar', analysisId: ':analysis' }
  },
  {
    path: 'avatars/:avatar/video-analysis-result/:analysis', component: AvatarVideoAnalysisResultComponent,
    data: { avatarId: ':avatar', analysisId: ':analysis' }
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

export const routingComponents = [NewAnalysisComponent, FileManagerComponent, AvatarListComponent, AvatarVideoAnalysisComponent, AvatarImageAnalysisComponent, AvatarAnalysisListComponent, AvatarAnalysisInfoComponent, FileListComponent]
