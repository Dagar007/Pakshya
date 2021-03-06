import { ICategory } from './post';

export interface IProfile {
    id: string;
    displayName: string;
    email: string;
    image: string;
    bio: string;
    education: string;
    work: string;
    following: boolean;
    followersCount: number;
    followingCount: number;
    photos: IPhoto[];
    views: number;
    interests: ICategory[];
    posts: IUserPosted[];
    comments: IUserCommented[];
    followings: IFollow[];
    followers: IFollow[];
}

export interface IPhoto {
    id: string;
    url: string;
    isMain: boolean;
}

export interface IUserPosted {
    id: string;
    heading: string;
    noOfLikes: number;
    noOfComments: number;
}
export interface IUserCommented {
    id: string;
    body: string;
    noOfLikes: number;
}
export interface IFollow {
    id: string;
    displayName: string;
    url: string;
}
