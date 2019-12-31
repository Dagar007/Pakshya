import { Component, OnInit, Input } from '@angular/core';
import { IProfile } from 'src/app/_models/profile';
import { ProfileService } from 'src/app/_services/profile.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

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
    this.profileService.updateBio(this.profile).subscribe(()=> {
      this.bioEdit = false;
      this.alertifyService.success("Information updated successfully.");
    }, err => {
      this.alertifyService.error("Error updading personal information." + err);
    })
  }

}
