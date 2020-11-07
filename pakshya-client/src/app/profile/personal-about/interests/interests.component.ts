import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, OnInit, Input } from '@angular/core';
import { PostService } from 'src/app/post/post.service';

import { FormBuilder, FormGroup, FormArray, FormControl } from '@angular/forms';
import { ProfileService } from 'src/app/profile/profile.service';

import { AlertifyService } from 'src/app/core/services/alertify.service';
import { IProfile } from 'src/app/shared/_models/profile';
import { ICategory } from 'src/app/shared/_models/post';

@Component({
  selector: 'app-interests',
  templateUrl: './interests.component.html',
  styleUrls: ['./interests.component.scss']
})
export class InterestsComponent implements OnInit {
  @Input() profile: IProfile;
  @Input() edit: boolean;
  categories: ICategory[];
  interestsOfUsers: ICategory[];
  allInterests: ICategory[];


  constructor(
    private alertify: AlertifyService,
    private profileService: ProfileService,
  ) {}
  ngOnInit() {
    this.getAllInterests();
  }

  onSubmit() {
    this.profileService.updateBio(this.profile).subscribe(() => {
      this.alertify.success('Interests updated successfully.');
    }, err => {
      this.alertify.error('error updating interests');
    });
  }
  getAllInterests() {
    this.profileService.getAllInterests().subscribe((allInterests: ICategory[]) => {
      this.allInterests = allInterests;
    }, err => {
      this.alertify.error('Error occured while fetching the interests');
    });
  }
}
