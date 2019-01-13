import { Component, OnInit } from '@angular/core';
import { User, Game, GameScore, BoardMats, Team } from '../model';
import { MainService } from '../main.service';
import { UtilityService } from '../shared/utility.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { LoaderService } from '../shared/loader.service';
import { ToastrService } from '../toastr.service';

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
  teamId: string;
  boardId = 0;
  teams = new Array<Team>();
  constructor(
    private service: MainService,
    private activeRoute: ActivatedRoute,
    private utility: UtilityService,
    private loader: LoaderService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.activeRoute.params.subscribe((param: Params) => {
      const id = param['id'];
      if (id === 'new') {
        this.game = new Game();
        this.game.date = new Date();
        this.getMyTeams();
      } else {
        this.router.navigate(['/home']);
      }
    });
    this.boards = this.utility.parseEnum(BoardMats);

  }

  getMyTeams() {
    this.service.getMyTeams().subscribe(res => {
      this.teamId = res[0].id;
      this.teams = res;
      this.getTeamPlayers();
    }, error => {
      this.toastr.danger(error);
    });
  }

  getTeamPlayers() {
    this.service.getTeamMates(this.teamId).subscribe(res => {
      this.players = res;
    }, error => {
      this.toastr.danger(error);
    });
  }


  addPlayer() {
    if (this.game.gamePlayers.length === 5) { return; }
    this.gameScore = new GameScore();
  }

  addGameScoreToGame() {
    if (!this.validatePlayerInput(this.gameScore)) {
      this.toastr.warning('You haven\'t fill all the fields correctly.');
      return;
    }
    this.game.gamePlayers.push(this.gameScore);
    this.game.gamePlayers.sort(function (a, b) { return a.place - b.place; });
    this.gameScore = null;
  }

  setBoardToPlayers() {
    if (this.game.gamePlayers.length > 0) {
      this.game.gamePlayers.forEach(x => x.board = this.boardId);
    }
  }

  insertGame() {
    this.loader.show();
    this.setBoardToPlayers();
    this.service.addGame(this.game).subscribe(res => {
      this.game = res;
      this.loader.hide();
      this.toastr.success('Game was successfully saved');
    }, error => {
      this.loader.hide();
      this.toastr.danger(error);
    });
  }

  updateGame() {
    this.loader.show();
    this.service.updateGame(this.game).subscribe(res => {
      this.game = res;
      this.loader.hide();
      this.toastr.success('Game was successfully updated');
    }, error => {
      this.loader.hide();
      this.toastr.danger(error);
    });
  }

  getPlayer(id: string) {
    const i = this.players.findIndex(x => x.id === id);
    return this.players[i].lastname + ' ' + this.players[i].firstname;
  }

  validatePlayerInput(g: GameScore): boolean {
    return g.userId && g.points >= 20 && g.place > 0 && g.place < 6 &&
      g.milestones >= 0 && g.milestones < 4 && g.points > 30 && g.awardsWon >= 0
      && g.awardsWon < 4 && g.awardsPlaced >= 0 && g.awardsPlaced < 4;
  }

  removePlayer(i: number) {
    if (i > -1) {
      this.game.gamePlayers.splice(i, 1);
    }
  }

}
