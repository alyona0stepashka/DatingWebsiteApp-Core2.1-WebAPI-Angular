import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { AlbumDetails } from 'src/app/models/album-details.model';
import { ToastrService } from 'ngx-toastr';
import { PhotoAlbumService } from 'src/app/services/photo-album.service';
import { Lightbox } from 'ngx-lightbox';
import { NgxDropzoneModule } from 'ngx-dropzone';

@Component({
  selector: 'app-album-detail',
  templateUrl: './album-detail.component.html',
  styleUrls: ['./album-detail.component.css']
})
export class AlbumDetailComponent implements OnChanges {

  @Input() albumId: number;
  @Input() userId: any;

  album: AlbumDetails;
  UploadFiles: File[] = new Array();
  submitted = false;
  private lbAlbums: any[] = new Array();
  private baseURL = 'https://localhost:44394';

  constructor(private toastr: ToastrService,
              private albumService: PhotoAlbumService,
              private lbLightbox: Lightbox,
              public dropzone: NgxDropzoneModule) { }

  // async ngOnInit() {
  //   this.resetList();
  // }

  ngOnChanges() {
    this.resetList();
  }

  resetList() {
    this.albumService.getAlbumById(this.albumId).subscribe(
        res => {
          this.album = res as AlbumDetails;
          this.lbAlbums = new Array();
          this.album.FilePathes.forEach(element => {
            const src = this.baseURL + element.Value;
            const caption = '';
            const thumb = '';
            const album = {
              src,
              caption,
              thumb
           };

            this.lbAlbums.push(album);
          });
        },
        err => {
          console.log(err);
          this.toastr.error(err.error, 'Error');
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
        this.toastr.error(err.error, 'Error');
      }
    );
  } 
  
  open(index: number): void {
    // open lightbox
    this.lbLightbox.open(this.lbAlbums, index);
  }

  close(): void {
    // close lightbox programmatically
    this.lbLightbox.close();
  }

  onFilesAdded(files: File[]) {
    this.UploadFiles = files;
  }

  onClearPhoto() {
    this.UploadFiles = new Array();
  }

  onUploadFiles() {
    let isSuccess = false;
    this.UploadFiles.forEach(file => {
      const reader = new FileReader();
      reader.readAsDataURL(file);

      this.albumService.createAlbumPhoto(file, this.albumId).subscribe(
      (res: any) => {
          this.resetList();
      },
      err => {
        console.log(err);
        isSuccess = false;
      }
    );
    });
    if (isSuccess) {
      this.toastr.success('New photo added!', 'Process successful.');
    } else {
      this.toastr.error('Some of files not added', 'Process error');
    }
  }

  onFilesRejected(files: File[]) {
    console.log(files);
  }

}
