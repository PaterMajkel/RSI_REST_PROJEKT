import { Component, OnInit } from '@angular/core';
import { EventService } from './services/event.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(public eventSrv: EventService){

  }
  ngOnInit(): void {
  }
  getOne = this.eventSrv.getAllEvents().pipe(take(1))
}
