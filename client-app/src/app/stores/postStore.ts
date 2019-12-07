import { observable, action, computed, configure, runInAction } from "mobx";
import { createContext, SyntheticEvent } from "react";
import { IPost } from "../models/post";
import agent from "../api/agent";

configure({enforceActions: "always"})

export class PostStore {
  @observable postRegistry = new Map();
  @observable posts: IPost[] = [];
  @observable selectedPost: IPost | undefined;
  @observable loadingInitial = false;
  @observable editMode = false;
  @observable submitting = false;
  @observable target = "";

  @computed get postByDate() {
    return Array.from(this.postRegistry.values()).sort(
      (a, b) => Date.parse(a.date) - Date.parse(b.date)
    );
  }

  @action loadPosts = async () => {
    this.loadingInitial = true;
    try {
      const posts = await agent.Posts.list();
      runInAction("loading posts", () => {
        posts.forEach(post => {
          post.date = post.date.split("T")[0];
          this.postRegistry.set(post.id, post);
        });
        this.loadingInitial = false;
      })  
    } catch (error) {
      runInAction("loading post error", () => {
        this.loadingInitial = false;
      })
      console.log(error);
    }
  };

  @action createPost = async (post: IPost) => {
    this.submitting = true;
    try {
      await agent.Posts.create(post);
      runInAction("creating post", () => {
        this.postRegistry.set(post.id, post);
        this.editMode = false;
        this.submitting = false;
      });
     
    } catch (error) {
      runInAction("create post error", () => {
        this.submitting = false;
      });
      console.log(error);
    }
  };

  @action editPost = async (post: IPost) => {
    this.submitting = true;
    try {
      await agent.Posts.update(post);
      runInAction("editting post", () => {
        this.postRegistry.set(post.id, post);
        this.selectedPost = post;
        this.editMode = false;
        this.submitting = false;
      });
    
    } catch (error) {
      runInAction("edit post error", () => {
        this.submitting = false;
      })
      console.log(error);
    }
  };

  @action deletePost = async (
    event: SyntheticEvent<HTMLButtonElement>,
    id: string
  ) => {
    this.submitting = true;
    this.target = event.currentTarget.name;
    try {
      await agent.Posts.delete(id);
      runInAction("deleting post",() => {
        this.postRegistry.delete(id);
        this.submitting = false;
        this.target = '';
      });
    } catch (error) {
      runInAction("edit post error", () => {
        this.submitting = false;
        this.target = '';
      });
      console.log(error);
    }
  };

  @action openCreateForm = () => {
    this.editMode = true;
    this.selectedPost = undefined;
  };

  @action openEditForm = (id: string) => {
    this.selectedPost = this.postRegistry.get(id);
    this.editMode = true;
  };

  @action cancelSelectedPost = () => {
    this.selectedPost = undefined;
  };

  @action cancelFormOpen = () => {
    this.editMode = false;
  };

  @action selectPost = (id: string) => {
    this.selectedPost = this.postRegistry.get(id);
    this.editMode = false;
  };
}
export default createContext(new PostStore());
