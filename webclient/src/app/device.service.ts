import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL, AppComponent } from './app.component';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  constructor(private http: HttpClient) { }

  getAllDevices() {
    if(!AppComponent.isMock()) {
      return this.http.get<Device[]>(BASE_URL + "device/all").pipe(result => result); 
    } else {
      let devices: Device[] = [
        {Id: 1, Name: "Wiertarka"},
        {Id: 2, Name: "Żelazko"},
        {Id: 3, Name: "Spawarka"},
        {Id: 4, Name: "Kuchenka"},
        {Id: 5, Name: "Kaloryfer"}]

      return of<Device[]>(devices).pipe(map(result => result))
    }
  }
}

export interface Device {
  Id: number;
  Name: string;
}