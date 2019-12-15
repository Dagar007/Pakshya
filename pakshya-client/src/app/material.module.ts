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
  MatDividerModule
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
  MatDividerModule
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
  MatDividerModule
  ]
})
export class MaterialModule {}
