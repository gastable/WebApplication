function getAPI(symbol, interval) {
    $.ajax({
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=TIME_SERIES_' + interval + '_ADJUSTED&symbol=' + symbol + '&apikey=XBRCUGRFRDK9P0HJ',
        success: function (data) {
            /*console.log(data);*/
            return data
        },
        error: function () {
            alert('error');
        }
    });
}


function getAPIDates(api) { 
        var chartLegends = [];
        var dataPoints = [];
        var i = 0;
        $.each(api, function (objectTitle, objects) {
            if (i >= 1) {
                $.each(objects, function (objectKey, objectValue) {
                    chartLegends.push(objectKey);
                    dataPoints.push(objectValue['4. close']);
                });
            }
            i++;
        });
        //console.log(chartLegends.reverse());
        //console.log(dataPoints.reverse());
        //console.log(data['Meta Data']['2. Symbol']);
        chartLegends.reverse();
        dataPoints.reverse();
        return [chartLegends, dataPoints];
}  

    //console.log(api);    


function getCircleChart(id, chartType, dataLegends, dataset, tooltipON) {
const data = {
    labels: dataLegends,
    datasets: [{
        label: ['現值'],
        data: dataset,
        backgroundColor: ['rgba(255, 26, 104,0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(255, 206, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(255, 159, 64, 0.5)'
            
        ],
        borderColor: [
            'rgba(255, 26, 104, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
        ]
    }]
};
    var legendON = dataset.length<5?true:false;
    
const config = {
    type: chartType,
    data,
    options: {
        maintainAspectRatio: true,
        plugins: {
            tooltip: { enabled: tooltipON },
            datalabels: {
                align: 'center',
                formatter: (value, context) => {
                    const datapoints = context.chart.config.data.datasets[0].data;
                    //console.log(datapoints);
                    const assets = context.chart.config.data.labels[context.dataIndex];
                    const totalvalue = datapoints.reduce((total, datapoint) => total + datapoint,
                        0);
                    const percentageValue = (value / totalvalue * 100).toFixed(2);
                    const display = [`${assets}`, `${percentageValue}%`]
                    return display;
                }
            },
        legend: {
            display: legendON,
            onHover: (event, chartElement) => {
                event.native.target.style.cursor = 'pointer';
            },
            onLeave: (event, chartElement) => {
                event.native.target.style.cursor = 'default';
            }
        }
        }
    },
    plugins: [ChartDataLabels]
};

const myChart = new Chart(document.getElementById(id),
    config);
}

//function getLineChart(id, dataset, tooltipON) {
//    const data = {
//        labels: data.map(row => row.year),
//        datasets: [{
//            label: ['現值'],
//            data: dataset,
//            backgroundColor: ['rgba(255, 26, 104,0.5)',
//                'rgba(54, 162, 235, 0.5)',
//                'rgba(255, 206, 86, 0.5)',
//                'rgba(75, 192, 192, 0.5)',
//                'rgba(153, 102, 255, 0.5)',
//                'rgba(255, 159, 64, 0.5)'

//            ],
//            borderColor: [
//                'rgba(255, 26, 104, 1)',
//                'rgba(54, 162, 235, 1)',
//                'rgba(255, 206, 86, 1)',
//                'rgba(75, 192, 192, 1)',
//                'rgba(153, 102, 255, 1)',
//                'rgba(255, 159, 64, 1)'
//            ],
//            pointRadius: 0
//        }]
//    };
//    var legendON = dataset.length < 5 ? true : false;

//    const config = {
//        type: 'line',
//        data,
//        options: {
//            scales: {
//                y: {
//                    beginAtZero: true
//                }
//            },
//            plugins: {
//                tooltip: { enabled: tooltipON },
//                datalabels: {
//                    align: 'center',
//                    formatter: (value, context) => {
//                        const datapoints = context.chart.config.data.datasets[0].data;
//                        console.log(datapoints);
//                        const assets = context.chart.config.data.labels[context.dataIndex];
//                        const totalvalue = datapoints.reduce((total, datapoint) => total + datapoint,
//                            0);
//                        const percentageValue = (value / totalvalue * 100).toFixed(2);
//                        const display = [`${assets}`, `${percentageValue}%`]
//                        return display;
//                    }
//                },
//                legend: {
//                    display: legendON,
//                    onHover: (event, chartElement) => {
//                        event.native.target.style.cursor = 'pointer';
//                    },
//                    onLeave: (event, chartElement) => {
//                        event.native.target.style.cursor = 'default';
//                    }
//                }
//            }
//        },
//        plugins: [ChartDataLabels]
//    };

//    const myChart = new Chart(document.getElementById(id),
//        config);
//}
