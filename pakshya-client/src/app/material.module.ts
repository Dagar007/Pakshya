import {
  MatToolbarModule,
  MatButtonModule,
  MatCardModule,
  MatChipsModule,
  MatRippleModule,
  MatIconModule,
  MatInputModule,
  MatTooltipModule
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
  MatTooltipModule
  ],
  exports: [
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatChipsModule,
    MatIconModule,
    MatRippleModule,
    MatInputModule,
  MatTooltipModule
  ]
})
export class MaterialModule {}
