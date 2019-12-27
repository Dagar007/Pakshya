import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalAboutComponent } from './personal-about.component';

describe('PersonalAboutComponent', () => {
  let component: PersonalAboutComponent;
  let fixture: ComponentFixture<PersonalAboutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonalAboutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonalAboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
