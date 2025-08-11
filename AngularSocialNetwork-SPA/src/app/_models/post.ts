export interface Post {
  id: number;
  postId: number;
  userId: number;
  username: string;
  firstName: string;
  lastName: string;
  text: string;
  date: Date;
  photo: string;
  likeCount: number;
  retweetCount: number;
  commentCount: number;
  isReposted: boolean;
  repostedFirstName: string;
  repostedLastName: string;
  liked: boolean;
  reposted: boolean;

  ownPost: boolean;
  
  comments: Post[];
}