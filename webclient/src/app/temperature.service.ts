import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { of } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TemperatureService {


  constructor() { }

  getAllTemp() {

    // return this.http.get<number[]>(BASE_URL + "temperature/all")
    //   .pipe(result => result);
    let tab = [0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1]
    return of<number[]>(tab).pipe(map(result => result))   
  }

  getMinTemp() {
    // return this.http.get<Temperature>(this.BASE_URL + "temperature/min")
    // .pipe(result => result);
    let temperature: Temperature = {Id: 1, Temp: 20, Date: new Date()}
    return of<Temperature>(temperature).pipe(result => result)
  }

  getMaxTemp() {
    // return this.http.get<Temperature>(this.BASE_URL + "temperature/max")
    // .pipe(result => result);
    let temperature: Temperature = {Id: 1, Temp: 20, Date: new Date()}
    return of<Temperature>(temperature).pipe(result => result)
  }

  getAvgTemp() {
    // return this.http.get<number>(this.BASE_URL + "temperature/average")
    // .pipe(result => result);
    let temperature: number = 20;
    return of<number>(temperature).pipe(result => result)
  }

  getAvgTempForDevice(id:number) {
    let temperature: number = (id+1)*20;
    return of<number>(temperature).pipe(map(result => result))
  }

  getAllTempForDevice(id:number) {
    
    let tab = [0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1]
    let resultTab = []
    tab.forEach(element => {
      resultTab.push(id*element)
    });
    return of<number[]>(resultTab).pipe(map(result => result))   
  }
  
}

export interface Temperature {
  Id: number;
  Temp: number;
  Date: Date;
}