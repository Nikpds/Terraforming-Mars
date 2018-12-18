import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  currentUrl = 'home';

  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
       const url = (event.url.substr(1)).split('/')[0];
       this.currentUrl = url;
      }
    });
  }
  ngOnInit(): void {

  }
}
