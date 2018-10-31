import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { of, Observable } from "rxjs";
import { AppComponent, BASE_URL } from './app.component';

@Injectable({
  providedIn: 'root'
})
export class TemperatureService {


  constructor(private http: HttpClient) { }

  getTemperatureInfoForDevice(id:number): Observable<TemperatureInfo> {
    if(!AppComponent.isMock()) {
      return this.http.get<TemperatureInfo>(BASE_URL + "temperature/info?deviceId=" + id).pipe(result => result)
    }
    // } else {
    //   let temperature: number = (id+1)*20;
    //   return of<number>(temperature).pipe(map(result => result))
    // }
  }

  getAllTempForDevice(id:number) {
    
    if(!AppComponent.isMock()) {
      return this.http.get<number[]>(BASE_URL + "temperature/all?deviceId=" + id)
    } else {
      let tab = [0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1]
      let resultTab = []
      tab.forEach(element => {
        resultTab.push(id*element)
      });
      return of<number[]>(resultTab).pipe(result => result)
    }
  }

  getAverageTemperatures() {
    if(!AppComponent.isMock()) {
      return this.http.get<number[]>(BASE_URL + "temperature/avgs")
    } else {
    }
  }
  
}

export interface Temperature {
  Id: number;
  Temp: number;
  Date: Date;
}

export interface TemperatureInfo {
  Min: Temperature;
  Max: Temperature;
  Average: number;
}