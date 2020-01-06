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
    interests: ICategory[]
}

export interface IPhoto {
    id: string;
    url: string;
    isMain: boolean;
}