import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { UserRole } from './model';
@Injectable({
  providedIn: 'root'
})
export class GmGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router
  ) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.authService.user.userRole === UserRole.Admin || this.authService.user.userRole === UserRole.GM) {
      return true;
    }
    this.router.navigate(['/home']);
    return false;
  }
}
