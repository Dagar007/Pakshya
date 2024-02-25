import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';

import { SocialLoginModule, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import {
  FacebookLoginProvider
} from '@abacritt/angularx-social-login';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ErrorInterceptorProvider } from './core/interceptors/error.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './core/interceptors/loading.interceptors';
import { CoreModule } from './core/core.module';


export function tokenGetter() {
  return localStorage.getItem('token');
}

// declare module "@angular/core" {
//   interface ModuleWithProviders<T = any> {
//     ngModule: Type<T>;
//     providers?: Provider[];
//   }
// }

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CoreModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: [
          'localhost:5000/api/user/login',
          'localhost:5000/api/user/register'
        ]
      }
    }),
    SocialLoginModule,
    NgxSpinnerModule
  ],
  providers: [
    // provideRouter([
    //   {
    //     component: PostDelailsCommentsComponent,
    //     resolve: {comment: CommentResolver},
    //   },
    // ]),
    ErrorInterceptorProvider,
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('536520493877013')
          }
        ],
        onError: (err) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig,
    },
  ],
  //entryComponents: [LoginPopupComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
