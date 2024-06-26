import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EventDt } from '../models/event';

@Injectable()
export class EventService {

  private apiUrl = 'https://localhost:7278/Event'; // Replace with your actual API URL
  private httpOptions = {
    headers: new HttpHeaders({
      'Authorization': 'Basic ' + btoa('dobry:user') // Replace with your actual username and password
    })
  };

  constructor(private http: HttpClient) { }

  getEventsForDay(date: Date): Observable<any> {
    return this.http.get(`${this.apiUrl}/EventsForDay`, {
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

  useLink(link: string): Observable<any>{
    return this.http.get(`${link}`, {
      ...this.httpOptions,
    });
  }

  getEventInformation(eventId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Events/${eventId}/Information`, this.httpOptions);
  }

  addEvent(name: string, type: string, date: Date, description: string): Observable<any> {
    const body = { name, type, date, description };
    return this.http.post(`${this.apiUrl}/AddEvent`, body, this.httpOptions);
  }

  modifyEventInformation(eventId: number, name: string, type: string, date: Date, description: string): Observable<any> {
    const body = { name, type, date, description };
    console.log(body)
    return this.http.put(`${this.apiUrl}/ModifyEventInformation/${eventId}`, body, this.httpOptions);
  }

  getAllEvents(): Observable<EventDt[]> {
    console.log(this.httpOptions)
    return this.http.get<EventDt[]>(`${this.apiUrl}/Events`, this.httpOptions);
  }
  removeEvent(eventId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${eventId}`, this.httpOptions);
  }

  getReport(ids: number[]): Observable<any> {
    let httpOpts = { ...this.httpOptions }
    httpOpts.headers = httpOpts.headers.append('Accept', 'application/pdf');
    httpOpts.headers = httpOpts.headers.append('Content-Type', 'application/json')
    return this.http.post<Blob>(`${this.apiUrl}/GetPdf`, {sentList: ids},{ ...httpOpts, responseType: 'blob' as 'json'});
  }
}
