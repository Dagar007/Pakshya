import {
  Component,
  OnInit,
  Input
} from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { ProfileService } from 'src/app/profile/profile.service';
import { IFollow, IProfile } from 'src/app/shared/_models/profile';
import { IUser } from 'src/app/shared/_models/user';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.scss']
})
export class ProfileHeaderComponent implements OnInit {
  @Input() profile: IProfile;
  edit$: Observable<boolean>;

  currentUser$: Observable<IUser>;
  currentUser: IUser;
  currentFollower: IFollow;

  profilePhoto$: Observable<string>;

  constructor(
    private authService: AuthService,
    private profileService: ProfileService
  ) {}

  ngOnInit() {
      this.authService.loggedInUser$.subscribe((user: IUser) => {
        this.currentUser = user;
      }, err => {
        console.log(err);
      });
    this.profilePhoto$ = this.profileService.profilePhoto$;
     this.profileService.setFollowers(this.profile.followers);
     this.profileService.setFollowings(this.profile.followings);
     this.edit$ = this.profileService.canEdit$;
     this.currentFollower = {
       id: this.currentUser.id,
       displayName: this.currentUser.displayName,
       url: this.currentUser.image
     };
  }

  // ngOnChanges(changes: SimpleChanges): void {
  //   if (changes.profile) {
  //     if (this.currentUser.id === changes.profile.currentValue.id) {
  //       this.authService.currentPhotoUrl.subscribe(photoURL => {
  //         this.photoUrl = photoURL;
  //       });
  //     } else {
  //       this.photoUrl = changes.profile.currentValue.image;
  //     }
  //   }

  // }

  onFollow(following: boolean, id: string) {
    if (following) {
      this.profileService.unfollow(id).subscribe(() => {
        this.profile.following = false;
        --this.profile.followersCount;
        this.profileService.setFollowers(this.profile.followers.filter(x => x.id !== id));
      });
    } else {
      this.profileService.follow(id).subscribe(() => {
        this.profile.following = true;
        ++this.profile.followersCount;
        this.profileService.setFollowers(this.profile.followers.concat(this.currentFollower));
      });
    }
  }

}
