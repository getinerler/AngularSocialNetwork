import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { UserDetail } from '../_models/userDetail';
import { Post } from '../_models/post';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {
  id: number = -1;
  userId: number = -1;
  user: UserDetail = null!;
  posts: Post[] = [];
  isUsersPage: boolean = false;
  followButtonLabel: string = "";

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {  
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    this.id = user.id;
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('id')!);
      this.getUserDetails(this.userId);
      this.getUserPosts(this.userId);
      this.isUsersPage = this.id === this.userId;
    });
  }

  getUserDetails(userId: number) {
    this.userService.getUserDetails(userId).subscribe(
      res => {
        this.user = res;
        this.followButtonLabel = this.user.following ? "Following" : "Follow";
      },
      err => {
        alert(JSON.stringify(err));
      }
    );
  }

  getUserPosts(userId: number) {
    this.userService.getUserPosts(userId).subscribe(
      res => {
        this.posts = res;
      },
      err => {
        alert(JSON.stringify(err));
      }
    );
  }

  changeFollow() {
    this.userService.changeFollow(this.id, this.userId, !this.user.following).subscribe(
      res => {
        this.user.following = !this.user.following;
        this.followButtonLabel = this.user.following ? "Following" : "Follow";
      }, 
      err => {
        alert(JSON.stringify(err));
      })
  }
}
