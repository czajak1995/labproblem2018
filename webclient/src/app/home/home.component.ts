import { Component, OnInit } from '@angular/core';
import { TemperatureService, TemperatureInfo } from '../temperature.service';
import { DeviceService, Device } from '../device.service';
import { Chart } from 'chart.js';
import { map, timeInterval, take, delay } from 'rxjs/operators';
import { of, Observable, forkJoin, interval, pipe } from 'rxjs';
import { ChartConfig } from "../utils/chart-config"
import { when } from 'q';
import { async } from '@angular/core/testing';
import { waitForMap } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  devices: Device[] = [];
  avgTemperatures: number[] = [];
  chart: Chart;

  constructor(private _temperature: TemperatureService, 
    private _device: DeviceService) { }

  ngOnInit() {

  //   this._device.getAllDevices().subscribe(res => {
  //     this.devices = res
  //     forkJoin(this.devices.map((device) => {
  //       this._temperature.getTemperatureInfoForDevice(device.Id)
  //       .subscribe(res => {
  //         this.avgTemperatures.push(res.Average)
  //         console.log(this.avgTemperatures)
  //         console.log(this.avgTemperatures.length)
  //       })
  //     }
  //     ))

  //   })
  //   pipe(delay(6000))
  //   // while(this.avgTemperatures.length != 5);
  //   console.log("dupa")
  //   console.log(this.avgTemperatures)
  //   this.chart = new Chart('canvas', 
  //   ChartConfig.prepareChart('bar', '', 'Device', 'Temperature', 50, Math.max.apply(Math, this.avgTemperatures) + 1,
  //    this.devices.map(device => device.Name), this.avgTemperatures)
  // );




    this._device.getAllDevices().subscribe(res => {
      this.devices = res
    })



        
    this._temperature.getAverageTemperatures()
    .subscribe(res => {
      this.avgTemperatures = res

      let weeks = []

      for(var i = 0; i < this.avgTemperatures.length; i++) weeks.push(i) 

      this.chart = new Chart('canvas', 
        ChartConfig.prepareChart('bar', '', 'Device', 'Temperature', 50, Math.max.apply(Math, this.avgTemperatures) + 1,
        this.devices.map(device => device.Name), this.avgTemperatures)
      );
    })

    
  }

}
