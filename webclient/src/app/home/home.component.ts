import { Component, OnInit } from '@angular/core';
import { TemperatureService, TemperatureInfo } from '../temperature.service';
import { DeviceService, Device } from '../device.service';
import { Chart } from 'chart.js';
import { ChartConfig } from "../utils/chart-config"
import { saveAs } from 'file-saver';

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

    async ngOnInit() {
        await this._device.getAllDevices().toPromise().then(async (devices) => {
            this.devices = devices
            for (var device of devices) {
                await this._temperature.getTemperatureInfoForDevice(device.Id).toPromise().then(async (info) => {
                    this.avgTemperatures.push(info.Average)
                    if (this.avgTemperatures.length == this.devices.length) {
                        this.drawChart(this.avgTemperatures, this.devices)
                    }
                })
            }
        })

    }

    drawChart(temperatures, devices) {
        this.chart = new Chart('canvas',
            ChartConfig.prepareChart('bar', '', 'Device', 'Temperature', Math.max.apply(Math, temperatures) - 15, 
            Math.max.apply(Math, temperatures) + 1, devices.map(device => device.Name), temperatures));
    }

    onExportDataClick() {
        this._temperature.downloadExcelFile().subscribe(
            (data) => {
                if(data) {
                    const myBlob: Blob = new Blob([(<any>data)]);
                    saveAs(myBlob, 'SampleExcel.xlsx');
                }
            }, 
            (err) => {
                console.log("Can't download excel file")
            }
        )
    }
}
