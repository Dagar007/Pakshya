import {
  MatToolbarModule,
  MatButtonModule,
  MatCardModule,
  MatChipsModule,
  MatRippleModule,
  MatIconModule,
  MatInputModule,
  MatTooltipModule,
  MatListModule,
  MatDividerModule,
  MatCheckboxModule,
  MatRadioModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatMenuModule
} from '@angular/material';
import { NgModule } from '@angular/core';

@NgModule({
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatChipsModule,
    MatIconModule,
    MatRippleModule,
    MatInputModule,
    MatTooltipModule,
    MatListModule,
    MatDividerModule,
    MatCheckboxModule,
    MatRadioModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatMenuModule
  ],
  exports: [
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatChipsModule,
    MatIconModule,
    MatRippleModule,
    MatInputModule,
    MatTooltipModule,
    MatListModule,
    MatDividerModule,
    MatCheckboxModule,
    MatRadioModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatMenuModule
  ]
})
export class MaterialModule {}
