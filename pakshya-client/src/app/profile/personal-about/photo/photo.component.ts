import { Component, OnInit, Input } from "@angular/core";
import { IProfile, IPhoto } from "src/app/_models/profile";
import { ProfileService } from "src/app/_services/profile.service";
import { AlertifyService } from "src/app/_services/alertify.service";
import { AuthService } from "src/app/_services/auth.service";
import { FormBuilder, FormGroup } from "@angular/forms";

@Component({
  selector: "app-photo",
  templateUrl: "./photo.component.html",
  styleUrls: ["./photo.component.scss"]
})
export class PhotoComponent implements OnInit {
  @Input() profile: IProfile;
  @Input() edit: boolean;
  uploadForm: FormGroup;
  currentMainPhoto: IPhoto;

  constructor(
    private profileService: ProfileService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.uploadForm = this.formBuilder.group({
      profile: [""]
    });
  }

  setMainPhoto(photo: IPhoto) {
    this.profileService.setMainPhoto(photo.id).subscribe(
      () => {
        this.currentMainPhoto = this.profile.photos.filter(
          p => p.isMain === true
        )[0];
        this.currentMainPhoto.isMain = false;
        photo.isMain = true;
        this.authService.changeMainPhoto(photo.url);
        this.authService.currentUser1.image = photo.url;
        localStorage.setItem(
          "user",
          JSON.stringify(this.authService.currentUser1)
        );

        this.alertify.success("Photo set as main photo");
      },
      err => {
        this.alertify.error("Error setting photo as main Photo." + err);
      }
    );
  }

  deletePhoto(id: string) {
    this.alertify.confirm("Are you sure you want to delete this photo?", () => {
      this.profileService.deletePhoto(id).subscribe(
        () => {
          this.profile.photos.splice(
            this.profile.photos.findIndex(p => p.id === id),
            1
          );
          this.alertify.success("Photo deleted successfully.");
        },
        err => {
          this.alertify.error("Error deleting photo" + err);
        }
      );
    });
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.uploadForm.get("profile").setValue(file);
    }
    const formData = new FormData();
    formData.append("file", this.uploadForm.get("profile").value);
    this.profileService.uploadPhoto(formData).subscribe((res: IPhoto) => {
      if (this.profile.photos.length == 0) {
        res.isMain = true;
        this.authService.changeMainPhoto(res.url);
        this.authService.currentUser1.image = res.url;
        localStorage.setItem(
          "user",
          JSON.stringify(this.authService.currentUser1)
        );
      }
      this.profile.photos.push(res);
      this.alertify.success("Photo Uploaded Successfully.");
    });
  }
}
