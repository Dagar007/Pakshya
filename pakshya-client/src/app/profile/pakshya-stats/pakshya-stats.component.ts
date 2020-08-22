import { Component, OnInit, Input } from '@angular/core';
import { IProfile } from 'src/app/_models/profile';
import { ProfileService } from 'src/app/_services/profile.service';

@Component({
  selector: 'app-pakshya-stats',
  templateUrl: './pakshya-stats.component.html',
  styleUrls: ['./pakshya-stats.component.scss']
})
export class PakshyaStatsComponent implements OnInit {

  @Input() profile: IProfile;
  constructor() { }

  ngOnInit() {
  }

}
