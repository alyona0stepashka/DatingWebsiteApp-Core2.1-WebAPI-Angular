import { Component, OnInit, Input, OnChanges, QueryList } from '@angular/core'; 
import { ToastrService } from 'ngx-toastr'; 
import { Lightbox } from 'ngx-lightbox'; 
import { MessageTab } from 'src/app/models/message-tab.model';
import { ChatService } from 'src/app/services/chat.service';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SignalRService } from 'src/app/services/signal-r.service'; 
import { DatePipe } from '@angular/common'; 
import { Router } from '@angular/router';
import { EmojiTab } from 'src/app/models/emoji-tab.model';

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
  incomingMessage: MessageTab = new MessageTab();
  public Emojies: EmojiTab = new EmojiTab();
  public IsOpenEmoji: boolean = false;
  messageText = '';
  UploadFiles: File[] = new Array();
  OpenFiles: string[] = new Array();
  submitted = false;
  private message_images: any[] = new Array(); 
  private baseURL = 'https://localhost:44394'; 

  constructor(private toastr: ToastrService,
              private chatService: ChatService,
              private lbLightbox: Lightbox,
              public dropzone: NgxDropzoneModule,
              public signalRService: SignalRService,
              public datepipe: DatePipe,
              private router: Router
              ) 
  { 
    this.addSendListener();
    this.addSendMyselfListener(); 
  } 

  ngOnChanges() {
    this.resetList();

    (async () => {  
      await this.delay(2500);
      const new_mes = this.messages.filter(m=>m.IsNew);
      new_mes.forEach(m => { 
        document.getElementById(m.Id.toString()).style.backgroundColor = 'slategrey'; 
      }); 
    })();
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
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
      var message = new MessageTab(); 
      message.Id = data.id;
      message.ChatId = data.chatId; 
      message.SenderId = data.senderId; 
      message.SenderAvatarPath = data.senderAvatarPath; 
      message.SenderName = data.senderName; 
      message.Text = data.text; 
      message.FilePathes = data.filePathes; 
      message.DateSend = data.dateSend; 
      message.IsNew = false;

      // this.incomingMessage = message;
      this.messages.unshift(message); 
    //  var m1 =[];
    //  m1=  this.messages;
    //  this.messages = m1;
      // this.messages.splice(0,0,this.incomingMessage);
      // this.messages = [...this.messages];
      
      console.log(data);
    });
  }
  public addSendMyselfListener() {
    this.signalRService.hubConnection.on('SendMyself', (data) => {
      var message = new MessageTab(); 
      message.Id = data.id;
      message.ChatId = data.chatId; 
      message.SenderId = data.senderId; 
      message.SenderAvatarPath = data.senderAvatarPath; 
      message.SenderName = data.senderName; 
      message.Text = data.text; 
      message.FilePathes = data.filePathes; 
      message.DateSend = data.dateSend; 
      message.IsNew = false;

      // this.incomingMessage = message;
      this.messages.unshift(message);
    //   var m1 =[];
    //  m1=  this.messages;
    //  this.messages = m1;
      // this.messages.splice(0,0,this.incomingMessage);
      // this.messages = [...this.messages];
    
      console.log(data);
    });
  }

  public trackItem (index: number, item: MessageTab) {
    return item.Id;
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

    // if (this.messageText.length>0 && this.UploadFiles.length>0) { 
    //   var formData = new FormData();
    //   formData.append("ChatId", (this.chatId).toString());
    //   formData.append("ReceiverId", "0");
    //   formData.append("Text", this.messageText);
    //   this.UploadFiles.forEach(file => {
    //     formData.append("UploadFiles", file);
    //   }); 
    //   this.chatService.sendMessage(formData).subscribe(
    //     res => { 
    //     },
    //     err => {
    //       console.log(err);
    //       this.toastr.error(err.error, 'Error');
    //     }
    //   );
    //   this.messageText = '';
    // }

    var formData = new FormData();
    formData.append("ChatId", (this.chatId).toString());
    formData.append("ReceiverId", "0");
    formData.append("Text", this.messageText);
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
    this.messageText = '';
  }

  onFilesRejected(files: File[]) {
    console.log(files);
  }

  addEmoji(event) {
    const { messageText } = this;
    const text = `${messageText}${event.emoji.native}`;
    this.messageText = text;
  }

}
