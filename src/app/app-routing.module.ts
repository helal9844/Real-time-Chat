import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from 'src/Modules/layout/header/header.component';
import { HomeComponent } from 'src/Modules/main/home/home.component';
import { ListsComponent } from 'src/Modules/main/lists/lists.component';
import { LoginComponent } from 'src/Modules/main/login/login.component';
import { MemberDetailComponent } from 'src/Modules/main/members/member-detail/member-detail.component';
import { MemberListComponent } from 'src/Modules/main/members/member-list/member-list.component';
import { MessagesComponent } from 'src/Modules/main/messages/messages.component';
import { NotFoundComponent } from 'src/Modules/shared/Errors/not-found/not-found.component';
import { ServerErrorComponent } from 'src/Modules/shared/Errors/server-error/server-error.component';
import { TestErrorsComponent } from 'src/Modules/shared/Errors/test-errors/test-errors.component';
import { AuthGuard } from 'src/Modules/shared/Guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MemberListComponent },
      { path: 'members/:id', component: MemberDetailComponent },
      { path: 'lists', component: ListsComponent },
      { path: 'messages', component: MessagesComponent },
    ],
  },
  { path: 'errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },

  { path: 'LoginComponent', component: LoginComponent },
  { path: 'header', component: HeaderComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
