import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { of, Observable } from "rxjs";
import { AppComponent, BASE_URL } from './app.component';

@Injectable({
    providedIn: 'root'
})
export class TemperatureService {


    constructor(private http: HttpClient) { }

    getTemperatureInfoForDevice(id: number): Observable<TemperatureInfo> {
        return this.http.get<TemperatureInfo>(BASE_URL + "temperature/info?deviceId=" + id).pipe(result => result)
    }

    getAllTempForDevice(id: number) {
        return this.http.get<number[]>(BASE_URL + "temperature/all?deviceId=" + id)
    }

    getAverageTemperatures() {
        return this.http.get<number[]>(BASE_URL + "temperature/avgs")
    }

    downloadExcelFile() {
        return this.http.get(BASE_URL + '/temperature/export', { responseType: 'blob'});
        // const httpOptions = {
        //     headers: new HttpHeaders({ 'responseType':  'ResponseContentType.Blob',
        //     'Content-Type':  'application/vnd.ms-excel'})};
        //   return this.http.get(BASE_URL + '/temperature/export', httpOptions);
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