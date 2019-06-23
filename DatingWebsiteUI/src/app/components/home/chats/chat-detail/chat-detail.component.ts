import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { AlbumDetails } from 'src/app/models/album-details.model';
import { ToastrService } from 'ngx-toastr';
import { PhotoAlbumService } from 'src/app/services/photo-album.service';
import { Lightbox } from 'ngx-lightbox';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';
import { MessageTab } from 'src/app/models/message-tab.model';
import { ChatService } from 'src/app/services/chat.service';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SignalRService } from 'src/app/services/signal-r.service';
import { MessageSend } from 'src/app/models/message-send.model';
import { DatePipe } from '@angular/common'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-chat-detail',
  templateUrl: './chat-detail.component.html',
  styleUrls: ['./chat-detail.component.css']
})
export class ChatDetailComponent implements OnChanges {
  @Input() chatId: number;
  @Input() isOnline: any = null;
  @Input() isBlock: boolean;

  messages: MessageTab[] = new Array();
  outgoingMessage = new MessageSend();
  UploadFiles: File[] = new Array();
  OpenFiles: string[] = new Array();
  submitted = false;
  private message_images: any[] = new Array(); 
  private baseURL = 'https://localhost:44394';

  
  // private _hubConnection: HubConnection | undefined;
  // public async: any;
  // message = '';
  // messages: string[] = [];

  constructor(private toastr: ToastrService,
              private chatService: ChatService,
              private lbLightbox: Lightbox,
              public dropzone: NgxDropzoneModule,
              public signalRService: SignalRService,
              public datepipe: DatePipe,
              private router: Router
              ) 
  {
    //this.signalRService.startConnection();
    this.addSendListener();
    this.addSendMyselfListener(); 
  }

  // async ngOnInit() {
  //   this.resetList();
  // }

  ngOnChanges() {
    this.resetList();
  }

  resetList() {
    this.chatService.getChatById(this.chatId).subscribe(
        res => {
          this.messages = res as MessageTab[];
          this.messages.sort(m => m.DateSend);
          this.messages.reverse();
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
      this.messages.unshift(this.signalRService.incomingMessage);
      this.signalRService.soundNotify.play();
      console.log(data);
    });
  }
  public addSendMyselfListener() {
    this.signalRService.hubConnection.on('SendMyself', (data) => {
      this.signalRService.incomingMessage = data as MessageTab;
      this.messages.unshift(this.signalRService.incomingMessage);
      console.log(data);
    });
  }

  goToProfile(id: string) {
    this.router.navigate(['/home/profile/' + id]);
  }
  
  open(index: number): void { 
    this.message_images = new Array(); 
    this.OpenFiles.forEach(file => {
      const src = this.baseURL + file;
      const caption = '';
      const thumb = '';
      const img = { 
        src,
        caption,
        thumb
      };
      this.message_images.push(img);
    }); 
    this.lbLightbox.open(this.message_images, index); 
  }

  close(): void { 
    this.lbLightbox.close();
  }

  onFilesAdded(files: File[]) {
    this.UploadFiles = files;
  }

  onSendMessage() {  
    this.outgoingMessage.ChatId = this.chatId; 
    var formData = new FormData();
    formData.append("ChatId", (this.chatId).toString());
    formData.append("ReceiverId", "0");
    formData.append("Text", this.outgoingMessage.Text);
    this.UploadFiles.forEach(file => {
      formData.append("UploadFiles", file);
    });

    
    this.chatService.sendMessage(formData).subscribe(
      res => { 
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
    this.outgoingMessage.Text = '';

    //   this.albumService.createAlbumPhoto(file, this.albumId).subscribe(
    //   (res: any) => {
    //       this.toastr.success('New photo created!', 'Creating successful.');
    //       this.resetList();
    //   },
    //   err => {
    //     console.log(err);
    //     this.toastr.error('New photo created error!', 'Creating error.');
    //   }
    // ); 
  }

  onFilesRejected(files: File[]) {
    console.log(files);
  }

}
