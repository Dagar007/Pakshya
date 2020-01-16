import { COMMA, ENTER } from "@angular/cdk/keycodes";
import { Component, OnInit, Input } from "@angular/core";
import { PostService } from "src/app/_services/post.service";
import { ICategory } from "src/app/_models/post";
import { FormBuilder, FormGroup, FormArray, FormControl } from "@angular/forms";
import { ProfileService } from "src/app/_services/profile.service";
import { IProfile } from "src/app/_models/profile";
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: "app-interests",
  templateUrl: "./interests.component.html",
  styleUrls: ["./interests.component.scss"]
})
export class InterestsComponent implements OnInit {
  @Input() profile: IProfile;
  @Input() edit: boolean;
  categories: ICategory[];
  interestsOfUsers: ICategory[];
  

  constructor(
    private alertify: AlertifyService,
    private profileService: ProfileService,
  ) {

  }
  ngOnInit() {
    };

  onSubmit(){
    //console.log(this.profile.interests);
    this.profileService.updateBio(this.profile).subscribe(()=> {
      this.alertify.success("Interests updated successfully.")
    }, err =>{
      this.alertify.error("error updating interests");
    });
  };

}
