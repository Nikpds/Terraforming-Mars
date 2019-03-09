import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { GamesDetailsComponent } from './games-details/games-details.component';
import { AuthGuard } from './auth.guard';
import { GmGuard } from './gm.guard';
import { AdminGuard } from './admin.guard';
import { RatingsComponent } from './ratings/ratings.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TeamsComponent } from './user/teams/teams.component';
import { GamesComponent } from './user/games/games.component';
import { ProfileComponent } from './user/profile/profile.component';
import { UsersComponent } from './user/users/users.component';
import { ChangepasswordComponent } from './user/profile/changepassword/changepassword.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'teams', component: TeamsComponent, canActivate: [AuthGuard, GmGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'profile/change/password', component: ChangepasswordComponent, canActivate: [AuthGuard] },
  { path: 'register', component: GamesComponent, canActivate: [AuthGuard] },
  { path: 'games', component: GamesComponent, canActivate: [AuthGuard] },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard, AdminGuard] },
  { path: 'ratings', component: RatingsComponent, canActivate: [AuthGuard] },
  { path: 'game/:id', component: GamesDetailsComponent, canActivate: [AuthGuard, GmGuard] },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: 'home' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
