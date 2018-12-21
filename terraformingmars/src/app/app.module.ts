import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SharedModule } from './shared/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { GamesListComponent } from './games-list/games-list.component';
import { GamesDetailsComponent } from './games-details/games-details.component';
import { LoginRegComponent } from './login-reg/login-reg.component';
import { MainService } from './main.service';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';
import { RatingsComponent } from './ratings/ratings.component';
import { NavbarComponent } from './navbar/navbar.component';
import {
  SocialLoginModule,
  AuthServiceConfig,
  GoogleLoginProvider,
  FacebookLoginProvider,
} from 'angular-6-social-login';
export function getAuthServiceConfigs() {
  const config = new AuthServiceConfig(
    [
      {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider('393201351417039')
      },
      {
        id: GoogleLoginProvider.PROVIDER_ID,
        provider: new GoogleLoginProvider('869537187845-6ek57er8a90eg87i5l9r5j0h0oe6q3m7.apps.googleusercontent.com')
      }
    ]
  );
  return config;
}
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GamesListComponent,
    GamesDetailsComponent,
    LoginRegComponent,
    RatingsComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    SharedModule,
    AppRoutingModule,
    SocialLoginModule
  ],
  providers: [
    MainService,
    AuthService,
    AuthGuard,
    {
      provide: AuthServiceConfig,
      useFactory: getAuthServiceConfigs
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
