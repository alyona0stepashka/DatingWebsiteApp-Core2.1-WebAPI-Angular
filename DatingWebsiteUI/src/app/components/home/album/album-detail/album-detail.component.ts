import { Component, OnInit } from '@angular/core';
import { AlbumDetails } from 'src/app/models/album-details.model';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { PhotoAlbumService } from 'src/app/services/photo-album.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlbumTab } from 'src/app/models/album-tab.model';

@Component({
  selector: 'app-album-detail',
  templateUrl: './album-detail.component.html',
  styleUrls: ['./album-detail.component.css']
})
export class AlbumDetailComponent implements OnInit {

  public createForm: FormGroup = this.formBuilder.group({
    Name: ['', [Validators.required]],
    Description: ['', [Validators.required]]
  });

  albumId: any;
  album: AlbumDetails;
  UploadFile: File = null;
  imageUrl = '/assets/img/no-image.png';
  submitted = false;
  private baseURL = 'https://localhost:44394';

  constructor(private service: UserService,
              private toastr: ToastrService,
              private formBuilder: FormBuilder,
              private albumService: PhotoAlbumService,
              private activateRoute: ActivatedRoute,
              private router: Router) { }

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.albumId = params.id);
    //await this.activateRoute.params.subscribe(params => this.albumId = params.id);
    this.resetList();
  }

  resetList(){
      this.albumService.getAlbumById(this.albumId).subscribe(
        res => {
          this.album = res as AlbumDetails;
        },
        err => {
          console.log(err);
        }
      ); 
  }

  onSubmit() {
    this.albumService.createAlbum(this.createForm).subscribe(
      res => {
        this.toastr.success('New album created!', 'Creating successful.');
      },
      err => {
        console.log(err);
      }
    );
  }

  onDeletePhoto(id: number) {
    this.albumService.deleteAlbum(id).subscribe(
      res => {
        this.toastr.success('Photo deleted!', 'Deleting successful.');
        this.resetList();
      },
      err => {
        console.log(err);
      }
    );
  }

  uploadPhoto(file: FileList) {
    this.UploadFile = file.item(0);
    const reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    };
    reader.readAsDataURL(this.UploadFile);

    this.albumService.createAlbumPhoto(this.UploadFile, this.albumId).subscribe(
      (res: any) => {
          this.toastr.success('New photo created!', 'Creating successful.');
      },
      err => {
        console.log(err);
      }
    );
}

}
