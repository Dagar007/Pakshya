import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PakshyaStatsComponent } from './pakshya-stats.component';

describe('PakshyaStatsComponent', () => {
  let component: PakshyaStatsComponent;
  let fixture: ComponentFixture<PakshyaStatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PakshyaStatsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PakshyaStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
