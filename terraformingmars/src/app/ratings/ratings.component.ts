import { Component, OnInit } from '@angular/core';
import { LoaderService } from '../shared/loader.service';
import { MainService } from '../main.service';
import { User } from '../model';

@Component({
  selector: 'app-ratings',
  templateUrl: './ratings.component.html',
  styleUrls: ['./ratings.component.sass']
})
export class RatingsComponent implements OnInit {
  users = new Array<User>();
  constructor(
    private service: MainService,
    private loader: LoaderService
  ) { }

  ngOnInit() {
    this.getRatings();
  }

  getRatings() {
    this.loader.show();
    this.service.getRatings().subscribe(res => {
      this.users = res;
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  getTotalPoints(i: number) {
    return this.users[i].gameScores.reduce(function (a, b) { return a + b.points; }, 0);
  }

  getTotalMilestones(i: number) {
    return this.users[i].gameScores.reduce(function (a, b) { return a + b.milestones; }, 0);
  }

  getTotalAwards(i: number) {
    const won = this.users[i].gameScores.reduce(function (a, b) { return a + b.awardsWon; }, 0);
    const placed = this.users[i].gameScores.reduce(function (a, b) { return a + b.awardsPlaced; }, 0);
    return won + '/' + placed;
  }

  getTotalCountOfPlacement(i: number, p: number) {
    return this.users[i].gameScores.filter(x => x.place === p).length;
  }

  winRatio(i: number) {
    const wins = this.users[i].gameScores.filter(x => x.place === 1).length;
    if (wins === 0) { return 0; }
    return ((wins / this.users[i].gameScores.length) * 100).toFixed(0);
  }
}
