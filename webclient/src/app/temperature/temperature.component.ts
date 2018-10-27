import { Component } from '@angular/core';
import { TemperatureService } from '../temperature.service';
import { Chart } from 'chart.js';
import { Temperature } from '../temperature.service'
import { DeviceService, Device } from '../device.service';
import { ChartConfig } from "../utils/chart-config"


@Component({
  selector: 'app-temperature',
  templateUrl: './temperature.component.html',
  styleUrls: ['./temperature.component.css']
})
export class TemperatureComponent {
  minTemp: Temperature;
  maxTemp: Temperature;
  avgTemp: number;
  chart: Chart;
  devices : Device[] = [];
  deviceSelected: number;

  constructor(private _weather: TemperatureService, private _device: DeviceService) {}

  ngOnInit() {

    this._device.getAllDevices().subscribe(res => {
      res.forEach(device => {
        this.devices.push(device)
      });
    })
    this.deviceSelected = 1;

    this._weather.getAllTempForDevice(this.deviceSelected)
      .subscribe(res => {
        this.drawChart(res)
      })
      
  }

  onDeviceSelected(id:number) {
    this._weather.getAllTempForDevice(id).subscribe(res => {
      this.drawChart(res)
    })
  }

  drawChart(res:any[]) {
    let weeks = []

    for(var i = 0; i < res.length; i++) weeks.push(i) 

    this.chart = new Chart('canvas', 
      ChartConfig.prepareChart('line', '', 'Weeks', 'Temperature', Math.min.apply(Math, res), Math.max.apply(Math, res) + 10, weeks, res, '#0f060e')
      );
  }
  
}


