import { Component, OnInit } from '@angular/core';
import { AlbumTab } from 'src/app/models/album-tab.model'; 
import { ToastrService } from 'ngx-toastr';
import { PhotoAlbumService } from 'src/app/services/photo-album.service'; 
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms'; 

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

 public createForm: FormGroup = this.formBuilder.group({
    Name: ['', [Validators.required]],
    Description: ['', [Validators.required]]
  });

  userId: any;
  albumList: AlbumTab[];
  imageUrl = '/assets/img/no-image.png';
  submitted = false;
  isOpen = false;
  albumId: number = null;

  constructor(private toastr: ToastrService,
              private formBuilder: FormBuilder,
              private albumService: PhotoAlbumService,
              private activateRoute: ActivatedRoute
              ) { }

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.userId = params.id);
    this.resetList();
  }

  resetList(){
    if (this.userId == 0) {
      this.albumService.getMyAlbums().subscribe(
        res => {
          this.albumList = res as AlbumTab[];
        },
        err => {
          console.log(err);
        }
      );
    } else {
      this.albumService.getAlbumsByUserId(this.userId).subscribe(
        res => {
          this.albumList = res as AlbumTab[];
        },
        err => {
          console.log(err);
        }
          );
    }
  }

  onSubmit() {
    this.albumService.createAlbum(this.createForm).subscribe(
      res => {
        this.toastr.success('New album created!', 'Creating successful.');
        this.resetList();
      },
      err => {
        console.log(err);
      }
    );
  }

  onDeleteAlbum(id: number) {
    this.albumService.deleteAlbum(id).subscribe(
      res => {
        this.toastr.success('Album deleted!', 'Deleting successful.');
        this.isOpen = false;
        this.resetList();
      },
      err => {
        console.log(err);
      }
    );
  }

  openAlbum(id: number) {
    this.albumId = id;
    this.isOpen = true;
    console.log(id);
  }

}
