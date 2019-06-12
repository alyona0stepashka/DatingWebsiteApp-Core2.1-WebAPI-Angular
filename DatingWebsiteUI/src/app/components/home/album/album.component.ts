import { Component, OnInit } from '@angular/core';
import { AlbumTab } from 'src/app/models/album-tab.model';
import { UserService } from 'src/app/services/user.service';
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

  public userId: any;
  public albumList: AlbumTab[];
  private baseURL = 'https://localhost:44394';

  constructor(private service: UserService,
              private toastr: ToastrService,
              private formBuilder: FormBuilder,
              private albumService: PhotoAlbumService,
              private activateRoute: ActivatedRoute,
              private router: Router) { }

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.userId = params.id); 
    if (this.userId == 0) {
      this.albumService.getMyAlbums().subscribe(
        res => {
          this.albumList = res as AlbumTab[];
          this.albumList.forEach(element => {
            element.FilePath = this.baseURL + element.FilePath;
          });
        },
        err => {
          console.log(err);
        }
      );
    } else {
      this.albumService.getAlbumsByUserId(this.userId).subscribe(
        res => {
          this.albumList = res as AlbumTab[];
          this.albumList.forEach(element => {
            element.FilePath = this.baseURL + element.FilePath;
          });
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
      },
      err => {
        console.log(err);
      }
    );
  }

  goToAlbum(id: number) {
    this.router.navigate(['/home/album/' + this.userId + '/album-details/' + id]);
  }

}
