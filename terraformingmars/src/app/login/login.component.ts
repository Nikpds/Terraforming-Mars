import { Component, OnInit } from '@angular/core';
import { User } from '../model';
import { MainService } from '../main.service';
import { AuthService } from '../auth.service';
import { LoaderService } from '../shared/loader.service';
import { Router } from '@angular/router';
import { ToastrService } from '../toastr.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  username: string;
  password: string;

  constructor(
    private toastr: ToastrService,
    private auth: AuthService,
    private loader: LoaderService,
    private route: Router) { }

  ngOnInit() {

  }

  // public socialSignIn(socialPlatform: string) {
  //   const user = new User();
  //   let socialPlatformProvider;
  //   if (socialPlatform === 'facebook') {
  //     socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
  //   } else if (socialPlatform === 'google') {
  //     socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
  //   }
  //   this.socialAuthService.signIn(socialPlatformProvider).then(
  //     (userData) => {
  //       user.email = userData.email;
  //       user.firstname = userData.name.split(' ')[0];
  //       user.lastname = userData.name.split(' ')[1];
  //       this.externlaLogin(user);

  //     }
  //   );
  // }
  // externlaLogin(u: User) {
  //   this.loader.show();
  //   this.service.externalRegister(u).subscribe(res => {
  //     localStorage.setItem('token', res.token);
  //     this.auth.initializeUser(res.token);
  //     this.auth.loggedIn = true;
  //     this.route.navigate(['/home']);
  //     this.loader.hide();
  //   }, error => {
  //     this.loader.hide();
  //   });
  // }
  login() {
    this.loader.show();
    this.auth.login(this.username, this.password).subscribe(res => {
      this.loader.hide();
      if (res) {
        this.username = null;
        this.password = null;
        this.route.navigate(['/home']);
      }
    }, error => {
      this.toastr.danger(error);
      this.loader.hide();

    });
  }
}
