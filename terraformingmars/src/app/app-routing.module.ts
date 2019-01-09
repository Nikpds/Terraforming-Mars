import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { GamesDetailsComponent } from './games-details/games-details.component';
import { GamesListComponent } from './games-list/games-list.component';
import { AuthGuard } from './auth.guard';
import { RatingsComponent } from './ratings/ratings.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TeamsComponent } from './user/teams/teams.component';
import { GamesComponent } from './user/games/games.component';
import { ProfileComponent } from './user/profile/profile.component';
import { UsersComponent } from './user/users/users.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'teams', component: TeamsComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'register', component: GamesComponent, canActivate: [AuthGuard] },
  { path: 'games', component: GamesListComponent, canActivate: [AuthGuard] },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
  { path: 'ratings', component: RatingsComponent, canActivate: [AuthGuard] },
  { path: 'game/:id', component: GamesDetailsComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: 'home' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
