import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { IPost, ICategory, IPostConcise } from "src/app/_models/post";
import { ActivatedRoute, Router, ParamMap, Params } from "@angular/router";
import { PostService } from "src/app/_services/post.service";
import { switchMap } from "rxjs/operators";
import { v4 as uuid } from "uuid";
import { AlertifyService } from "src/app/_services/alertify.service";

@Component({
  selector: "app-create",
  templateUrl: "./create.component.html",
  styleUrls: ["./create.component.scss"]
})
export class CreateComponent implements OnInit {
  post$: Observable<IPostConcise>;
  categories: ICategory[];
  post: IPostConcise = {
    id: null,
    heading: null,
    description: null,
    date: null,
    url: null,
    category: null,
    hostUsername: null,
    hostDisplayName: null,
    hostImage: null,
    isAuthor: false,
    isPostLiked: false,
    noOfLikes: 0,
    noOfComments: 0
  };
  id: string = "";
  page: string = "Create";
  selectedValue: string = "";
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private postService: PostService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    //const id = this.route.snapshot.params['id'];
    //console.log(id);
    this.postService.getCategories().subscribe((categories: ICategory[]) => {
      this.categories = categories;
    });
    this.route.params.subscribe((params: Params) => {
      this.id = params["id"];
    });
    if (this.id) {
      this.post$ = this.route.paramMap.pipe(
        switchMap((params: ParamMap) =>
          this.postService.getPost(params.get("id"))
        )
      );
      this.post$.subscribe(
        (post: IPostConcise) => {
          this.post = post;
          this.page = "Edit";
          if (post.category) this.selectedValue = post.category.id;
        },
        err => {
          this.alertify.error(err);
        }
      );
    } else {
      this.page = "Create";
    }
  }
  onCancelCreateEditForm() {
    if (this.id) {
      this.router.navigate(["/posts", this.id]);
    } else {
      this.router.navigate(["/posts"]);
    }
  }
  onSubmit() {
    if (this.id) {
      this.post.date = new Date();
      this.post.category = this.categories.filter(
        c => c.id == this.selectedValue
      )[0];
      this.postService.updatePost(this.post).subscribe(
        () => {
          this.router.navigate(["/posts", this.post.id]);
        },
        err => {
          this.alertify.error(err);
        }
      );
    } else {
      this.post.id = uuid();
      this.post.date = new Date();
      this.post.category = this.categories.filter(
        c => c.id == this.selectedValue
      )[0];
      this.postService.createPost(this.post).subscribe(
        () => {
          this.router.navigate(["/posts", this.post.id]);
        },
        err => {
          this.alertify.error(err);
        }
      );
    }
  }
}
