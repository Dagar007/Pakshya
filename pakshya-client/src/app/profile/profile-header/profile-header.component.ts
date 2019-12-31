import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges
} from "@angular/core";
import { AuthService } from "src/app/_services/auth.service";
import { IProfile } from "src/app/_models/profile";

@Component({
  selector: "app-profile-header",
  templateUrl: "./profile-header.component.html",
  styleUrls: ["./profile-header.component.scss"]
})
export class ProfileHeaderComponent implements OnInit, OnChanges {
  @Input() profile: IProfile;
  @Input() edit: boolean;

  photoUrl: string;
  constructor(
    private authService: AuthService
  ) {}

  ngOnInit() {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.profile) {
      if (this.authService.currentUser1.username == changes.profile.currentValue.username) {
        this.authService.currentPhotoUrl.subscribe(photoURL => {
          this.photoUrl = photoURL;
        });
      } else {
        this.photoUrl = changes.profile.currentValue.image;
      }
    }
  }
}
