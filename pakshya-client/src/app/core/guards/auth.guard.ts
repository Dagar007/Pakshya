import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import {MatDialog } from '@angular/material/dialog';
import { AuthService } from 'src/app/auth/auth.service';
import { AlertifyService } from '../services/alertify.service';
import { LoginPopupComponent } from '../../shared/components/login-popup/login-popup.component';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService,
    public dialog: MatDialog
  ) {}
  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    } else {
      this.openDialog();
      return false;
    }
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(LoginPopupComponent, {
      width: '600px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}
