import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { MessageTab } from '../models/message-tab.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  public incoming_message: MessageTab;
  public outgoing_message: MessageSend;
  public sound_notify = '';
  private hubConnection: signalR.HubConnection;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:44394/chat')
                            .build();

    this.hubConnection.start()
                      .then(() => console.log('Connection started'))
                      .catch(err => console.log('Error while starting connection: ' + err));
  }
  public addSoundNotifyListener = () => {
    this.hubConnection.on('SoundNotify', (data) => {
      this.sound_notify = data;
      console.log(data);
    });
  }
  public addSendListener = () => {
    this.hubConnection.on('Send', (data) => {
      this.incoming_message = data;
      console.log(data);
    });
  }
  // public addNewChatRoomListener = () => {
  //   this.hubConnection.on('NewChatRoom', (data) => {
  //     this.incoming_message = data;
  //     console.log(data);
  //   });
  // }
  constructor() { }
}
