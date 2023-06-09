import { Component, Input, OnInit } from '@angular/core';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { Router } from '@angular/router';
import { faHeart as fasHeart, faRetweet } from '@fortawesome/free-solid-svg-icons';
import { faHeart, faComment, faTrashCan } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  faReweet = faRetweet;
  faComment = faComment;
  faHeart = faHeart;
  fasHeart = fasHeart;
  faTrashCan = faTrashCan;

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

  repostPost(id: number) {
    this.postService.repostPost(id).subscribe(
      res => {
        this.post.reposted = !this.post.reposted;
        this.post.retweetCount = res;
      },
      err => alert(JSON.stringify(err))
    );
  }

  deletePost(id: number){
    this.postService.deletePost(id).subscribe(
      res => {},
      err => { alert(JSON.stringify(err));}
    );
  }
}
