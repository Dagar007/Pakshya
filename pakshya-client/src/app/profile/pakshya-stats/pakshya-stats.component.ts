import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';

import { ProfileService } from 'src/app/profile/profile.service';
import { IFollow, IProfile } from 'src/app/shared/_models/profile';

@Component({
  selector: 'app-pakshya-stats',
  templateUrl: './pakshya-stats.component.html',
  styleUrls: ['./pakshya-stats.component.scss']
})
export class PakshyaStatsComponent implements OnInit {

  @Input() profile: IProfile;

  constructor(private profileService: ProfileService) { }
  followings$: Observable<IFollow[]>;
  followers$: Observable<IFollow[]>;
  ngOnInit() {
    this.followings$ = this.profileService.followings$;
    this.followers$ = this.profileService.followers$;
  }

}
