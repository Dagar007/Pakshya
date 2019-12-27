import { Component, OnInit } from "@angular/core";
import { ProfileService } from "../_services/profile.service";
import { ActivatedRoute, ParamMap, Params } from "@angular/router";
import { IProfile } from "../_models/profile";
import { switchMap } from "rxjs/operators";
import { AlertifyService } from "../_services/alertify.service";
import { AuthService } from "../_services/auth.service";

@Component({
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.scss"]
})
export class ProfileComponent implements OnInit {
 
  constructor(
    private profileService: ProfileService,
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  userFromParams: string;
  edit: boolean = false;
  profile: IProfile;

  ngOnInit() {
   
    this.route.params.subscribe((params: Params) => {
      if (this.authService.currentUser1.username == params["username"]) {
        this.edit = true;
      }
    });

    this.route.paramMap
      .pipe(
        switchMap((params: ParamMap) =>
          this.profileService.get(params.get("username"))
        )
      )
      .subscribe(
        (profile: IProfile) => {
          this.profile = profile;
        },
        err => {
          this.alertify.error(err);
        }
      );
  }


}
