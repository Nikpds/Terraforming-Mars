import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  private openJobs = 0;
  private _selector = 'loader-wrap';
  private _element: HTMLElement;

  constructor() {
    this._element = document.getElementById(this._selector);
  }

  show(): void {
    this.openJobs += 1;
    setTimeout(() => {
      if (this.openJobs > 0) {
        this._element.style['display'] = 'block';
      }
    }, 0);
  }

  hide(delay = 0): void {
    this.openJobs -= 1;
    if (this.openJobs < 1) {
      setTimeout(() => {
        this._element.style['display'] = 'none';
      }, delay);
    }
  }
}
