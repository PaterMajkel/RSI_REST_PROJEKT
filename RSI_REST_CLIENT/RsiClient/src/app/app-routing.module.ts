import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { InformationComponent } from './components/information/information.component';

const routes: Routes = [
  { path: '*', component: AppComponent },
  { path: 'Events', component: AppComponent },
  { path: 'Events/{id}/Information', component: InformationComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
