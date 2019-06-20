import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { MessageTab } from '../models/message-tab.model';
import { MessageSend } from '../models/message-send.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  public incomingMessage = new MessageTab();
 // public outgoing_message: MessageSend;
  public soundNotify = new Audio('assets/sounds/message.mp3');
  public hubConnection: signalR.HubConnection;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:44394/chat',
                            {
                              skipNegotiation: true,
                              transport: signalR.HttpTransportType.WebSockets,
                              accessTokenFactory: () => localStorage.getItem('token')
                            })
                            .build();

    this.hubConnection.start()
                      .then(() => console.log('Connection started'))
                      .catch(err => console.log('Error while starting connection: ' + err));
  }
  // public addSendListener() {
  //   this.hubConnection.on('Send', (data) => {
  //     this.incomingMessage = data as MessageTab;
  //     this.soundNotify.play();
  //     console.log(data);
  //   });
  // }
  // public addSendMyselfListener() {
  //   this.hubConnection.on('SendMyself', (data) => {
  //     this.incomingMessage = data as MessageTab;
  //     console.log(data);
  //   });
  // }
  public sendMessage(outgoingMessage: MessageSend) {
    this.hubConnection.invoke('Send', outgoingMessage)
    .catch(err => console.error(err));
  }
  public sendMessageFromProfile(outgoingMessage: MessageSend) {
    this.hubConnection.invoke('SendFromProfile', outgoingMessage)
    .catch(err => console.error(err));
  }
  // public addSoundNotifyListener = () => {
  //   this.hubConnection.on('SoundNotify', (data) => {
  //     this.sound_notify = data;
  //     console.log(data);
  //   });
  // }
  // public addNewChatRoomListener = () => {
  //   this.hubConnection.on('NewChatRoom', (data) => {
  //     this.incoming_message = data;
  //     console.log(data);
  //   });
  // }
  constructor() { }
}
