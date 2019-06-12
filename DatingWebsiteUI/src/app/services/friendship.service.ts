import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FriendshipService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44394/api';

sendRequest(id: string) {
  return this.http.get(this.BaseURI + '/friends/' + id.toString());
}

confirmRequest(id: string) {
  return this.http.get(this.BaseURI + '/friends/confirmation/' + id.toString());
}

deleteRequest(id: string) {
  return this.http.delete(this.BaseURI + '/friends/delete/' + id.toString());
}

getMyOutgoingRequests() {
  return this.http.get(this.BaseURI + '/friends/outgoing');
}

getMyIncomingRequests() {
  return this.http.get(this.BaseURI + '/friends/incoming');
}

getMyFriends() {
  return this.http.get(this.BaseURI + '/friends');
}

}
