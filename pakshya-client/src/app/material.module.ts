import {MatToolbarModule, MatButtonModule, MatCardModule, MatChipsModule, MatRippleModule, MatIconModule} from '@angular/material';
import { NgModule } from '@angular/core';

@NgModule({
  imports: [MatToolbarModule, MatButtonModule, MatCardModule, MatChipsModule, MatIconModule, MatRippleModule],
  exports: [MatToolbarModule, MatButtonModule, MatCardModule, MatChipsModule, MatIconModule, MatRippleModule]
})
export class MaterialModule { }
