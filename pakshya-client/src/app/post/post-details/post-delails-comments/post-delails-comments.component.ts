import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { IPost, IComment } from 'src/app/_models/post';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel
} from "@microsoft/signalr";


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
  constructor() { }

  ngOnInit() {
    this.createHubConnection();
  }

  createHubConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/chat", {
        accessTokenFactory: () => localStorage.getItem("token")
      })
      .configureLogging(LogLevel.Information)
      .build();

    this._hubConnection
      .start()
      .then(() => console.log(this._hubConnection!.start))
      .catch(err => console.log(err));

    this._hubConnection.on("ReceiveComment", (comment: IComment) => {
      //comment.formatedDate = formatDistance(comment.date,new Date());
      this.post.comments.push(comment);
    });
  }

  stopHubConnection() {
    if (this._hubConnection) 
      this._hubConnection.stop();
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
