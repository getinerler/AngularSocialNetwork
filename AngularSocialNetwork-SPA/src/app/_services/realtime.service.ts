import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';
import { environment } from 'src/environments/environments.component';

@Injectable({
  providedIn: 'root'
})

export class RealtimeService {
  private hubConnection!: signalR.HubConnection;

  // Observables for features
  private notificationSubject = new BehaviorSubject<any>(null);
  notifications$ = this.notificationSubject.asObservable();

  private messageSubject = new BehaviorSubject<any>(null);
  messages$ = this.messageSubject.asObservable();

  startConnection() {
    let user: User = JSON.parse(localStorage.getItem("user")?.toString() ?? "{}");
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.url +'hub?userId=' + user.id, {
        withCredentials: true
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR connected.');
        this.registerHandlers();
      })
      .catch(err => console.error('SignalR error: ', err));
  }

  private registerHandlers() {
    this.hubConnection.on('NewNotification', (data: number) => {
      this.notificationSubject.next(data);
    });
  }

  // sendMessage(chatId: string, message: string) {
  //   return this.hubConnection.invoke('SendMessage', chatId, message);
  // }
}