import {Component, Input, OnInit} from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ICategory } from 'src/app/shared/_models/post';
import { IProfile } from 'src/app/shared/_models/profile';
import { ProfileService } from '../profile.service';

@Component({
  selector: 'app-personal-about',
  templateUrl: './personal-about.component.html',
  styleUrls: ['./personal-about.component.scss']
})
export class PersonalAboutComponent implements OnInit {

  visible = true;
  @Input() profile: IProfile;
  @Input() edit: boolean;
  aboutForm: FormGroup;

  ngOnInit() {
    // this.createAboutForm();
  }
  constructor(private fb: FormBuilder, private profileService: ProfileService) {}

  // createAboutForm() {
  //   this.aboutForm = this.fb.group({
  //     bioForm: this.fb.group({
  //       displayName: [null, Validators.required],
  //       education: [null, Validators.required],
  //       work: [null, Validators.required],
  //       bio: [null, Validators.required]
  //     }),
  //     interestsForm: this.fb.group({
  //       interests: this.fb.array([]);
  //     })
  //   });
  // }
}
