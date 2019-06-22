import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhotoAlbumService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44394/api';

getAlbumById(id: number) {
  return this.http.get(this.BaseURI + '/albums/' + id.toString());
}

deleteAlbum(id: number) {
  return this.http.delete(this.BaseURI + '/albums/' + id.toString());
}

deleteAlbumPhoto(id: number) {
  return this.http.delete(this.BaseURI + '/albums/photo/' + id.toString());
}

createAlbumPhoto(fileToUpload: File, album_id: number) {
  const formData: FormData = new FormData(); 
  formData.append('Image', fileToUpload); 
  return this.http.post(this.BaseURI +  '/albums/photo/' + album_id.toString(), formData);
}

getMyAlbums() {
  return this.http.get(this.BaseURI + '/albums');
}

getAlbumsByUserId(id: string) {
  return this.http.get(this.BaseURI + '/albums/user/' + id.toString());
}

createAlbum(albumForm: FormGroup) {
  const body = {
    Name: albumForm.value.Name,
    Description: albumForm.value.Description
  };
  return this.http.post(this.BaseURI + '/albums', body);
}

}
