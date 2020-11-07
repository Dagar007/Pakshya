import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { SharedModule } from '../shared/shared.module';
import { PakshyaInfroComponent } from './pakshya-infro/pakshya-infro.component';



@NgModule({
  declarations: [NavComponent, PakshyaInfroComponent],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    NavComponent,
    PakshyaInfroComponent
  ]
})
export class CoreModule { }
