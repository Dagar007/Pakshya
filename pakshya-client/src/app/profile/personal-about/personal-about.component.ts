import {Component,Input} from "@angular/core";
import { IProfile} from "src/app/_models/profile";

@Component({
  selector: "app-personal-about",
  templateUrl: "./personal-about.component.html",
  styleUrls: ["./personal-about.component.scss"]
})
export class PersonalAboutComponent {

  visible = true;
  @Input() profile: IProfile;
  @Input() edit: boolean;
  constructor() {}
}
