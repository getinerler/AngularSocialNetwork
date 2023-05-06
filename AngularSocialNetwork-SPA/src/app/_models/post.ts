export interface Post {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    text: string;
    date: Date;
    photo: string;
    likeCount: number;
    retweetCount: number;
    commentCount: number;
    liked: boolean;
  }