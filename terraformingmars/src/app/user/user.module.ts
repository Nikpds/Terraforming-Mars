import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { TeamsComponent } from './teams/teams.component';
import { GamesComponent } from './games/games.component';
import { ProfileComponent } from './profile/profile.component';
import { TeamDetailsComponent } from './team-details/team-details.component';

import { UserService } from './user.service';
import { UsersComponent } from './users/users.component';
import { ChangepasswordComponent } from './profile/changepassword/changepassword.component';

@NgModule({
  declarations: [
    TeamsComponent,
    GamesComponent,
    ProfileComponent,
    TeamDetailsComponent,
    UsersComponent,
    ChangepasswordComponent
  ],
  imports: [
    SharedModule,
    RouterModule
  ], exports: [

  ], providers: [
    UserService
  ]

})
export class UserModule { }
