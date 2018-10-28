import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { of } from "rxjs";
import { AppComponent, BASE_URL } from './app.component';

@Injectable({
  providedIn: 'root'
})
export class TemperatureService {


  constructor(private http: HttpClient) { }

  getAllTemp() {
    if(!AppComponent.isMock()) {
      return this.http.get<number[]>(BASE_URL + "temperature/all")
        .pipe(result => result);
    } else {
      let tab = [0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1]
      return of<number[]>(tab).pipe(map(result => result))   
    }
  }

  getAvgTempForDevice(id:number) {
    if(!AppComponent.isMock()) {
      return this.http.get<number>(BASE_URL + "temperature/")
    } else {
      let temperature: number = (id+1)*20;
      return of<number>(temperature).pipe(map(result => result))
    }
  }

  getAllTempForDevice(id:number) {
    
    if(!AppComponent.isMock()) {
      return this.http.get<number[]>(BASE_URL + "temperature/all/" + id)
    } else {
      let tab = [0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1]
      let resultTab = []
      tab.forEach(element => {
        resultTab.push(id*element)
      });
      return of<number[]>(resultTab).pipe(result => result)
    }
  }
  
}

export interface Temperature {
  Id: number;
  Temp: number;
  Date: Date;
}