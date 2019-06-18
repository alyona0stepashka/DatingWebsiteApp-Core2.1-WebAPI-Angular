import { Component, OnInit } from '@angular/core';
import { AlbumTab } from 'src/app/models/album-tab.model'; 
import { ToastrService } from 'ngx-toastr';
import { PhotoAlbumService } from 'src/app/services/photo-album.service'; 
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms'; 
import * as signalR from '@aspnet/signalr';
import { ChatService } from 'src/app/services/chat.service';
import { ChatTab } from 'src/app/models/chat-tab.model';

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
 
  chatList: ChatTab[];
  imageUrl = '/assets/img/no-image.png';
  submitted = false;
  isOpen = false;
  chatId: number = null;

  constructor(private toastr: ToastrService,
              private formBuilder: FormBuilder,
              private chatService: ChatService,
              private activateRoute: ActivatedRoute
              ) { }

  async ngOnInit() { 
    this.resetList();
  }

  resetList() { 
      this.chatService.getMyChats().subscribe(
        res => {
          this.chatList = res as ChatTab[];
        },
        err => {
          console.log(err);
        }
      ); 
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
      }
    );
  }

  openChat(id: number) {
    this.chatId = id;
    this.isOpen = true;
    console.log(id);
  }

}