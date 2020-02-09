import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { PostComponent } from "./post/post.component";
import { PostDetailsComponent } from "./post/post-details/post-details.component";
import { CreateComponent } from "./post/create/create.component";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./auth/login/login.component";
import { RegisterComponent } from "./auth/register/register.component";
import { AuthGuard } from "./_guards/auth.guard";
import { ProfileComponent } from './profile/profile.component';
import { PostResolver } from './_resolvers/_post.resolver';

const routes: Routes = [
  { path: "", runGuardsAndResolvers:'always', component: PostComponent,  resolve: {posts:PostResolver } },
  {
    path: "",
    component: HomeComponent,
    children: [
      { path: "login", component: LoginComponent },
      { path: "signup", component: RegisterComponent }
    ]
  },
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      { path: "posts/:id", component: PostDetailsComponent },
      { path: "create-post", component: CreateComponent },
      { path: "create-post/:id", component: CreateComponent },
      { path: "profile/:username", component: ProfileComponent }
    ]
  },
 

  { path: "**", redirectTo: "", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{scrollPositionRestoration: 'top'})],
  exports: [RouterModule]
})
export class AppRoutingModule {}
