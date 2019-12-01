export interface IPost {
  id: string;
  heading: string;
  description: string;
  category: string;
  date: string;
  url?: string;
  for: number;
  against: number;
}
