import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../_services/profile.service';
import { ActivatedRoute,  Params } from '@angular/router';
import { IProfile } from '../_models/profile';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

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

    this.route.params.subscribe((params: Params) => {
      // tslint:disable-next-line: triple-equals
      if (this.authService.currentUser1.username == params['username']) {
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
