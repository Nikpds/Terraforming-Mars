import { Injectable } from '@angular/core';

declare var toastr: any;
@Injectable({
  providedIn: 'root'
})
export class ToastrService {
  constructor() {
    toastr.options.progressBar = true;
    toastr.options.positionClass = 'toast-bottom-right';
  }

  success(message = 'Your action was completed', title = 'Success') {
    toastr.success(message, title);
  }

  warning(message: string, title = 'Warning') {
    toastr.warning(message, title);
  }

  info(message: string, title = 'Information') {
    toastr.info(message, title);
  }

  danger(message: string, title = 'Error') {
    toastr.error(message, title);
  }

  confirm(message: string, title = 'Are you sure ?') {
    toastr.options = {
      'closeButton': false,
      'progressBar': false,
      'positionClass': 'toast-top-center',
      'onclick': null,
      'showDuration': '300',
      'hideDuration': '1000',
      'timeOut': 0,
      'extendedTimeOut': 0,
      'showEasing': 'linear',
      'hideEasing': 'linear',
      'showMethod': 'fadeIn',
      'hideMethod': 'fadeOut',
      'tapToDismiss': false
    };
    const vm = this;
    const bts = `<span class="buttons"> <a class="button is-small is-outlined" onclick="vm.resultFn(false)">Cancel</a>
    <a class="button is-primary is-small is-outlined" onclick="vm.resultFn(true)">Confirm</a></span>`;
    toastr.success(message + ' <br /> ' + bts, title);
  }
  resultFn(b: boolean): boolean {
    toastr.clear();
    return b;
  }

  closeAll() {
    toastr.clear();
  }
}
