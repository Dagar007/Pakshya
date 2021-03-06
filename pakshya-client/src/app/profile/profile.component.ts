import { Component, OnInit } from '@angular/core';
import { ProfileService } from './profile.service';
import { ActivatedRoute,  Params } from '@angular/router';
import { AlertifyService } from '../core/services/alertify.service';
import { AuthService } from '../auth/auth.service';
import { IProfile } from '../shared/_models/profile';
import { Observable } from 'rxjs';
import { IUser } from '../shared/_models/user';

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
  profile: IProfile;
  currentUser$: Observable<IUser>;
  id = '';

  ngOnInit() {

    this.route.params.subscribe((params: Params) => {
      this.authService.loggedInUser$.subscribe((user: IUser) => {
        if (user.id === params['id']) {
          this.profileService.setCanEdit(true);
        } else {
          this.profileService.setCanEdit(false);
        }
        this.profileService.get(params['id']).subscribe((profile: IProfile) => {
          this.profile = profile;
          this.profileService.changeProfilePhoto(this.profile.image);
          this.profileService.setFollowers(this.profile.followers);
          this.profileService.setFollowings(this.profile.followings);
          this.profileService.changeId(profile.id);
        },
        err => {
          this.alertify.error(err);
        });
      });
    });
  }
}
