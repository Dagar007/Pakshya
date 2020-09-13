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
  { path: '', runGuardsAndResolvers: 'always', component: PostComponent,  },
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: RegisterComponent }
    ]
  },
  { path: 'forget-password', component: ForgetPasswordComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'posts/:id', component: PostDetailsComponent, resolve: {comments: CommentResolver}  },
      { path: 'create-post', component: CreateComponent },
      { path: 'create-post/:id', component: CreateComponent },
      { path: 'profile/:username', component: ProfileComponent }
    ]
  },


  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'top'})],
  exports: [RouterModule]
})
export class AppRoutingModule {}
