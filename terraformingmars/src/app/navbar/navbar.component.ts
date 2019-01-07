import { Component, HostListener, Inject, OnInit, OnDestroy } from '@angular/core';
import { DOCUMENT } from '@angular/platform-browser';
import { WINDOW } from '../shared/windowService';
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
  bck = false;
  modalOptions = { modalIsOpen: false, isReg: false };
  openMenu = false;
  constructor(
    private router: Router,
    private auth: AuthService,
    @Inject(DOCUMENT) private document: Document,
    @Inject(WINDOW) private window: Window
  ) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const url = (event.url.substr(1)).split('/')[0];
        this.currentUrl = url;
      }
    });
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const offset = this.window.pageYOffset || this.document.documentElement.scrollTop || this.document.body.scrollTop || 0;
    if (offset === 0) {
      this.bck = false;
    } else {
      this.bck = true;
    }
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }
  ngOnInit(): void {
    this.subscriptions.push(this.auth.user$.subscribe((user) => {
      this.user = user;
    }));
  }

  logout() {
    this.auth.logout();
  }



}
