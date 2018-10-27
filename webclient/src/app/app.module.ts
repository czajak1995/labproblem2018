import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TemperatureService } from './temperature.service';
import { HttpClientModule } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TemperatureComponent } from './temperature/temperature.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TemperatureComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'temperature', component: TemperatureComponent, pathMatch: 'full' }
])
  ],
  providers: [TemperatureService],
  bootstrap: [AppComponent]
})
export class AppModule { }
