import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignalRService } from 'src/app/services/signal-r.service';
import { MessageTab } from 'src/app/models/message-tab.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router,
              public signalRService: SignalRService,
              public toastrService: ToastrService) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.addSendListener();
    this.addSendFriendRequestListener();
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login']);
  }

  public addSendListener() {
    this.signalRService.hubConnection.on('Send', (data) => {
      const mess = data;
      if (mess.text.length > 15) { 
        mess.text = mess.text.substring(0, 14) + '...';
      }
      this.toastrService.info(mess.senderName + ': ' + mess.text, 'New Message');
      this.signalRService.soundNotify.load();
      this.signalRService.soundNotify.play();
      console.log(data);
    });
  }
  public addSendFriendRequestListener() {
    this.signalRService.hubConnection.on('SendFriendRequest', (data) => { 
      this.toastrService.info(data, 'New Friend Request');
      this.signalRService.soundNotify.load();
      this.signalRService.soundNotify.play();
      console.log(data);
    });
  }


}
