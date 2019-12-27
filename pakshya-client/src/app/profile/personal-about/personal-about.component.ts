import { COMMA, ENTER } from "@angular/cdk/keycodes";
import {
  Component,
  ElementRef,
  ViewChild,
  OnInit,
  Input,
  Output,
  EventEmitter
} from "@angular/core";
import { FormControl, FormGroup, FormBuilder } from "@angular/forms";
import {
  MatAutocompleteSelectedEvent,
  MatAutocomplete
} from "@angular/material/autocomplete";
import { MatChipInputEvent } from "@angular/material/chips";
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";
import { IProfile, IPhoto } from "src/app/_models/profile";
import { ProfileService } from "src/app/_services/profile.service";
import { AlertifyService } from "src/app/_services/alertify.service";
import { AuthService } from "src/app/_services/auth.service";

@Component({
  selector: "app-personal-about",
  templateUrl: "./personal-about.component.html",
  styleUrls: ["./personal-about.component.scss"]
})
export class PersonalAboutComponent implements OnInit {
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  fruitCtrl = new FormControl({ disabled: true });
  filteredFruits: Observable<string[]>;
  fruits: string[] = ["Lemon"];
  allFruits: string[] = ["Apple", "Lemon", "Lime", "Orange", "Strawberry"];
  uploadForm: FormGroup;

  @ViewChild("fruitInput", { static: false }) fruitInput: ElementRef<
    HTMLInputElement
  >;
  @ViewChild("auto", { static: false }) matAutocomplete: MatAutocomplete;
  @Input() profile: IProfile;
  @Input() edit: boolean;
  currentMainPhoto: IPhoto;

  constructor(
    private profileService: ProfileService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {
    this.filteredFruits = this.fruitCtrl.valueChanges.pipe(
      startWith(null),
      map((fruit: string | null) =>
        fruit ? this._filter(fruit) : this.allFruits.slice()
      )
    );
  }

  ngOnInit() {
    this.uploadForm = this.formBuilder.group({
      profile: [""]
    });
  }

  add(event: MatChipInputEvent): void {
    // Add fruit only when MatAutocomplete is not open
    // To make sure this does not conflict with OptionSelected Event
    if (!this.matAutocomplete.isOpen) {
      const input = event.input;
      const value = event.value;

      // Add our fruit
      if ((value || "").trim()) {
        this.fruits.push(value.trim());
      }

      // Reset the input value
      if (input) {
        input.value = "";
      }

      this.fruitCtrl.setValue(null);
    }
  }

  remove(fruit: string): void {
    const index = this.fruits.indexOf(fruit);

    if (index >= 0) {
      this.fruits.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.fruits.push(event.option.viewValue);
    this.fruitInput.nativeElement.value = "";
    this.fruitCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allFruits.filter(
      fruit => fruit.toLowerCase().indexOf(filterValue) === 0
    );
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
      this.profile.photos.push(res);
      this.alertify.success("Photo Uploaded Successfully.");
    });
  }
}
