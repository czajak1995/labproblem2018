
export class ChartConfig {

    private static chart: {};

    static prepareChart(type:string, title:string, labelX:string, 
        labelY:string, minY:number, maxY:number, xData:any[], yData:any[], color="#BBBBBB") {
        this.chart = {
            type: type,
            data: this.addData(title, xData, yData, color),
            options: {
              responsive: true,
              maintainAspectRatio: false,
              legend: {
                display: true
              },
              scales: {
                yAxes: this.prepareYAxis(labelY, minY, maxY),
                xAxes: this.prepareXAxis(labelX)
              },
              layout: this.prepareLayout()
            }
          }
        
        return this.chart
    }

    private static prepareXAxis(label:string) {
        return [{
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
              labelString: label,
              display:true
            }
          }]
    }

    private static prepareYAxis(label:string, min:number=0, max:number=100) {
        return [{
            ticks :{
              suggestedMin: min,
              suggestedMax: max,
              display:true,
              padding: 10
            },
            gridLines:{
              drawTicks:false,
              drawBorder:false
            },
            scaleLabel:{
              labelString: label,
              display:true
            }
          }]
    }

    private static prepareLayout() {
        return {
            padding: {
                left: 50,
                right: 50,
                top: 50,
                bottom: 50
            }
        }
    }

    private static addData(title:string, labels:any[], data:any[], color:string) {
        return {
            labels: labels,
            datasets: [
              { 
                label: "Temperatures",
                data: data,
                borderColor: color,
                fill: false
              },
            ]
          }
    }
}