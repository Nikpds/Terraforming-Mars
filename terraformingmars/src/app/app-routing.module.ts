import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { GamesDetailsComponent } from './games-details/games-details.component';
import { GamesListComponent } from './games-list/games-list.component';
import { AuthGuard } from './auth.guard';
import { RatingsComponent } from './ratings/ratings.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'games', component: GamesListComponent, canActivate: [AuthGuard] },
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
