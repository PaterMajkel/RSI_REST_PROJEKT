import { Component, OnInit } from '@angular/core';
import { EventService } from './services/event.service';
import { take } from 'rxjs';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { InformationComponent } from './components/information/information.component';
import { Router } from '@angular/router';
import { EventDt } from './models/event';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  ref: DynamicDialogRef | undefined;
  constructor(
    public eventSrv: EventService,
    private dialogService: DialogService,
  ) {}

  public date = new Date
  ngOnInit(): void {}
  getOne = this.eventSrv.getAllEvents().pipe(take(1));

  show(link: string) {
    this.ref = this.dialogService.open(InformationComponent, {
      data: { link: link },
      width: '50%',
    });
    this.ref.onClose.subscribe((data: any) => {
      if (data?.put) {
        this.eventSrv.modifyEventInformation(
          data.event.id,
          data.event.name,
          data.event.details.type,
          data.event.details.date,
          data.event.details.description
        ).subscribe(p=>p);
      }
    });
  }

  getWeek(week: Date){
    this.getOne = this.eventSrv.getEventsForWeek(week).pipe(take(1));
  }

  getDay(day: Date){
    this.getOne = this.eventSrv.getEventsForDay(day).pipe(take(1));
  }


  delete(id: number){
    this.eventSrv.removeEvent(id).subscribe(p=>p)
    window.location.reload();
  }

  addEvent(){
    this.ref = this.dialogService.open(InformationComponent, {
      width: '50%',
    });
    this.ref.onClose.subscribe((data: any) => {
      if (data?.put) {
        this.eventSrv.addEvent(
          data.event.name,
          data.event.details.type,
          data.event.details.date,
          data.event.details.description
        ).subscribe(p=>window.location.reload());
      }
    });
  }
  getReport(events: EventDt[]){
    this.eventSrv.getReport(events.map(p=>p.id)).subscribe(p=>{
      saveAs(p, "raport.pdf")
      // console.log(p)
      // this.saveFile(p)
    })
  }

  saveFile(p: any){
    console.log(p)
    const data = window.URL.createObjectURL(p.data);
      var blob = new Blob([p.blob()], {type: 'application/pdf'});
      var link = document.createElement('a');
      link.href = data
      link.download = p.fileName
      link.dispatchEvent( new MouseEvent('click', {bubbles: true, cancelable: true, view: window}))

      setTimeout(function() {
        window.URL.revokeObjectURL(data)
        link.remove()
      }, 100)
  }
}
