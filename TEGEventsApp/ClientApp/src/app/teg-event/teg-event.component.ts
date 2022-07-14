import { Component, Inject } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/angular';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-teg-event',
  templateUrl: './teg-event.component.html'
})

export class TEGEventsComponent {
  eventList: object[] = [];
  calendarOptions: CalendarOptions | undefined;
  public tegevents: TEGEvent[] = [];
  public tegvenues: TEGVenue[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  calendarEvents: TEGEvent[] = [];
  ngOnInit() {
    this.http.get<TEGEventVenue>(this.baseUrl + 'tegeventsvenues').subscribe(result => {

      this.tegevents = result.events;
      for (let ev of result.events) {
        this.eventList.push({
          id: ev.id.toString(),
          start: new Date(ev.startDate).getTime()
        });
      }

      this.tegvenues = result.venues;
    }, error => console.error(error));

    setTimeout(() => {
      let calendarEvents = this.tegevents;

      this.calendarOptions = {
        initialView: 'dayGridMonth',
        events: this.eventList,
        eventClick: function (event) {
          var calEvent = calendarEvents.find(x => x.id.toString() === event.event.id);

          if (calEvent != null) {
            var description = (calEvent.description == undefined || calEvent.description == null) ? "" : calEvent.description;
            alert(new Date(calEvent.startDate).toLocaleString() + "\n" + calEvent.name + "\n" + calEvent.venueName + "\n" + calEvent.location + "\n\n" + description);
          }
        }
      };
    }, 1000);
  }
}


interface TEGEvent {
  id: number;
  name: string;
  description: string;
  startDate: Date;
  venueId: number;
  venueName: string;
  location: string;
  capacity: number;
}

interface TEGVenue {
  id: number;
  name: string;
  capacity: number;
  location: string;
}

interface TEGEventVenue {
  events: TEGEvent[];
  venues: TEGVenue[];
}
