import { Component, OnInit, Input } from '@angular/core';
import { ProfileService } from 'src/app/_services/profile.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.scss']
})
export class ProfileHeaderComponent implements OnInit {
  @Input() profile: string;
  @Input() edit : boolean;
  
  photoUrl: string;
  constructor(private profileService: ProfileService, 
    private alertify: AlertifyService, private authService: AuthService) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe((photoURL) => {
      this.photoUrl = photoURL;
    });
   
  }
  
}
