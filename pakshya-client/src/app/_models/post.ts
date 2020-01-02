export interface IPostsEnvelope {
  posts: IPost[],
  postCount: number
}

export interface IPost {
  id: string;
  heading: string;
  description: string;
  category: string;
  date: Date;
  url?: string;
  for: number;
  against: number;
  engagers: IEngagers[];
  comments: IComment[];
}

export interface IEngagers {
  username: string;
  displayName: string;
  image: string;
  isAuthor: boolean;
  // following: boolean
}
export interface IComment {
  postId: string;
  body: string;
  for: boolean;
  against: boolean;
  liked: number;
  date: Date;
  username: string;
  displayName: string;
  image: string;
}
