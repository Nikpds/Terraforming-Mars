import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/shared/loader.service';
import { ToastrService } from 'src/app/toastr.service';
import { UserService } from '../user.service';
import { User, UserProfile } from 'src/app/model';

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
      console.log(res);
      this.user = res;
      this.loader.hide();
    }, error => {
      this.toastr.danger(error);
      this.loader.hide();
    });
  }

  joinDeclineTeam(st: number, id: string) {
    this.loader.show();
    this.service.joinDeclineTeam(st, id).subscribe(res => {
      console.log(res);
      this.user.teams.push(res);
      this.loader.hide();
    }, error => {
      this.toastr.danger(error);
      this.loader.hide();
    });
  }


}
