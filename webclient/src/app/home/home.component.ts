import { Component, OnInit } from '@angular/core';
import { TemperatureService } from '../temperature.service';
import { DeviceService, Device } from '../device.service';
import { Chart } from 'chart.js';
import { map } from 'rxjs/operators';
import { ChartConfig } from "../utils/chart-config"

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  devices: Device[] = [];
  avgTemperatures: number[] = [];
  chart: Chart;

  constructor(private _weather: TemperatureService, 
    private _device: DeviceService) { }

  ngOnInit() {
     this._device.getAllDevices()
     .subscribe(res => {
      this.devices = res;
    })

    for(var i = 0; i < this.devices.length; i++) {
      this._weather.getAvgTempForDevice(<number>i).subscribe(result => {
        this.avgTemperatures.push(<number>result)
      })
    }

    let weeks = []

    for(var i = 0; i < this.avgTemperatures.length; i++) weeks.push(i) 

    this.chart = new Chart('canvas', 
      ChartConfig.prepareChart('bar', '', 'Device', 'Temperature', 0, Math.max.apply(Math, this.avgTemperatures) + 1,
       this.devices.map(device => device.Name), this.avgTemperatures)
    );



  }

}
