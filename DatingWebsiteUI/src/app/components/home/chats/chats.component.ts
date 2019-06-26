import { Component, OnInit } from '@angular/core'; 
import { ToastrService } from 'ngx-toastr'; 
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ChatService } from 'src/app/services/chat.service';
import { ChatTab } from 'src/app/models/chat-tab.model';
import { DatePipe } from '@angular/common';
import { SignalRService } from 'src/app/services/signal-r.service';
import { MessageTab } from 'src/app/models/message-tab.model';
//import { timer } from 'rxjs/observable/timer';

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
  incomingMessage: MessageTab = new MessageTab();
  imageUrl = '/assets/img/no-image.png';
  submitted = false;
  isOpen = false;
  isBlock = false;
  isOnline: any;
  chatId: number = null;
  ChatAvatar = ''; 

  constructor(private toastr: ToastrService,
              private formBuilder: FormBuilder,
              private chatService: ChatService, 
              public signalRService: SignalRService,
              public datepipe: DatePipe
              ) { }

  async ngOnInit() {
    this.resetList();
    this.addSendListener();
    this.addSendMyselfListener(); 
  }

  resetList() {
    this.chatList = new Array();
    this.chatService.getMyChats().subscribe(
      res => {
        this.chatList = res as ChatTab[];
        this.chatList.sort(m => m.LastMessageDateTime);
        this.chatList.reverse(); 
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    ); 
  }
  
  public addSendListener() {
    this.signalRService.hubConnection.on('Send', (data) => { 
      let new_chat = new ChatTab(); 
      new_chat.Id = data.chatId;
      if (data.text.length < 15){
        new_chat.LastMessage = data.text;
      } else {
        new_chat.LastMessage = data.text.substring(0, 14) + '...';
      }
      new_chat.LastMessageDateTime = data.dateSend;
      new_chat.LastSenderAvatarPath = data.senderAvatarPath;
      new_chat.Name = data.senderName;
      new_chat.IsBlock = false;
      new_chat.HasNew = true;

      const index = this.chatList.indexOf(this.chatList.find(m => m.Id == new_chat.Id));
      if (index > -1) {
        this.chatList.splice(index, 1);
      }
      this.chatList.unshift(new_chat); 
      console.log(data);
    });
  } 
  
  public addSendMyselfListener() {
    this.signalRService.hubConnection.on('Send', (data) => { 
      let new_chat = new ChatTab(); 
      new_chat.Id = data.chatId;
      if (data.text.length < 15){
        new_chat.LastMessage = data.text;
      } else {
        new_chat.LastMessage = data.text.substring(0, 14) + '...';
      }
      new_chat.LastMessageDateTime = data.dateSend;
      new_chat.LastSenderAvatarPath = data.senderAvatarPath;
      new_chat.Name = data.senderName;
      new_chat.IsBlock = false;
      const old_chat = this.chatList.find(m => m.Id == new_chat.Id);
      new_chat.HasNew = old_chat.HasNew;

      const index = this.chatList.indexOf(old_chat);
      if (index > -1) {
        this.chatList.splice(index, 1);
      }
      this.chatList.unshift(new_chat); 
      
      if (this.chatId == new_chat.Id && this.isOpen){
        (async () => {  
          await this.delay(2500); 
          document.getElementById(this.chatId.toString()).style.backgroundColor = '#62758600'; 
        })();
      }


    });
  }

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
  
  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
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
      this.ChatAvatar = ch.ChatIconPath;
      this.isOpen = true;  
      (async () => {  
        await this.delay(2500); 
        document.getElementById(id.toString()).style.backgroundColor = '#62758600'; 
      })();


    }
  }

}