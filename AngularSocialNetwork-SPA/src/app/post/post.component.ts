import { Component, Input, OnInit } from '@angular/core';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  @Input() post: Post = null!;

  constructor(private router: Router, private postService: PostService) { }

  ngOnInit() {
  }

  getDetail(id: number) {
    this.router.navigate(['/postDetail', id]);
  }

  likePost(id: number) {
    this.postService.likePost(id).subscribe(
      res => {
        this.post.liked = !this.post.liked;
        this.post.likeCount = res;
      },
      err => alert(JSON.stringify(err))
    );
  }
}
