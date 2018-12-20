import { Component, OnInit, Input, HostListener } from '@angular/core';
import { User } from '../model';
import { MainService } from '../main.service';
import { AuthService } from '../auth.service';
@Component({
  selector: 'app-login-reg',
  templateUrl: './login-reg.component.html',
  styleUrls: ['./login-reg.component.sass']
})
export class LoginRegComponent implements OnInit {
  @Input() showLogin = { modalIsOpen: false, isReg: false };
  user = new User();
  loader = false;
  username: string;
  password: string;
  constructor(
    private service: MainService,
    private auth: AuthService, ) { }

  @HostListener('document:keydown.escape', ['$event']) onKeydownHandler(event: KeyboardEvent) {
    this.showLogin.modalIsOpen = false;
    this.showLogin.isReg = false;
  }
  ngOnInit() {

  }

  register() {
    this.service.registerUser(this.user).subscribe(res => {
      console.log(res);
      this.showLogin.modalIsOpen = false;
      this.showLogin.isReg = false;
    }, error => {

    });
  }

  login() {
    this.loader = true;
    this.auth.login(this.username, this.password).subscribe(res => {
      this.loader = false;
      if (res) {
        this.username = null;
        this.password = null;
        this.showLogin.modalIsOpen = false;
        this.showLogin.isReg = false;
      }
    }, error => {
      this.loader = false;
    });
  }
}
