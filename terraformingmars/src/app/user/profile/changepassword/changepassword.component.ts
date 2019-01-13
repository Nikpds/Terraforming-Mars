import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/shared/loader.service';
import { ToastrService } from 'src/app/toastr.service';
import { UserService } from '../../user.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.sass']
})
export class ChangepasswordComponent implements OnInit {
  current = '';
  newPassword = '';
  verify = '';
  showverify = false;
  shownewpass = false;
  showcurrent = false;
  constructor(
    private loader: LoaderService,
    private toastr: ToastrService,
    private service: UserService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  changePassword() {
    if (this.newPassword === '' || this.verify === '' || this.current === '') {
      this.toastr.warning('Please fill all the fields');
      return;
    }
    if (this.newPassword !== this.verify) {
      this.toastr.warning('New passwords does not match');
      return;
    }
    this.loader.show();
    this.service.changepassword(this.current, this.newPassword).subscribe(res => {
      this.toastr.success('Password has changed');
      this.router.navigate(['/profile']);
      this.loader.hide();
    }, error => {
      this.loader.hide();
      this.toastr.danger(error);
    });
  }

  showPassword(n: string) {
    const pass = document.createElement(n);
    pass.setAttribute('type', 'text');
  }
}
