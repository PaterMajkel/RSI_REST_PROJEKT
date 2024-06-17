import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Observable, of, take } from 'rxjs';
import { EventDt } from 'src/app/models/event';
import { EventService } from 'src/app/services/event.service';

@Component({
  selector: 'app-information',
  templateUrl: './information.component.html',
  styleUrls: ['./information.component.css'],
})
export class InformationComponent implements OnInit {
  public form: FormGroup = new FormGroup({
    id: new FormControl(),
    name: new FormControl(''),
    date: new FormControl(),
    type: new FormControl(''),
    description: new FormControl(''),
  });
  constructor(
    private eventSrv: EventService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public route: ActivatedRoute
  ) {
    var content = config.data?.link;
    if (content) this.linkedEvent = this.eventSrv.useLink(content);
    else if(this.route.snapshot.paramMap.get('id'))
      this.linkedEvent = this.eventSrv.getEventInformation(
        +this.route.snapshot.paramMap.get('id')!
      );

    if (this.linkedEvent)
      this.linkedEvent.pipe(take(1)).subscribe((p) =>
        this.form.setValue({
          id: p.id,
          name: p.name,
          type: p.details.type,
          date: new Date(p.details.date),
          description: p.details.description,
        })
      );
      else
        this.linkedEvent = of({id: 0} as EventDt)
  }
  linkedEvent?: Observable<EventDt>;
  ngOnInit() {}

  closeDialog() {
    let form = this.form.getRawValue();
    this.ref.close({
      put: true,
      event: {
        id: form.id,
        name: form.name,
        details: {
          description: form.description,
          date: form.date,
          type: form.type,
        },
      } as EventDt,
    });
  }
}
