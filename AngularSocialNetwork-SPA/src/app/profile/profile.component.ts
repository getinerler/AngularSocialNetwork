import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { UserDetail } from '../_models/userDetail';
import { Post } from '../_models/post';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {

  userId: number = -1;
  user: UserDetail = null!;
  posts: Post[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('id')!);
      this.getUserDetails(this.userId);
      this.getUserPosts(this.userId);
    });
  }

  getUserDetails(userId: number) {
    this.userService.getUserDetails(this.userId).subscribe(
      res => {
        this.user = res;
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
}
