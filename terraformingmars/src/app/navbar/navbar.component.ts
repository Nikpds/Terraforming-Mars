import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from '../model';
import { AuthService } from '../auth.service';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass']
})
export class NavbarComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  currentUrl = 'home';
  user: User;
  modalOptions = { modalIsOpen: false, isReg: false };
  constructor(
    private router: Router,
    private auth: AuthService
  ) {
    this.subscriptions.push(this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const url = (event.url.substr(1)).split('/')[0];
        this.currentUrl = url;
      }
    }));
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }
  ngOnInit(): void {
    this.subscriptions.push(this.auth.user$.subscribe((user) => {
      if (user) {
        this.user = user;
      }
    }));
  }

  logout() {
    this.auth.logout();
  }

}
