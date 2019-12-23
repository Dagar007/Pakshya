import { IUser } from './user';

export interface IPost {
  id: string;
  heading: string;
  description: string;
  category: string;
  date: Date;
  url?: string;
  for: number;
  against: number;
  engagers:IEngagers[]
}

export interface IEngagers {
  username: string
  displayName: string,
  image: string,
  isAuthor: boolean
}
