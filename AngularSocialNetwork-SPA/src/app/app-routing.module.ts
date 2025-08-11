import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';
import { LoginComponent } from 'src/app/login/login.component';
import { NotificationsComponent } from 'src/app/notifications/notifications.component';
import { ProfileComponent } from 'src/app/profile/profile.component';
import { PostDetailComponent } from './post-detail/post-detail.component';
import { FollowersComponent } from './followers/followers.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'profile/:id',
    component: ProfileComponent
  },
  {
    path: 'notifications',
    component: NotificationsComponent
  },
  {
    path: "postDetail/:id",
    component: PostDetailComponent
  },
  {
    path: "followers/:id",
    component: FollowersComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
