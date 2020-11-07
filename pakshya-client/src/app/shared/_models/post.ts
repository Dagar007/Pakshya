import { IPhoto } from './profile';

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
    body: string;
    support: boolean;
    against: boolean;
    totalLikes: number;
    hasLoggedInUserLiked: boolean;
    date: string;
    authorEmail: string;
    authorDisplayName: string;
    authorImage: string;
  }

export interface ICommentor {
  username: string;
  displayName: string;
  isAuthor: boolean;
  noOfLikes: number;
}

export interface IPostConcise {
  id: string;
  heading: string;
  description: string;
  category: ICategory;
  date: Date;
  hostId: string;
  hostDisplayName: string;
  hostImage: string;
  isAuthor: boolean;
  isPostLiked: boolean;
  noOfLikes: number;
  noOfComments: number;
  photos: IPhoto[];
  isImageEdited: boolean;
}
