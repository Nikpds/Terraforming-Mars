import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  private openJobs = 0;
  private openJobs2 = 0;
  private _selector = 'loader-wrapper';
  private _selector2 = 'container-small-loader';
  private _element: HTMLElement;
  private _elementSmall: HTMLElement;

  constructor() {
    this._element = document.getElementById(this._selector);
    this._elementSmall = document.getElementById(this._selector2);
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

  showSmall(): void {
    this.openJobs2 += 1;
    setTimeout(() => {
      if (this.openJobs2 > 0) {
        this._elementSmall.style['display'] = 'block';
      }
    }, 300);
  }

  hideSmall(delay = 0): void {
    this.openJobs2 -= 1;
    if (this.openJobs2 < 1) {
      setTimeout(() => {
        this._elementSmall.style['display'] = 'none';
      }, delay);
    }
  }
}
