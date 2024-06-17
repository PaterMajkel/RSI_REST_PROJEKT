import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class EventService {

  private apiUrl = 'https://localhost:7278/'; // Replace with your actual API URL
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Basic ' + btoa('dobry:user') // Replace with your actual username and password
    })
  };

  constructor(private http: HttpClient) { }

  getEventsForDay(date: Date): Observable<any> {
    return this.http.get(`${this.apiUrl}/Events`, {
      ...this.httpOptions,
      params: { date: date.toISOString() }
    });
  }

  getEventsForWeek(date: Date): Observable<any> {
    return this.http.get(`${this.apiUrl}/EventsForWeek`, {
      ...this.httpOptions,
      params: { date: date.toISOString() }
    });
  }

  getEventInformation(eventId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/EventInformation/${eventId}`, this.httpOptions);
  }

  addEvent(name: string, type: string, date: Date, description: string): Observable<any> {
    const body = { name, type, date, description };
    return this.http.post(`${this.apiUrl}/AddEvent`, body, this.httpOptions);
  }

  modifyEventInformation(eventId: number, name: string, type: string, date: Date, description: string): Observable<any> {
    const body = { name, type, date, description };
    return this.http.put(`${this.apiUrl}/ModifyEventInformation/${eventId}`, body, this.httpOptions);
  }

  getAllEvents(): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetAllEvents`, this.httpOptions);
  }

}
