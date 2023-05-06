import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Notification } from 'src/app/_models/notification';
import { User } from 'src/app/_models/user';
import { environment } from 'src/environments/environments.component';

@Injectable({
  providedIn: 'root'
})

export class NotificationService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getNotifications(): Observable<Notification[]> {
    let user: User = JSON.parse(localStorage.getItem("user")?? "{}");
    return this.http.get<Notification[]>(this.baseUrl + "notifications?id=" + user.id);
  }
}
