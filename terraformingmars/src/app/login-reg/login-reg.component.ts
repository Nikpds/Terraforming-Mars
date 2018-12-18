import { Component, OnInit, Input, HostListener } from '@angular/core';

@Component({
  selector: 'app-login-reg',
  templateUrl: './login-reg.component.html',
  styleUrls: ['./login-reg.component.sass']
})
export class LoginRegComponent implements OnInit {
  @Input() showLogin = { modalIsOpen: false };
  isLogin = true;
  constructor() { }
  @HostListener('document:keydown.escape', ['$event']) onKeydownHandler(event: KeyboardEvent) {
    this.showLogin.modalIsOpen = false;
  }
  ngOnInit() {
  }

}
