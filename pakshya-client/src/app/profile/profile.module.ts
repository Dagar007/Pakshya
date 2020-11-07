import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile.component';
import { ProfileHeaderComponent } from './profile-header/profile-header.component';
import { PersonalAboutComponent } from './personal-about/personal-about.component';
import { PakshyaStatsComponent } from './pakshya-stats/pakshya-stats.component';
import { PhotoComponent } from './personal-about/photo/photo.component';
import { InterestsComponent } from './personal-about/interests/interests.component';
import { BioComponent } from './personal-about/bio/bio.component';
import { SharedModule } from '../shared/shared.module';
import { ProfileRoutingModule } from './profile-routing.module';



@NgModule({
  declarations: [
    ProfileComponent,
    ProfileHeaderComponent,
    PersonalAboutComponent,
    PakshyaStatsComponent,
    PhotoComponent,
    InterestsComponent,
    BioComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ProfileRoutingModule
  ]
})
export class ProfileModule { }
