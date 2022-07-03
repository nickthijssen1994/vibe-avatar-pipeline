import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from "@angular/forms";
import { FileListComponent } from './components/file-list/file-list.component';
import { AvatarAnalysisListComponent } from './components/avatar-analysis-list/avatar-analysis-list.component';
import { AvatarAnalysisInfoComponent } from "./components/avatar-analysis-info/avatar-analysis-info";
import { AvatarEditorComponent } from './components/avatar-editor/avatar-editor.component';
import { AvatarInfoComponent } from './components/avatar-info/avatar-info.component';
import { AvatarDetailsComponent } from './components/avatar-details/avatar-details.component';
import { AvatarImageAnalysisResultComponent } from './components/avatar-image-analysis-result/avatar-image-analysis-result.component';
import { AvatarVideoAnalysisResultComponent } from './components/avatar-video-analysis-result/avatar-video-analysis-result.component';
import { NewAnalysisComponent } from "./components/new-analysis/new-analysis.component";
import { FileManagerComponent } from './components/file-manager/file-manager.component';
import { MessagesLogComponent } from './components/messages-log/messages-log.component';

@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    NewAnalysisComponent,
    FileListComponent,
    AvatarAnalysisListComponent,
    AvatarAnalysisInfoComponent,
    AvatarEditorComponent,
    AvatarInfoComponent,
    AvatarDetailsComponent,
    MessagesLogComponent,
    AvatarImageAnalysisResultComponent,
    AvatarVideoAnalysisResultComponent,
    FileManagerComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
