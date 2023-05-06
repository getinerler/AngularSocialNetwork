import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from 'src/app/_models/post';
import { User } from 'src/app/_models/user';
import { UserDetail } from 'src/app/_models/userDetail';
import { environment } from 'src/environments/environments.component';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUserDetails(): Observable<UserDetail> {
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    return this.http.get<UserDetail>(this.baseUrl + "users/getProfileInfo?id=" + user.id);
  }

  getUserPosts(): Observable<Post[]> {
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    return this.http.get<Post[]>(this.baseUrl + "users/getposts?id=" + user.id);
  }
}
