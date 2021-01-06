import { Component, OnInit, Input } from '@angular/core';
import { ProfileService } from 'src/app/profile/profile.service';

import { AlertifyService } from 'src/app/core/services/alertify.service';
import { IProfile } from 'src/app/shared/_models/profile';
import { ICategory } from 'src/app/shared/_models/post';
import { IUser } from 'src/app/shared/_models/user';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-interests',
  templateUrl: './interests.component.html',
  styleUrls: ['./interests.component.scss']
})
export class InterestsComponent implements OnInit {
  @Input() profile: IProfile;
  edit: boolean;
  categories: ICategory[];
  interestsOfUsers: string[];
  allInterests: ICategory[];
  id: string;


  constructor(
    private alertify: AlertifyService,
    private profileService: ProfileService,
    private authService: AuthService,
  ) {}
  ngOnInit() {
    this.getAllInterests();
    this.profileService.canEdit$.subscribe((edit: boolean) => {
      this.edit = edit;
    });
    this.profileService.profileId$.subscribe((id: string) => {
      this.id = id;
    });
    this.getUserInterests(this.id);
  }

  updateUserInterests() {
    console.log(this.interestsOfUsers);
    this.profileService.updateUserInterests(this.interestsOfUsers).subscribe(() => { });
  }
  getAllInterests() {
    this.profileService.getAllInterests().subscribe((allInterests: ICategory[]) => {
      this.allInterests = allInterests;
    }, () => {
      this.alertify.error('Error occured while fetching the interests');
    });
  }

  getUserInterests(id: string) {
    console.log(id);
    this.profileService.getUserInterests(id).subscribe((userIntrests: string[]) => {
      this.interestsOfUsers = userIntrests;
    });
  }
}
