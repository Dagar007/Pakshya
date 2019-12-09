import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PostDelailsCommentsComponent } from './post-delails-comments.component';

describe('PostDelailsCommentsComponent', () => {
  let component: PostDelailsCommentsComponent;
  let fixture: ComponentFixture<PostDelailsCommentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostDelailsCommentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostDelailsCommentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
