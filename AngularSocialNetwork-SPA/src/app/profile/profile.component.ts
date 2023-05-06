import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { UserDetail } from '../_models/userDetail';
import { Post } from '../_models/post';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {

  user: UserDetail = null!;
  posts: Post[] = [];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.getUserDetails();
    this.getUserPosts();
  }

  getUserDetails() {
    this.userService.getUserDetails().subscribe(
      res => {
        this.user = res;
      },
      err => {
        alert(JSON.stringify(err));
      }
    );
  }

  getUserPosts() {
    this.userService.getUserPosts().subscribe(
      res => {
        this.posts = res;
      },
      err => {
        alert(JSON.stringify(err));
      }
    );
  }
}
