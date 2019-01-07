import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Team } from 'src/app/model';
import { LoaderService } from 'src/app/shared/loader.service';
import { ToastrService } from 'src/app/toastr.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-team-details',
  templateUrl: './team-details.component.html',
  styleUrls: ['./team-details.component.sass']
})
export class TeamDetailsComponent implements OnInit {
  @Input() team: Team;
  @Output() actionPerfomed = new EventEmitter<Team>();
  teams = new Array<Team>();
  constructor(
    private loader: LoaderService,
    private toastr: ToastrService,
    private service: UserService
  ) { }

  ngOnInit() {

  }

  onIconPickerSelect(icon: any) {
    this.team.icon = icon;
  }

  saveTeam() {
    this.team.id ? this.editTeam() : this.insertTeam();
  }

  insertTeam() {
    this.service.createTeam(this.team).subscribe(res => {
      this.loader.hide();
      this.team = res;
      this.emitAction();
    }, error => {
      this.loader.hide();
    });
  }

  editTeam() {
    this.service.updateTeam(this.team).subscribe(res => {
      this.loader.hide();
      this.team = res;
      this.emitAction();
    }, error => {
      this.loader.hide();
    });
  }

  emitAction() {
    this.actionPerfomed.emit(this.team);
  }

}
