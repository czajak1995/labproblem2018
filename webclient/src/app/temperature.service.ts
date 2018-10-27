import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { filter, map, take, toArray } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TemperatureService {

  constructor(private http: HttpClient) { }

  getAllTemp() {
    return this.http.get<number[]>("http://192.168.43.114:3002/api/temperature/all")
      .pipe(result => result);
  }

  getMinTemp() {
    return this.http.get<Temperature>("http://192.168.43.114:3002/api/temperature/min")
    .pipe(result => result);
  }

  getMaxTemp() {
    return this.http.get<Temperature>("http://192.168.43.114:3002/api/temperature/max")
    .pipe(result => result);
  }

  getAvgTemp() {
    return this.http.get<number>("http://192.168.43.114:3002/api/temperature/average")
    .pipe(result => result);
  }
  
}

export interface Temperature {
  Id: number;
  Temp: number;
  Date: Date;
}