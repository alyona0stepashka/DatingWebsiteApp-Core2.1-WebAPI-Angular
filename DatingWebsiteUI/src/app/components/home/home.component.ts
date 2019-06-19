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
    // this.signalRService.soundNotify.play();
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login']);
  }

  public addSendListener() {
    this.signalRService.hubConnection.on('Send', (data) => {
      const mess = data as MessageTab;
      this.toastrService.info(mess.SenderName + ': ' + mess.Text, 'New Message');
      this.signalRService.soundNotify.play();
      console.log(data);
    });
  }


}
