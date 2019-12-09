import { observable, action, computed, configure, runInAction } from "mobx";
import { createContext, SyntheticEvent } from "react";
import { IPost } from "../models/post";
import agent from "../api/agent";

configure({ enforceActions: "always" });

export class PostStore {
  @observable postRegistry = new Map();
  @observable post: IPost | null = null
  @observable loadingInitial = false;
  @observable submitting = false;
  @observable target = "";

  @computed get postByDate() {
    return this.groupPostsByDate(Array.from(this.postRegistry.values()))
  }

  groupPostsByDate(posts: IPost[]) {
    const sortedPost = posts.sort(
      (a, b) => Date.parse(a.date) - Date.parse(b.date)
    )
    return Object.entries(sortedPost.reduce((posts, post) => {
      const date = post.date.split('T')[0];
      posts[date] = posts[date] ? [...posts[date], post] : [post]
      return posts;
    }, {} as {[key: string]: IPost[]}))
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
      });
    } catch (error) {
      runInAction("loading post error", () => {
        this.loadingInitial = false;
      });
      console.log(error);
    }
  };

  @action loadPost = async (id: string) => {
    let post = this.getPost(id);
    if (post) {
      this.post = post;
    } else {
      this.loadingInitial = true;
      try {
        post = await agent.Posts.details(id);
        runInAction("Getting Post", () => {
          this.post = post;
          this.loadingInitial = false;
        });
      } catch (error) {
        runInAction("Get Post Error", () => {
          this.loadingInitial = false;
        });
        console.log(error);
      }
    }
  };

  @action clearPost = () =>{
    this.post = null;
  }

  getPost = (id: string) => {
    return this.postRegistry.get(id);
  };

  @action createPost = async (post: IPost) => {
    this.submitting = true;
    try {
      await agent.Posts.create(post);
      runInAction("creating post", () => {
        this.postRegistry.set(post.id, post);
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
        this.post = post;
        this.submitting = false;
      });
    } catch (error) {
      runInAction("edit post error", () => {
        this.submitting = false;
      });
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
      runInAction("deleting post", () => {
        this.postRegistry.delete(id);
        this.submitting = false;
        this.target = "";
      });
    } catch (error) {
      runInAction("edit post error", () => {
        this.submitting = false;
        this.target = "";
      });
      console.log(error);
    }
  };
}
export default createContext(new PostStore());
