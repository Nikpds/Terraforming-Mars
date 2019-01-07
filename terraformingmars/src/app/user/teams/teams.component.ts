import { Component, OnInit } from '@angular/core';
import { Team, UserSearchView } from 'src/app/model';
import { LoaderService } from 'src/app/shared/loader.service';
import { ToastrService } from 'src/app/toastr.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.sass']
})
export class TeamsComponent implements OnInit {
  teams = new Array<Team>();
  selectedTeam: Team;
  newTeam: Team;
  searchField: string;
  usersFound = new Array<UserSearchView>();
  constructor(
    private loader: LoaderService,
    private toastr: ToastrService,
    private service: UserService) { }

  ngOnInit() {
    this.getMyTeams();
  }

  getMyTeams() {
    this.loader.show();
    this.service.getMyTeams().subscribe(res => {
      this.loader.hide();
      this.teams = res;
    }, error => {
      this.loader.hide();
    });
  }

  addOrUpdate(t: Team) {
    console.log(t);
    const i = this.teams.findIndex(x => x.id === t.id);
    if (i > -1) {
      this.teams[i] = t;
    } else {
      this.teams.unshift(t);
    }
    this.selectedTeam = t;
    this.newTeam = null;
  }

  selectTeam(i: number) {
    this.newTeam = null;
    this.selectedTeam = this.teams[i];
  }

  searchUser() {
    if (this.searchField.length < 4) { return; }
    if (this.searchField.length === 4) {
      this.service.searchPlayersForInvitation(this.searchField).subscribe(res => {
        console.log(res);
        this.usersFound = res;
      }, error => {

      });
    } else {
      this.usersFound = this.usersFound.filter(x => x.nickname === this.searchField || x.email === this.searchField);
    }
  }

  createNewTeam() {
    this.selectedTeam = null;
    this.newTeam = new Team();
    this.newTeam.icon = 'fa fa-check';
    this.newTeam.title = 'My Awsome Team';
    this.newTeam.color = '#e66465';
  }

  editTeam() {
    this.newTeam = new Team();
    this.newTeam.id = this.selectedTeam.id;
    this.newTeam.color = this.selectedTeam.color;
    this.newTeam.icon = this.selectedTeam.icon;
    this.newTeam.title = this.selectedTeam.title;
    this.selectedTeam = null;
  }

  deleteTeam() {
    const i = this.teams.findIndex(x => x.id === this.selectedTeam.id);
    this.loader.show();
    this.service.deleteTeam(this.selectedTeam.id).subscribe(res => {
      this.loader.hide();
      this.teams.splice(i, 1);
      this.selectedTeam = null;
      this.newTeam = null;
    }, error => {
      this.loader.hide();
    });
  }
}
