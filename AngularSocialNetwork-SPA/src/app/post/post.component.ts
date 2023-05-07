import { Component, Input, OnInit } from '@angular/core';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  @Input() post: Post = null!;
  
  constructor(postService: PostService) { }

  ngOnInit() {
  }

}
