import { Component, OnInit, Input, HostListener } from '@angular/core';
import { User } from '../model';
import { MainService } from '../main.service';
import { AuthService } from '../auth.service';
import {
  AuthService as SocialService,
  FacebookLoginProvider,
  GoogleLoginProvider
} from 'angular-6-social-login';
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
    private auth: AuthService,
    private socialAuthService: SocialService) { }

  @HostListener('document:keydown.escape', ['$event']) onKeydownHandler(event: KeyboardEvent) {
    this.showLogin.modalIsOpen = false;
    this.showLogin.isReg = false;
  }
  ngOnInit() {

  }

  public socialSignIn(socialPlatform: string) {
    const user = new User();
    let socialPlatformProvider;
    if (socialPlatform === 'facebook') {
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    } else if (socialPlatform === 'google') {
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
    }
    this.socialAuthService.signIn(socialPlatformProvider).then(
      (userData) => {
        user.email = userData.email;
        user.firstname = userData.name.split(' ')[0];
        user.lastname = userData.name.split(' ')[1];
        this.externlaLogin(user);

      }
    );
  }

  register() {
    this.service.registerUser(this.user).subscribe(res => {

      this.showLogin.isReg = false;
    }, error => {

    });
  }

  externlaLogin(u: User) {
    this.service.externalRegister(u).subscribe(res => {
      this.showLogin.modalIsOpen = false;
      this.showLogin.isReg = false;
      localStorage.setItem('token', res.token);
      this.auth.initializeUser(res.token);
      this.auth.loggedIn = true;
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
