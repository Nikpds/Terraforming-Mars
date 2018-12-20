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
    AppRoutingModule
  ],
  providers: [MainService, AuthService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
