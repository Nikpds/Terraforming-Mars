export class User {
    id: string;
    firstname: string;
    lastname: string;
    email: string;
    password: string;
    externaLogin: boolean;
    teams: Array<Team>;
    gameScores: Array<GameScore>;
    constructor() {
        this.gameScores = new Array<GameScore>();
        this.teams = new Array<Team>();
    }
}

export class Game {
    date: Date;
    gamePlayers: Array<GameScore>;
    constructor() {
        this.gamePlayers = new Array<GameScore>();
    }
}

export class Team {
    id: string;
    title: string;
    color: string;
}

export class GameScore {
    id: string;
    userId: string;
    user: User;
    gameId: string;
    game: Game;
    points: number;
    place: number;
    awardsPlaced = 0;
    awardsWon = 0;
    milestones = 0;
    board: BoardMats;
}

export enum BoardMats {
    Basic,
    Hellas,
    Elysium
}
