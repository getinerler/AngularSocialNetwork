import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Follower } from '../_models/follower';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {

  followers: Follower[] = [];
  followings: Follower[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
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

}
