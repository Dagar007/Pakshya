import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { IPost, IComment } from 'src/app/_models/post';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel
} from "@microsoft/signalr";
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { CommentService } from 'src/app/_services/comment.service';


@Component({
  selector: 'app-post-delails-comments',
  templateUrl: './post-delails-comments.component.html',
  styleUrls: ['./post-delails-comments.component.scss']
})
export class PostDelailsCommentsComponent implements OnInit, OnDestroy {

  @Input() post: IPost;
  formatedDate:string;
  comment: string;
  commentToPost: any;
  currentUserName: string;
  private _hubConnection: HubConnection;
  constructor(private alertify: AlertifyService, private authService: AuthService, private commentService: CommentService) { }

  type:string = 'for'

  ngOnInit() {
    if (this.authService.currentUser1)
    this.currentUserName = this.authService.currentUser1.username;
    this.createHubConnection();
  }

  createHubConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/chat", {
        accessTokenFactory: () => localStorage.getItem("token")
      })
      .build();

    this._hubConnection
      .start()
      .then(() => {
        if(this.post)
          this._hubConnection!.invoke('AddToGroup', this.post.id)
      })
      .catch(err => this.alertify.error(err));

    this._hubConnection.on("ReceiveComment", (comment: IComment) => {
      this.post.comments.push(comment);
    });
  }

  stopHubConnection() {
    if (this._hubConnection) {
      this._hubConnection.invoke('RemoveFromGroup', this.post.id).then(() => {
        this._hubConnection.stop();
      })
    }  
  }

  ngOnDestroy(): void {
    this.stopHubConnection();
  }

  addComment() {
    if(this.comment== null || this.comment== ''){
      this.alertify.error("Please enter your comment");
    } else {
      this.commentToPost = {
        postId: this.post.id,
        body : this.comment,
        for : this.type=='for'? true: false,
        against: this.type=='against'? true: false,
      }
      this._hubConnection.invoke('SendComment', this.commentToPost)
      this.comment = '';
    }
    
    
  }

  onReply(displayName: string ){
    this.comment = '';
    this.comment = '@'+ displayName;
  }

  commentDelete(comment: IComment) {
    this.commentService.deleteComment(comment.id).subscribe(()=> {
      var index = this.post.comments.indexOf(comment);
      if(index > -1) {
        this.post.comments.splice(index, 1);
      }
      this.alertify.success("comment deleted successfully");
    }, err => {
      this.alertify.error("error deleting comment.");
    })
  }
  likeComment(comment: IComment) {
    if(comment.isLikedByUser) {
      this.commentService.unlikeComment(comment.id).subscribe(() => {
        --comment.liked;
        comment.isLikedByUser = false;
      })
    } else {
      this.commentService.likeComment(comment.id).subscribe(() => {
        ++comment.liked;
        comment.isLikedByUser = true;
      })
    }
  }
}
