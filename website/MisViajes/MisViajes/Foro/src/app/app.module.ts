import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentComponent } from './component/component.component';
import { HeaderForumComponent } from './component/header-forum/header-forum.component';
import { CardComponent } from './component/card/card.component';
import { NewDiscussionComponent } from './component/new-discussion/new-discussion.component';
import { SidebarComponent } from './component/sidebar/sidebar.component';

@NgModule({
  declarations: [
    AppComponent,
    ComponentComponent,
    HeaderForumComponent,
    CardComponent,
    NewDiscussionComponent,
    SidebarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
