import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Follower } from '../_models/follower';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {

  id: number = -1;
  followers: Follower[] = [];
  followings: Follower[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    this.id = user.id;

    let id = Number(this.route.snapshot.params['id']);

    this.userService.getFollowers(id).subscribe(
      res => { this.followers = res; },
      err => { JSON.stringify(err); }
    );

    this.userService.getFollowings(id).subscribe(
      res => { this.followings = res; },
      err => { JSON.stringify(err); }
    );
  }

  follow (follow: Follower) {
    this.userService.changeFollow(this.id, follow.id, !follow.following).subscribe(
      res => {
        follow.following = !follow.following;
      }, err => {
        alert(JSON.stringify(err));
      }
    );
  }

}
