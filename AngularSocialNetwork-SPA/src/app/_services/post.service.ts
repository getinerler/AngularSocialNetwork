import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environments.component';
import { Post } from 'src/app/_models/post';
import { Observable } from 'rxjs';
import { User } from 'src/app/_models/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class PostService {

  baseUrl = environment.apiUrl + "posts/";

  constructor(private authService: AuthService, private http: HttpClient) { }

  getMessages(userId?: number): Observable<Post[]> {
    if (userId) {
      return this.http.get<Post[]>(this.baseUrl + '?userId=' + userId);
    } else {
      return this.http.get<Post[]>(this.baseUrl);
    }
  }

  getPostDetail(feedId?: number): Observable<Post> {
    return this.http.get<Post>(this.baseUrl + 'getPostDetails/?feedId=' + feedId);
  }

  sendPost(text: string): Observable<any> {
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    let model = { userId: user.id, text: text };
    return this.http.post(this.baseUrl + 'saveNewPost/', model);
  }

  likePost(id: number): Observable<number> {
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    let model = { 
      userId: user.id, 
      feedId: id 
    };
    return this.http.post<number>(this.baseUrl + 'likePost/', model);
  }
}
