import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PostDelailsHeaderComponent } from './post-delails-header.component';

describe('PostDelailsHeaderComponent', () => {
  let component: PostDelailsHeaderComponent;
  let fixture: ComponentFixture<PostDelailsHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostDelailsHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostDelailsHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
