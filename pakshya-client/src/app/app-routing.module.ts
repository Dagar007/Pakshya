import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  { path: 'posts',
    loadChildren: () => import('./post/post.module').then(mod => mod.PostModule)},
  { path: 'account', loadChildren: () => import('./auth/auth.module').then(mod => mod.AuthModule)},
  { path: 'profile', canActivate: [AuthGuard],
    loadChildren: () => import('./profile/profile.module').then(mod => mod.ProfileModule)},
  { path: '**', redirectTo: 'posts', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
