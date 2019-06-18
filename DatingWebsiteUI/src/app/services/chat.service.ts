import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44394/api';

  getMyChats() {
    return this.http.get(this.BaseURI + '/chats/my');
  }

  getChatById(id: number) {
    return this.http.get(this.BaseURI + '/chats/' + id.toString());
  }

  clearChatHistory(id: number) {
    return this.http.get(this.BaseURI + '/chats/clear/' + id.toString());
  }
}
