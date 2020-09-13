import { Component, OnInit, Input } from '@angular/core';
import { ProfileService } from 'src/app/profile/profile.service';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { IProfile } from 'src/app/shared/_models/profile';

@Component({
  selector: 'app-bio',
  templateUrl: './bio.component.html',
  styleUrls: ['./bio.component.scss']
})
export class BioComponent implements OnInit {

  bioEdit = false;
  @Input() profile: IProfile;
  @Input() edit: boolean;
  constructor(private profileService: ProfileService, private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  onSubmit() {
    this.profileService.updateBio(this.profile).subscribe(() => {
      this.bioEdit = false;
      this.alertifyService.success('Information updated successfully.');
    }, err => {
      this.alertifyService.error('Error updading personal information.' + err);
    });
  }

}
