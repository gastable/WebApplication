function searchEndpoint(symbol) {
    $.ajax({
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords='+symbol+'&apikey=XBRCUGRFRDK9P0HJ',
        success: function (data) {
        //    console.log(data)
        },
        error: function () {
            alert('error');
        }
    });
};






function CircleChart(id, chartType, dataLegends, dataset, tooltipON) {
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

//年化報酬率
function getCAGR(date1, date2,price1,price2) {
    const timeDiff = date2.getTime() - date1.getTime();
    const yearDiff = timeDiff / (1000 * 3600 * 24 * 365);
    const CAGR = (Math.pow((price2 / price1), (1 / yearDiff))) - 1;
    return CAGR;
};
//總報酬率
function getGrossReturn(date1, date2, price1, price2) {
    const timeDiff = date2.getTime() - date1.getTime();    
    const grossReturn = (price2 / price1) - 1;
    return grossReturn;
};

//資產表現圖表
function symbolLineChart(symbol, interval) {
    $.ajax({
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=TIME_SERIES_' + interval + '_ADJUSTED&symbol=' + symbol + '&apikey=XBRCUGRFRDK9P0HJ',
        success: function (data) {
            var dates = [];
            var closes = [];
            var adjCloses = [];
            var volumes = [];
            var dividends = [];
            //console.log(data);
            var i = 0;
            $.each(data, function (objectTitle, objects) {
                if (i >= 1) {
                    $.each(objects, function (objectKey, objectValue) {
                        dates.push(objectKey);
                        closes.push(objectValue['4. close']);
                        adjCloses.push(objectValue['5. adjusted close']);
                        volumes.push(objectValue['6. volume']);
                        dividends.push(objectValue['7. dividend amount']);
                    });
                }
                i++;
            });
            dates.reverse();
            closes.reverse();
            adjCloses.reverse();
            volumes.reverse();
            dividends.reverse();

            //年化報酬率
            const date1 = new Date(dates[0]);
            const date2 = new Date(dates[dates.length - 1]);
            const price1 = adjCloses[0];
            const price2 = adjCloses[adjCloses.length - 1];
            const CAGR = getCAGR(date1, date2, price1, price2).toFixed(4);
            
            
            //畫圖
            const ctx = document.getElementById('symbolLineChart');
            new Chart(ctx, {
                type: 'line',
                data: {
                    labels: dates,
                    datasets: [{
                        label: data['Meta Data']['2. Symbol'],
                        data: closes,
                        borderWidth: 1,
                        pointRadius: 0
                    }]
                },
                options: {
                    scales: {
                        x: {grid: {drawOnChartArea: false}},
                        y: {
                            beginAtZero: false,
                            
                            
                        }
                    }
                }
            });
        },
        error: function () {
            alert('error');
        }
    });
};

