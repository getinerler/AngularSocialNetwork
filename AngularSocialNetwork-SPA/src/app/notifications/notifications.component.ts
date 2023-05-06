import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../_services/notification.service';
import { Notification } from 'src/app/_models/notification';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {

  notifications: Notification[] = [];

  constructor(private notificationService: NotificationService) { }

  ngOnInit() {
    this.getNotifications();
  }

  getNotifications() {
    this.notificationService.getNotifications()
      .subscribe(
        res => this.notifications = res,
        err => alert(JSON.stringify(err))
      );
  }
}
