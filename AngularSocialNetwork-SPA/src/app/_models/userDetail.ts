export interface UserDetail {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    bio: string;
    link: string;
    photo: string;
    date: Date;
    followerCount: number;
    followingCount: number;
    following: boolean;
}
