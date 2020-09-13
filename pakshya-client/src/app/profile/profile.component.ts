import { Component, OnInit } from '@angular/core';
import { ProfileService } from './profile.service';
import { ActivatedRoute,  Params } from '@angular/router';
import { AlertifyService } from '../core/services/alertify.service';
import { AuthService } from '../auth/auth.service';
import { IProfile } from '../shared/_models/profile';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(
    private profileService: ProfileService,
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  userFromParams: string;
  edit = false;
  profile: IProfile;

  ngOnInit() {
    console.log('here');
    this.route.params.subscribe((params: Params) => {
      if (this.authService.currentUser1.username === params['username']) {
        this.edit = true;
      }
      this.profileService.get(params['username']).subscribe((profile: IProfile) => {
        this.profile = profile;
      },
      err => {
        this.alertify.error(err);
      });
    });
  }


}
