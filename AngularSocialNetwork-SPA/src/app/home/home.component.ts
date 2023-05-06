import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { Post } from '../_models/post';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  profilePhotoUrl: string = null!;

  newPostForm: FormGroup = new FormGroup({
    newPostText: new FormControl("")
  });

  messages: Post[] = [];

  ngOnInit() {
    this.profilePhotoUrl = this.getProfilePhotoUrl();
  }

  constructor(
    private router: Router,
    private postService: PostService) {

    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    let userId: number = user.id;

    postService.getMessages(userId).subscribe(
      (res: Post[]) => {
        this.messages = res;
      }, error => {
        alert(JSON.stringify(error));
      });
  }

  getProfilePhotoUrl(): string {
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    return user.photo ?? "";
  }

  sendPost() {
    let value = this.newPostForm.value.newPostText;
    this.postService.sendPost(value).subscribe(
      next => {}, 
      err=>{alert(JSON.stringify(err))},
      () => {location.reload(); }
      );
  }
}
