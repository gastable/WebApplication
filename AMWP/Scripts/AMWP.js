﻿//宣告全域變數
var symbol;  //資產代號
var interval;  //成交資料週期
var dates = [];  //成交日期
var closes = [];  //收盤價
var adjCloses = [];  //調整收盤價
var volumes = [];  //成交量
var dividends = [];  //配息
var CCY1;  //原幣
var CCY2;  //兌外幣
var exRate; //匯率
var exRate1; //匯率1
var exRate2; //匯率2
var exRate3; //匯率3
var usdTotal;
var amount1;
var amount2;
var amount3;
function searchEndpoint(symbol) {
    $.ajax({        
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=' + symbol + '&apikey=XBRCUGRFRDK9P0HJ',
        async:false,
        success: function (data) {
            /*console.log(data)*/
            if (data["Note"] != null) {
                alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
            };
        },
        error: function () {
            alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
        }
    });
};


//轉換匯率
function CCY1toCCCY2(CCY1,CCY2) {
    $.ajax({
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency='+CCY1+'&to_currency='+CCY2+'&apikey=XBRCUGRFRDK9P0HJ',
        async:false,
        success: function (data) {
            //console.log(data);
            if (data["Note"] != null) {
                alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
            };
            exRate = data["Realtime Currency Exchange Rate"]["5. Exchange Rate"];
        },
        error: function () {
            alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
        }
    });
};


//外幣兌台幣匯率
function toTWD() {
    CCY1toCCCY2("USD", "TWD");    
    exRate1 = exRate;
    CCY1toCCCY2("EUR", "TWD"); 
    exRate2 = exRate;
    CCY1toCCCY2("JPY", "TWD");
    exRate3 = exRate;
};


//外幣兌美元匯率
function toUSD(amount1,amount2,amount3) {
    CCY1toCCCY2("TWD", "USD");
    exRate1 = exRate;
    usdTotal = amount1 * exRate1;
    CCY1toCCCY2("EUR", "USD");
    exRate2 = exRate;
    usdTotal += amount2 * exRate2;
    CCY1toCCCY2("JPY", "USD");
    exRate3 = exRate;
    usdTotal += amount3 * exRate3;    
};

//外幣兌歐元匯率
function toUSD() {
    CCY1toCCCY2("USD", "EUR");
    exRate1 = exRate;
    CCY1toCCCY2("TWD", "USD");
    exRate2 = exRate;
    CCY1toCCCY2("JPY", "USD");
    exRate3 = exRate;
};

//外幣兌日幣匯率
function toUSD() {
    CCY1toCCCY2("USD", "JPY");
    exRate1 = exRate;
    CCY1toCCCY2("TWD", "JPY");
    exRate2 = exRate;
    CCY1toCCCY2("EUR", "JPY");
    exRate3 = exRate;
};


//取資產成交資料
function getTimeSeries(symbol) {
    $.ajax({
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol=' + symbol + '&apikey=XBRCUGRFRDK9P0HJ',
        async: false,
        success: function (data) {
            if (data["Note"] != null) {
                alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
            };
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
        },
        error: function () {
            alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
        }
    });
};

