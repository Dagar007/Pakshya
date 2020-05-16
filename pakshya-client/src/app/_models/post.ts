import { IPhoto } from './profile';


// export interface IPostsEnvelope {
//   posts: IPostConcise[],
//   postCount: number
// }

export interface IPost {
  id: string;
  heading: string;
  description: string;
  category: ICategory;
  date: Date;
  url?: string;
  for: number;
  against: number;
  engagers: IEngagers[];
  comments: IComment[];
}

export interface ICategory {
  id: string;
  value: string;
  doesUser : boolean
}
export interface IEngagers {
  username: string;
  displayName: string;
  image: string;
  isAuthor: boolean;
  // following: boolean
}
export interface IComment {
  id: string;
  postId: string;
  body: string;
  for: boolean;
  against: boolean;
  liked: number;
  isLikedByUser: boolean;
  date: Date;
  username: string;
  displayName: string;
  image: string;
  commentors: ICommentor[]
}

export interface ICommentor {
  username: string,
  displayName: string,
  isAuthor: boolean,
  noOfLikes: number
}

export interface IPostConcise {
  id: string;
  heading: string;
  description: string;
  category: ICategory;
  date: Date;
  hostUsername: string;
  hostDisplayName: string;
  hostImage: string;
  isAuthor: boolean;
  isPostLiked: boolean;
  noOfLikes: number;
  noOfComments: number;
  photos: IPhoto[]
}
