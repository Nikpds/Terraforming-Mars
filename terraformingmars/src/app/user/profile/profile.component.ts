import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/shared/loader.service';
import { ToastrService } from 'src/app/toastr.service';
import { UserService } from '../user.service';
import { UserProfile, Team } from 'src/app/model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.sass']
})
export class ProfileComponent implements OnInit {
  user: UserProfile;

  constructor(
    private loader: LoaderService,
    private toastr: ToastrService,
    private service: UserService
  ) { }

  ngOnInit() {
    this.getProfile();
  }

  getProfile() {
    this.loader.show();
    this.service.getProfile().subscribe(res => {
      this.user = res;
      this.loader.hide();
    }, error => {
      this.toastr.danger(error);
      this.loader.hide();
    });
  }

  joinDeclineTeam(st: number, id: string) {
    this.loader.show();
    const ind = this.user.invitations.findIndex(x => x.id === id);
    if (ind < 0) { this.toastr.danger('Invalid inviatations selected'); }
    this.service.joinDeclineTeam(st, id).subscribe(res => {
      if (res) {
        const t = new Team();
        t.color = this.user.invitations[ind].color;
        t.icon = this.user.invitations[ind].icon;
        t.title = this.user.invitations[ind].title;
        t.id = this.user.invitations[ind].teamId;
        this.user.invitations[ind].status = 1;
        this.user.teams.push(t);
      } else {
        this.user.invitations[ind].status = 2;
      }
      this.loader.hide();
    }, error => {
      this.toastr.danger(error);
      this.loader.hide();
    });
  }



}