//取資產成交資料+指定時間
function getTimeSeries(symbol, interval) {
    $.ajax({
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=TIME_SERIES_' + interval + '_ADJUSTED&symbol=' + symbol + '&apikey=XBRCUGRFRDK9P0HJ',
        async: false,
        success: function (data) {
            var i = 0;
            $.each(data, function (objectTitle, objects) {
                if (data["Note"] != null) {
                    alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
                };
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
        },
        error: function () {
            alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
        }
    });
};

//圓形圖
function CircleChart(id, chartType, dataLegends, dataset) {
const data = {
    labels: dataLegends,
    datasets: [{
        label: ['現值'],
        data: dataset,
        backgroundColor: ["#b2e061", "#bd7ebe", "#ffb55a", "#ffee65", "#beb9db", "#fdcce5", "#8bd3c7", "#fd7f6f", "#7eb0d5"],
        //borderColor: [
        //    'rgba(54, 162, 235, 1)',
        //    'rgba(255, 206, 86, 1)',
        //    'rgba(75, 192, 192, 1)',
        //    'rgba(153, 102, 255, 1)',
        //    'rgba(255, 159, 64, 1)',
        //    'rgba(255, 26, 104, 1)'
        //]
    }]
};
    var legendON = dataset.length<5?true:false;
    
const config = {
    type: chartType,
    data,
    options: {
        maintainAspectRatio: true,
        plugins: { 
            datalabels: {
                align: 'center',
                formatter: (value, context) => {
                    const datapoints = context.chart.config.data.datasets[0].data;
                    //console.log(datapoints);
                    const totalvalue = datapoints.reduce((total, datapoint) => total + datapoint,
                        0);
                    const percentageValue = (value / totalvalue * 100).toFixed(2);
                    const display = [`${percentageValue}%`]
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

//基本線圖
function LineChart(id, dataLegends, dataset) {
    const data = {
        labels: dataLegends,
        datasets: [{
            label: ['現值'],
            data: dataset,
            borderWidth: 1,
            pointRadius: 0
        }]
    };
    var legendON = dataset.length < 5 ? true : false;

    const config = {
        type: 'line',
        data,
        options: {
            scales: {
                    x: { grid: { drawOnChartArea: false } },
                    y: {
                        beginAtZero: false,
                    }
                },
            plugins: {                
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
        }       
    };
    const myChart = new Chart(document.getElementById(id),
        config);
}

//年化報酬率(%)
function getCAGR(dates,prices) {
    const date1 = new Date(dates[0]);
    const date2 = new Date(dates[dates.length - 1]);
    const price1 = prices[0];
    const price2 = prices[prices.length - 1];
    const timeDiff = date2.getTime() - date1.getTime();
    const yearDiff = timeDiff / (1000 * 3600 * 24 * 365);
    const CAGR = ((Math.pow((price2 / price1), (1 / yearDiff))) - 1)*100;
    return CAGR;
};
//總報酬率
function getGrossReturn(date1, date2, price1, price2) {
    const timeDiff = date2.getTime() - date1.getTime();    
    const grossReturn = (price2 / price1) - 1;
    return grossReturn;
};


//資產表現圖表-原價
function symbolLineChart(symbol) {
    $.ajax({
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol=' + symbol + '&apikey=XBRCUGRFRDK9P0HJ',
        async:false,
        success: function (data) {            
            var i = 0;
            $.each(data, function (objectTitle, objects) {
                if (data["Note"] != null) {
                    alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
                };
                //console.log(data);
                if (i >= 1) {
                    $.each(objects, function (objectKey, objectValue) {
                        dates.push(objectKey);
                        closes.push(objectValue['4. close']);
                        adjCloses.push(objectValue['5. adjusted close']);
                        volumes.push(objectValue['6. volume']);
                        dividends.push(objectValue['7. dividend amount']);
                    });
                };                
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
                        x: { grid: { drawOnChartArea: false } },
                        y: {
                            beginAtZero: false,
                        }
                    }
                }
            });
        },
        error: function () {
            alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
        }
    });
};

//資產表現圖表-指定時間
function symbolLineChart(symbol, interval) {
    $.ajax({
        type: 'get',
        url: 'https://www.alphavantage.co/query?function=TIME_SERIES_' + interval + '_ADJUSTED&symbol=' + symbol + '&apikey=XBRCUGRFRDK9P0HJ',
        async: false,
        success: function (data) {
            var i = 0;
            $.each(data, function (objectTitle, objects) {
                if (data["Note"] != null) {
                    alert('市場資料提供網站發生錯誤，請稍後再使用，造成您的不便請見諒！');
                };
                //console.log(data);
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
                        x: { grid: { drawOnChartArea: false } },
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