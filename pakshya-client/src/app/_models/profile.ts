import { ICategory } from './post';

export interface IProfile {
    displayName: string;
    username: string;
    image: string;
    bio: string;
    education: string;
    work: string;
    address: string;
    following: boolean;
    followersCount: number;
    followingCount: number;
    photos: IPhoto[];
    views: number;
    interests: ICategory[];
    posts: IUserPosed[];
    comments: IUserCommented[];
    followings: IFollow[];
    followers: IFollow[];
}

export interface IPhoto {
    id: string;
    url: string;
    isMain: boolean;
}

export interface IUserPosed {
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
    username: string;
    displayName: string;
    url: string;
}
