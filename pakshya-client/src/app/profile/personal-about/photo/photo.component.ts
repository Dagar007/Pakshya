import { Component, OnInit, Input } from '@angular/core';
import { ProfileService } from 'src/app/profile/profile.service';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { AuthService } from 'src/app/auth/auth.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IProfile, IPhoto } from 'src/app/shared/_models/profile';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/_models/user';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.scss']
})
export class PhotoComponent implements OnInit {
  @Input() profile: IProfile;
  edit$: Observable<boolean>;
  uploadForm: FormGroup;
  currentMainPhoto: IPhoto;
  loggedInUser$: Observable<IUser>;
  currentUser: IUser;

  constructor(
    private profileService: ProfileService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.uploadForm = this.formBuilder.group({
      profile: ['']
    });
    this.authService.loggedInUser$.subscribe((user: IUser) => {
      this.currentUser = user;
    });
    this.edit$ = this.profileService.canEdit$;
  }

  setMainPhoto(photo: IPhoto) {
    this.profileService.setMainPhoto(photo.id).subscribe(
      () => {
        this.currentMainPhoto = this.profile.photos.filter(
          p => p.isMain === true
        )[0];
        this.currentMainPhoto.isMain = false;
        photo.isMain = true;
        this.currentUser.image = photo.url;
        this.authService.changeCurrentUser(this.currentUser);
        this.profileService.changeProfilePhoto(photo.url);
        this.alertify.success('Photo set as main photo');
      },
      err => {
        this.alertify.error('Error setting photo as main Photo.' + err);
      }
    );
  }

  deletePhoto(id: string) {
    this.alertify.confirm('Are you sure you want to delete this photo?', () => {
      this.profileService.deletePhoto(id).subscribe(
        () => {
          this.profile.photos.splice(
            this.profile.photos.findIndex(p => p.id === id),
            1
          );
          this.alertify.success('Photo deleted successfully.');
        },
        err => {
          this.alertify.error('Error deleting photo' + err);
        }
      );
    });
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.uploadForm.get('profile').setValue(file);
    }
    const formData = new FormData();
    formData.append('file', this.uploadForm.get('profile').value);
    console.log(formData);
    this.profileService.uploadPhoto(formData).subscribe((res: IPhoto) => {
      if (this.profile.photos.length === 0) {
        res.isMain = true;
        this.authService.changeMainPhoto(res.url);
        this.profileService.changeProfilePhoto(res.url);
      }
      this.profile.photos.push(res);
      this.alertify.success('Photo Uploaded Successfully.');
    });
  }
}
