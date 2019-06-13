import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { AlbumDetails } from 'src/app/models/album-details.model';
import { ToastrService } from 'ngx-toastr';
import { PhotoAlbumService } from 'src/app/services/photo-album.service';

@Component({
  selector: 'app-album-detail',
  templateUrl: './album-detail.component.html',
  styleUrls: ['./album-detail.component.css']
})
export class AlbumDetailComponent implements OnChanges {

  @Input() albumId: number;

  album: AlbumDetails;
  UploadFile: File = null; 
  submitted = false;
  private baseURL = 'https://localhost:44394';

  constructor(private toastr: ToastrService,
              private albumService: PhotoAlbumService
              ) { }

  // async ngOnInit() {
  //   this.resetList();
  // }

  ngOnChanges(){
    this.resetList();
  }

  resetList() {
    console.log(this.albumId + 'afqwfqfqef');
    this.albumService.getAlbumById(this.albumId).subscribe(
        res => {
          this.album = res as AlbumDetails;
        },
        err => {
          console.log(err);
        }
      );
  }

  onDeletePhoto(id: number) {
    this.albumService.deleteAlbumPhoto(id).subscribe(
      res => {
        this.toastr.success('Photo deleted!', 'Deleting successful.');
        this.resetList();
      },
      err => {
        console.log(err);
        this.toastr.error('Photo deleted Error!', 'Deleting error.');
      }
    );
  }

  uploadPhoto(file: FileList) {
    this.UploadFile = file.item(0);
    const reader = new FileReader(); 
    reader.readAsDataURL(this.UploadFile);

    this.albumService.createAlbumPhoto(this.UploadFile, this.albumId).subscribe(
      (res: any) => {
          this.toastr.success('New photo created!', 'Creating successful.');
          this.resetList();
      },
      err => {
        console.log(err);
        this.toastr.error('New photo created error!', 'Creating error.');
      }
    );
  }

}
