import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Follower } from '../_models/follower';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {

  followers: Follower[] = [];
  followings: Follower[] = [];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getFollowers().subscribe(
      res => { this.followers = res; },
      err => { JSON.stringify(err); }
    );

    this.userService.getFollowings().subscribe(
      res => { this.followings = res; },
      err => { JSON.stringify(err); }
    );
  }

}
