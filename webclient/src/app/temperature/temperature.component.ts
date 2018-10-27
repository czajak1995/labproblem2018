import { Component } from '@angular/core';
import { TemperatureService } from '../temperature.service';
import { Chart } from 'chart.js';
import { Temperature } from '../temperature.service'



@Component({
  selector: 'app-temperature',
  templateUrl: './temperature.component.html',
  styleUrls: ['./temperature.component.css']
})
export class TemperatureComponent {
  chart = []; // This will hold our chart info
  minTemp: Temperature;
  maxTemp: Temperature;
  avgTemp: number;

  constructor(private _weather: TemperatureService) {}

  ngOnInit() {

    this._weather.getMinTemp()
      .subscribe(res=> {
          this.minTemp = res;
      })

    this._weather.getMaxTemp()
      .subscribe(res=> {
          this.maxTemp = res;
      })

    this._weather.getAvgTemp()
      .subscribe(res=> {
          this.avgTemp = res;
      })

    this._weather.getAllTemp()
      .subscribe(res => {
        let weeks = []

        for(var i = 0; i < res.length; i++) weeks.push(i) 

        this.chart = new Chart('canvas', {
          type: 'line',
          data: {
            labels: weeks,
            datasets: [
              { 
                label: "Temperatures",
                data: res,
                borderColor: "#3cba9f",
                fill: false
              },
            ]
          },
          options: {
            responsive: false,
            legend: {
              display: true
            },
            scales: {
              yAxes: [{
                ticks :{
                  suggestedMin: Math.min.apply(Math, res) - 1,
                  suggestedMax: Math.max.apply(Math, res) + 1,
                  display:true,
                  padding: 10
                },
                gridLines:{
                  drawTicks:false,
                  drawBorder:false
                },
                scaleLabel:{
                  labelString: "Temperature",
                  display:true
                }
              }],
              xAxes: [{
                ticks :{
                  display:true,
                  padding: 10
                },
                gridLines:{
                  display:false,
                  drawTicks:false,
                  drawBorder:false
                },
                scaleLabel:{
                  labelString: "Week",
                  display:true
                }
              }]
            },
            layout: {
              padding: {
                  left: 50,
                  right: 50,
                  top: 50,
                  bottom: 50
              }
          }
          }
        });
      })
      
  }
  
}


