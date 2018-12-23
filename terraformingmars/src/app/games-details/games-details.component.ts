import { Component, OnInit } from '@angular/core';
import { User, Game, GameScore, BoardMats } from '../model';
import { MainService } from '../main.service';
import { UtilityService } from '../shared/utility.service';
import { ActivatedRoute, Params } from '@angular/router';
import { LoaderService } from '../shared/loader.service';

@Component({
  selector: 'app-games-details',
  templateUrl: './games-details.component.html',
  styleUrls: ['./games-details.component.sass']
})
export class GamesDetailsComponent implements OnInit {
  players: Array<User>;
  game: Game;
  gameScore: GameScore;
  boards: any;
  boardId: any;
  constructor(
    private service: MainService,
    private activeRoute: ActivatedRoute,
    private utility: UtilityService,
    private loader: LoaderService
  ) { }

  ngOnInit() {
    this.activeRoute.params.subscribe((param: Params) => {
      const id = param['id'];
      if (id === 'new') {
        this.game = new Game();
      } else {

      }
    });
    this.boards = this.utility.parseEnum(BoardMats);
    this.getPlayers();
  }

  getPlayers() {
    this.service.getPlayers().subscribe(res => {
      this.players = res;
      console.log(res);
    }, error => {

    });
  }

  addPlayer() {
    if (this.game.gamePlayers.length === 5) {
      return;
    }
    this.gameScore = new GameScore();
  }

  addGameScoreToGame() {
    this.game.gamePlayers.push(this.gameScore);
    this.game.gamePlayers.sort(x => x.place);
    this.gameScore = null;
  }

  setBoardToPlayers() {
    if (this.game.gamePlayers.length > 0) {
      this.game.gamePlayers.forEach(x => x.board = this.boardId);
    }
    console.log(this.boardId);
  }

  insertGame() {
    this.loader.show();
    this.setBoardToPlayers();
    this.service.addGame(this.game).subscribe(res => {
      this.game = res;
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  updateGame() {
    this.loader.show();
    this.service.updateGame(this.game).subscribe(res => {
      this.game = res;
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  getPlayer(id: string) {
    const i = this.players.findIndex(x => x.id === id);
    return this.players[i].lastname + ' ' + this.players[i].firstname;
  }
}
