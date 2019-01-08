import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { TeamsComponent } from './teams/teams.component';
import { GamesComponent } from './games/games.component';
import { ProfileComponent } from './profile/profile.component';
import { TeamDetailsComponent } from './team-details/team-details.component';

import { UserService } from './user.service';
import { UsersComponent } from './users/users.component';

@NgModule({
  declarations: [
    TeamsComponent,
    GamesComponent,
    ProfileComponent,
    TeamDetailsComponent,
    UsersComponent
  ],
  imports: [
    SharedModule
  ], exports: [

  ], providers: [
    UserService
  ]

})
export class UserModule { }
