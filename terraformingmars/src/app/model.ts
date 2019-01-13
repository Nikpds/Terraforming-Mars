export class User {
    id: string;
    firstname: string;
    lastname: string;
    nickname: string;
    email: string;
    password: string;
    isActive: boolean;
    externaLogin: boolean;
    teams: Array<Team>;
    gameScores: Array<GameScore>;
    userRole: UserRole;
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
    // Not Mapped
    members: number;
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
    invitationsId: string;
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
    invitations: Array<InvitationViewDto>;
    constructor() {
        this.teams = new Array<Team>();
        this.invitations = new Array<InvitationViewDto>();
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

export interface InvitationViewDto {
    id: string;
    ownerName: string;
    teamId: string;
    title: string;
    color: string;
    icon: string;

    comments: string;
    status: InvitationStatus;
    actionDate: Date | string;
    created: Date | string;

    isMember: boolean;

}

export enum UserRole {
    Admin,
    User,
    GM
}
