import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostComponent } from './post/post.component';
import { PostDetailsComponent } from './post/post-details/post-details.component';
import { CreateComponent } from './post/create/create.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { HomeComponent } from './auth/home/home.component';
import { ForgetPasswordComponent } from './auth/forget-password/forget-password.component';
import { AuthGuard } from './core/guards/auth.guard';
import { CommentResolver } from './shared/_resolvers/comments.resolver';

const routes: Routes = [
  { path: 'posts', canActivate: [AuthGuard],
    loadChildren: () => import('./post/post.module').then(mod => mod.PostModule)},
  { path: 'account', loadChildren: () => import('./auth/auth.module').then(mod => mod.AuthModule)},
  { path: 'profile', canActivate: [AuthGuard],
    loadChildren: () => import('./profile/profile.module').then(mod => mod.ProfileModule)},
  { path: '**', redirectTo: 'posts', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'top'})],
  exports: [RouterModule]
})
export class AppRoutingModule {}
