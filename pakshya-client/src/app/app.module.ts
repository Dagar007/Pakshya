import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { TimeAgoPipe } from 'time-ago-pipe';


import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './material.module';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PostComponent } from './post/post.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RightSidebarComponent } from './right-sidebar/right-sidebar.component';
import { PostCardsComponent } from './post/post-cards/post-cards.component';
import { CreateComponent } from './post/create/create.component';
import { PostDetailsComponent } from './post/post-details/post-details.component';
import { PostCardComponent } from './post/post-cards/post-card/post-card.component';
import { PostDelailsHeaderComponent } from './post/post-details/post-delails-header/post-delails-header.component';
import { PostDelailsCommentsComponent } from './post/post-details/post-delails-comments/post-delails-comments.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { NavComponent } from './nav/nav.component';
import { ProfileComponent } from './profile/profile.component';
import { ProfileHeaderComponent } from './profile/profile-header/profile-header.component';
import { PersonalAboutComponent } from './profile/personal-about/personal-about.component';
import { PakshyaStatsComponent } from './profile/pakshya-stats/pakshya-stats.component';
import { PhotoComponent } from './profile/personal-about/photo/photo.component';
import { InterestsComponent } from './profile/personal-about/interests/interests.component';
import { BioComponent } from './profile/personal-about/bio/bio.component';
import { EducationComponent } from './profile/personal-about/education/education.component';
import { WorkComponent } from './profile/personal-about/work/work.component';
import { LivingComponent } from './profile/personal-about/living/living.component';


export function tokenGetter() {
  return localStorage.getItem('token');
}


@NgModule({
  declarations: [
    AppComponent,
    ValueComponent,
    PostComponent,
    SidebarComponent,
    RightSidebarComponent,
    PostCardsComponent,
    CreateComponent,
    PostDetailsComponent,
    PostCardComponent,
    PostDelailsHeaderComponent,
    PostDelailsCommentsComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    NavComponent,
    ProfileComponent,
    ProfileHeaderComponent,
    PersonalAboutComponent,
    PakshyaStatsComponent,
    PhotoComponent,
    InterestsComponent,
    BioComponent,
    EducationComponent,
    WorkComponent,
    LivingComponent,
    TimeAgoPipe
  ],
  imports: [
    BrowserModule,
    InfiniteScrollModule,
    AppRoutingModule,
    MaterialModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/user/login','localhost:5000/api/user/register']
      }
    })
  ],
  providers: [ErrorInterceptorProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }
