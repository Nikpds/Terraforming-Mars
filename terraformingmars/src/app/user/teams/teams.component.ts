import { Component, OnInit } from '@angular/core';
import { Team, UserSearchView, InvitationDto } from 'src/app/model';
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
  selectedUser: string;
  members = new Array<InvitationDto>();
  newTeam: Team;
  searchField = '';
  showAddMember = false;
  comments: string;
  usersFound = new Array<UserSearchView>();
  usersResult = new Array<UserSearchView>();
  focused = false;
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
      this.toastr.danger(error);
      this.loader.hide();
    });
  }

  getMembersOfTeam() {
    this.service.getMembersAndInvites(this.selectedTeam.id).subscribe(res => {
      this.members = res;
    }, error => {
      this.toastr.danger(error);
    });
  }

  addOrUpdate(t: Team) {
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
    this.closeAddMember();
    this.newTeam = null;
    this.selectedTeam = this.teams[i];
    this.getMembersOfTeam();
  }

  searchUser() {
    if (this.searchField.length === 0) {
      this.focused = false;
      this.usersFound = [];
      return;
    }
    if (this.searchField.length < 4) { return; }
    if (this.searchField.length === 4) {
      this.service.searchPlayersForInvitation(this.searchField).subscribe(res => {
        this.focused = true;
        this.usersFound = res;
        this.usersResult = res;
      }, error => {

      });
    } else {
      this.focused = true;
      this.searchField = this.searchField.toLocaleLowerCase();
      this.usersFound = this.usersResult.filter(x => x.nickname.toLocaleLowerCase().includes(this.searchField) ||
        x.email.toLocaleLowerCase().includes(this.searchField));
    }
  }

  selectPlayerForInvite(a: UserSearchView) {
    this.searchField = a.nickname;
    this.selectedUser = a.id;
    this.focused = false;
  }

  sendInvitation() {
    this.loader.show();
    this.focused = false;
    this.service.sendInvitation(this.selectedUser, this.selectedTeam.id, this.comments).subscribe(res => {
      this.toastr.success('The invitation was sent');
      this.loader.hide();
      this.members.unshift(res);
      this.closeAddMember();
    }, error => {
      this.loader.hide();
      this.closeAddMember();
      this.toastr.success(error);
    });
  }

  resendInitation(i: number) {
    this.loader.show();
    this.service.reSendInvitation(this.members[i].invitationsId).subscribe(res => {
      this.toastr.success('The invitation was sent again');
      this.loader.hide();
      this.members[i] = res;
    }, error => {
      this.loader.hide();
      this.toastr.success(error);
    });
  }

  confirmInvitation() {
    this.loader.show();
    this.focused = false;
    this.service.sendInvitation(this.selectedUser, this.selectedTeam.id, this.comments).subscribe(res => {
      this.toastr.closeAll();
      this.loader.hide();
    }, error => {
      this.loader.hide();
      this.toastr.closeAll();
    });
  }

  createNewTeam() {
    this.closeAddMember();
    this.selectedTeam = null;
    this.newTeam = new Team();
    this.newTeam.icon = 'fa fa-check';
    this.newTeam.title = 'My Awsome Team';
    this.newTeam.color = '#e66465';
    this.closeAddMember();
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
      this.closeAddMember();
      this.newTeam = null;
      this.toastr.success('Team was deleted');
    }, error => {
      this.loader.hide();
      this.toastr.danger(error);
    });
  }

  closeAddMember() {
    this.selectedUser = null;
    this.searchField = '';
    this.showAddMember = false;
    this.usersFound = new Array<UserSearchView>();
    this.usersResult = new Array<UserSearchView>();
    this.focused = false;
    this.comments = null;
    this.members = new Array<InvitationDto>();
  }
}
