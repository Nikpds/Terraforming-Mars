import { Component, OnInit } from '@angular/core';
import { User } from '../model';
import { MainService } from '../main.service';
import { LoaderService } from '../shared/loader.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {
  user = new User();
  verifyPassword: string;
  error = false;
  constructor(
    private service: MainService,
    private loader: LoaderService,
    private route: Router) { }

  ngOnInit() {
  }

  register() {
    if (this.verifyPassword !== this.user.password) {
      this.error = true;
      return;
    } else {
      this.error = false;
    }
    this.loader.show();
    this.service.registerUser(this.user).subscribe(res => {
      this.loader.hide();
      this.route.navigate(['/login']);
    }, error => {
      this.loader.hide();
    });
  }

}
