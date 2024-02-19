import  { MatToolbarModule }   from '@angular/material/toolbar';
import  { MatButtonModule }   from '@angular/material/button';
import  { MatCardModule }   from '@angular/material/card';
import  { MatChipsModule }   from '@angular/material/chips';
import  { MatRippleModule, MatNativeDateModule } from '@angular/material/core';
import  { MatIconModule }   from '@angular/material/icon';
import  { MatAutocompleteModule }   from '@angular/material/autocomplete';
import  { MatInputModule }   from '@angular/material/input';
import  { MatTooltipModule }   from '@angular/material/tooltip';
import  { MatListModule }   from '@angular/material/list';
import  { MatDividerModule }   from '@angular/material/divider';
import  { MatCheckboxModule }   from '@angular/material/checkbox';
import  { MatRadioModule }   from '@angular/material/radio';
import  { MatDatepickerModule }   from '@angular/material/datepicker';
import  { MatMenuModule }   from '@angular/material/menu';
import  { MatTabsModule }   from '@angular/material/tabs';
import  { MatProgressSpinnerModule }   from '@angular/material/progress-spinner';
import  { MatSelectModule }   from '@angular/material/select';
import  { MatBadgeModule }   from '@angular/material/badge';
import  { MatDialogModule }   from '@angular/material/dialog';

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
    MatMenuModule,
    MatAutocompleteModule,
    MatTabsModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatBadgeModule,
    MatDialogModule
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
    MatMenuModule,
    MatAutocompleteModule,
    MatTabsModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatBadgeModule,
    MatDialogModule
  ]
})
export class MaterialModule {}
