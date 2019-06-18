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

@Component({
  selector: 'app-chat-detail',
  templateUrl: './chat-detail.component.html',
  styleUrls: ['./chat-detail.component.css']
})
export class ChatDetailComponent implements OnChanges {
  @Input() chatId: number; 

  messages: MessageTab[] = new Array();
  UploadFiles: File[] = new Array();
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
              public dropzone: NgxDropzoneModule) { }

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
          this.message_images = new Array();
          this.messages.forEach(element => {
            if (element.FilePathes.length>0) {
              element.FilePathes.forEach(file => {
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
            }
          });
        },
        err => {
          console.log(err);
        }
      );
  } 
  
  open(index: number): void {
    // open lightbox
    this.lbLightbox.open(this.message_images, index);
  }

  close(): void {
    // close lightbox programmatically
    this.lbLightbox.close();
  }

  onFilesAdded(files: File[]) {
    this.UploadFiles = files;
  }

  onUploadFiles() {
    this.UploadFiles.forEach(file => {
      const reader = new FileReader();
      reader.readAsDataURL(file);

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
    });
  }

  onFilesRejected(files: File[]) {
    console.log(files);
  }

}
