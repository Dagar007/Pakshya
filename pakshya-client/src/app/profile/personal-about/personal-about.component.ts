import {Component, Input, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IProfile } from 'src/app/shared/_models/profile';

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
    this.createAboutForm();
  }
  constructor(private fb: FormBuilder) {}

  createAboutForm() {
    this.aboutForm = this.fb.group({
      bioForm: this.fb.group({
        displayName: [null, Validators.required],
        education: [null, Validators.required],
        work: [null, Validators.required],
        bio: [null, Validators.required]
      }),
      interestsForm: this.fb.group({
        interests: [null, Validators.required]
      })
    });
  }
}
