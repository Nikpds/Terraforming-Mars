export class User {
    id: string;
    firstname: string;
    lastname: string;
    nickname: string;
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

export class UserSearchView {
    id: string;
    firstname: string;
    lastname: string;
    nickname: string;
    email: string;
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
    created: Date;
    owner: User;
    icon: string;
    constructor() {
    }
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

export class InvitationDto {
    fullname: string;
    status: InvitationStatus;
    actionDate: Date;
    created: Date;
    isMember: boolean;
}

export enum InvitationStatus {
    Pending,
    Accepted,
    Declined
}

export class Invitation {

}

export class UserProfile {
    firstname: string;
    lastname: string;
    nickname: string;
    email: string;
    teams: Array<Team>;
    invitations: Array<Invitation>;
    constructor() {
        this.teams = new Array<Team>();
        this.invitations = new Array<Invitation>();
    }
}

export interface Invitation {
    id: string;
    userId: string;
    user: User;
    ownerId: string;
    owner: User;
    teamId: string;
    teamTitle: string;
    comments: string;
    created: Date | string;
    inivtationStatus: InvitationStatus;
    actionDate: Date | string;

}
