import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { LoaderService } from 'src/app/shared/loader.service';
import { ToastrService } from 'src/app/toastr.service';
import { User } from 'src/app/model';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.sass']
})
export class UsersComponent implements OnInit {
  users = new Array<User>();
  constructor(
    private service: UserService,
    private loader: LoaderService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.getAllUsers();
  }
  getAllUsers() {
    this.loader.show();
    this.service.getAllUsers().subscribe(res => {
      this.users = res;
      this.loader.hide();
    }, error => {
      this.loader.hide();
      this.toastr.danger(error);
    });
  }

  changeUserStatus(userId: string, status: boolean) {
    this.loader.show();
    this.service.enableDisableUser(userId, status).subscribe(res => {
      if (res) {
        const i = this.users.findIndex(x => x.id === userId);
        this.users[i].isActive = status;
      }
      this.loader.hide();
    }, error => {
      this.toastr.danger(error);
      this.loader.hide();
    });
  }

  resetPassword(id: string) {
    this.loader.show();
    this.service.resetpassword(id).subscribe(res => {
      if (res) {
        this.toastr.success('Password has been reset');
      }
      this.loader.hide();
    }, error => {
      this.toastr.danger(error);
      this.loader.hide();
    });
  }
}
