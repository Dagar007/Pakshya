import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { IPost, IComment } from 'src/app/_models/post';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel
} from "@microsoft/signalr";
import { AlertifyService } from 'src/app/_services/alertify.service';


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
  private _hubConnection: HubConnection;
  constructor(private alertify: AlertifyService) { }

  ngOnInit() {
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
    this.commentToPost = {
      postId: this.post.id,
      body : this.comment,
      for : false,
      against: true,
    }
    this._hubConnection.invoke('SendComment', this.commentToPost)
    this.comment = '';
  }

  onReply(displayName: string ){
    this.comment = '';
    this.comment = '@'+ displayName;
  }
}
