import { Component, Input, OnInit } from '@angular/core';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.css']
})
export class PostDetailComponent implements OnInit {

  @Input() feedId: number = -1;
  post: Post = null!;

  constructor(private route: ActivatedRoute, private postService: PostService) { }

  ngOnInit() {
    let id = Number(this.route.snapshot.params['id']);

    this.postService.getPostDetail(id)
      .subscribe(
        res => { this.post = res; },
        err => alert(JSON.stringify(err))
      );
  }
}
