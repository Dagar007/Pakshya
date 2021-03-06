import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';

import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel
} from '@microsoft/signalr';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { AuthService } from 'src/app/auth/auth.service';
import { CommentService } from 'src/app/post/comment.service';
import { environment } from 'src/environments/environment';
import { ActivatedRoute } from '@angular/router';
import { IComment } from 'src/app/shared/_models/post';
import { Pagination, PaginatedResult } from 'src/app/shared/_models/pagination';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/_models/user';


@Component({
  selector: 'app-post-delails-comments',
  templateUrl: './post-delails-comments.component.html',
  styleUrls: ['./post-delails-comments.component.scss']
})
export class PostDelailsCommentsComponent implements OnInit, OnDestroy {

  @Output() commentsModified = new EventEmitter<string>();
  allComments: IComment[];
  formatedDate: string;
  comment: string;
  commentToPost: any;
  loggedInUser$: Observable<IUser>;
  private _hubConnection: HubConnection;
  baseUrl = environment.chatUrl;

  postId: string;

  userParams: any = {};
  pagination: Pagination;

  constructor(private alertify: AlertifyService,
    private authService: AuthService,
    private commentService: CommentService,
    private route: ActivatedRoute) { }


  type = 'support';

  ngOnInit() {
    // if (this.authService.currentUser$) {

    // this.currentUserName = this.authService.currentUser$.username;
    // }
    this.loggedInUser$ = this.authService.loggedInUser$;

    this.route.params.subscribe(params => {
      if (params['id']) {
        this.postId = params['id'];
      }
    });
    this.createHubConnection();

    // this.route.data.subscribe(data => {
    //   this.pagination = data['comments'].pagination;
    //   this.allComments = data['comments'].result;
    // });

    this.commentService.getComments(this.postId).subscribe((res: PaginatedResult<IComment[]>) => {
      this.allComments = res.result;
      console.log(this.allComments);
    });

  }

  createHubConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(this.baseUrl + 'chat', {
        accessTokenFactory: () => localStorage.getItem('token')
      })
      .build();

    this._hubConnection
      .start()
      .then(() => {
        if (this.postId && this._hubConnection) {
          this._hubConnection.invoke('AddToGroup', this.postId);
        }

      })
      .catch(err => this.alertify.error(err));


    this._hubConnection.on('ReceiveComment', (comment: IComment) => {
      this.allComments.unshift(comment);
      this.commentsModified.emit('added');
    });
  }

  stopHubConnection() {
    if (this._hubConnection) {
      this._hubConnection.invoke('RemoveFromGroup', this.postId).then(() => {
        this._hubConnection.stop();
      });
    }
  }

  ngOnDestroy(): void {
    this.stopHubConnection();
  }

  addComment() {
    if (this.comment == null || this.comment === '') {
      this.alertify.error('Please enter your comment');
    } else {
      this.commentToPost = {
        postId: this.postId,
        body : this.comment,
        support : this.type === 'support' ? true : false,
        against: this.type === 'against' ? true : false,
      };
      this._hubConnection.invoke('SendComment', this.commentToPost);
      this.comment = '';
    }


  }

  onReply(displayName: string ) {
    this.comment = '';
    this.comment = '@' + displayName;
  }

  commentDelete(comment: IComment) {
    this.commentService.deleteComment(comment.id).subscribe(() => {
      this.commentsModified.emit('deleted');
      const index = this.allComments.indexOf(comment);
      if (index > -1) {
        this.allComments.splice(index, 1);
      }
      this.alertify.success('comment deleted successfully');
    }, err => {
      this.alertify.error('error deleting comment.');
    });
  }
  likeComment(comment: IComment) {
    if (comment.hasLoggedInUserLiked) {
      this.commentService.unlikeComment(comment.id).subscribe(() => {
        --comment.totalLikes;
        comment.hasLoggedInUserLiked = false;
      }, err => {
        this.alertify.error(err);
      });
    } else {
      this.commentService.likeComment(comment.id).subscribe(() => {
        ++comment.totalLikes;
        comment.hasLoggedInUserLiked = true;
      }, err => {
        this.alertify.error(err);
      });
    }
  }

  seeMore() {
    this.pagination.currentPage++;
    this.commentService.getComments(this.postId, this.pagination.currentPage,
      this.pagination.itemsPerPage).subscribe((res: PaginatedResult<IComment[]>) => {
      this.pagination = res.pagination;
      Array.prototype.push.apply(this.allComments, res.result);
    });
  }
}
