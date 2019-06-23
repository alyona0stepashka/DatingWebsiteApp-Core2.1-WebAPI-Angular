import { Component, OnInit } from '@angular/core';
import { AlbumTab } from 'src/app/models/album-tab.model'; 
import { ToastrService } from 'ngx-toastr';
import { PhotoAlbumService } from 'src/app/services/photo-album.service'; 
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms'; 
import * as signalR from '@aspnet/signalr';
import { ChatService } from 'src/app/services/chat.service';
import { ChatTab } from 'src/app/models/chat-tab.model';
import { DatePipe } from '@angular/common';
import { SignalRService } from 'src/app/services/signal-r.service';
import { MessageTab } from 'src/app/models/message-tab.model';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css']
})
export class ChatsComponent implements OnInit {

  public createForm: FormGroup = this.formBuilder.group({
    Name: ['', [Validators.required]],
    Description: ['', [Validators.required]]
  });
 
  chatList: ChatTab[] = new Array();
  // resList: ChatTab[] = new Array();
  imageUrl = '/assets/img/no-image.png';
  submitted = false;
  isOpen = false;
  isBlock = false;
  isOnline: any;
  chatId: number = null;

  constructor(private toastr: ToastrService,
              private formBuilder: FormBuilder,
              private chatService: ChatService,
              private activateRoute: ActivatedRoute,
              public signalRService: SignalRService,
              public datepipe: DatePipe
              ) { }

  async ngOnInit() {
    this.resetList();
    this.addSendListener();
  }

  resetList() {
    this.chatList = new Array();
    this.chatService.getMyChats().subscribe(
      res => {
        this.chatList = res as ChatTab[];
        this.chatList.sort(m=>m.LastMessageDateTime);
        this.chatList.reverse();
        // this.resList.forEach(chat => {
        //   this.chatList.unshift(chat);
        // });
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    ); 
  }
  
  public addSendListener() {
    this.signalRService.hubConnection.on('Send', (data) => {
      this.signalRService.incomingMessage = data as MessageTab;

      let new_chat = new ChatTab();
      new_chat.Id = this.signalRService.incomingMessage.ChatId;
      if (this.signalRService.incomingMessage.Text.length<15){
        new_chat.LastMessage = this.signalRService.incomingMessage.Text;
      } else {
        new_chat.LastMessage = this.signalRService.incomingMessage.Text.substring(0, 14);
      }
      new_chat.LastMessageDateTime = this.signalRService.incomingMessage.DateSend;
      new_chat.LastSenderAvatarPath = this.signalRService.incomingMessage.SenderAvatarPath;
      new_chat.Name = this.signalRService.incomingMessage.SenderName;
      new_chat.IsBlock = false;
      new_chat.HasNew = true;

      this.chatList.unshift(new_chat);
      // this.resetList();
      this.signalRService.soundNotify.play();
      console.log(data);
    });
  }

  // onSubmit() {
  //   this.albumService.createAlbum(this.createForm).subscribe(
  //     res => {
  //       this.toastr.success('New album created!', 'Creating successful.');
  //       this.resetList();
  //     },
  //     err => {
  //       console.log(err);
  //     }
  //   );
  // }

  onClearHistory(id: number) {
    this.chatService.clearChatHistory(id).subscribe(
      res => {
        this.toastr.success('History deleted!', 'Deleting successful.');
        this.isOpen = false;
        this.resetList();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  openChat(id: number) {
    if (this.chatId == id){
      this.isOpen = false;
      this.chatId = 0;
    } else {
      this.chatId = id;
      let ch = this.chatList.find(m => m.Id == id);
      this.isBlock = ch.IsBlock;
      this.isOnline = ch.IsOnline;
      this.isOpen = true;
      console.log(id); 
    }
  }

}