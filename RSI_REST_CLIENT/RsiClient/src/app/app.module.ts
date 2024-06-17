import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { EventService } from './services/event.service';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { provideAnimations } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { InformationComponent } from './components/information/information.component';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [AppComponent, InformationComponent],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    TableModule,
    ButtonModule,
    DynamicDialogModule,
    CardModule,
    InputTextModule,
    CalendarModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    EventService,
    HttpClient,
    provideHttpClient(),
    DialogService,
    provideAnimations(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
