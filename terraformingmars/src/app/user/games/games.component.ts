import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/shared/loader.service';
import { ToastrService } from 'src/app/toastr.service';
import { UserService } from '../user.service';
import { Game } from 'src/app/model';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.sass']
})
export class GamesComponent implements OnInit {
  games = new Array<Game>();
  constructor(
    private loader: LoaderService,
    private toastr: ToastrService,
    private service: UserService
  ) { }

  ngOnInit() {
    this.getMyGames();
  }

  getMyGames() {
    this.loader.show();
    this.service.getMyGames().subscribe(res => {
      this.loader.hide();
      console.log(res);
      this.games = res;
    }, error => {
      this.toastr.danger(error);
      this.loader.hide();
    });
  }

}
