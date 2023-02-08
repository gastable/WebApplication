//宣告全域變數
var symbol;  //資產代號
var interval;  //成交資料週期
var types; //資產類別
var assetNets; //資產淨值
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
var now = new Date();//取得現在時間
var year = now.getFullYear();//取得今年
var today = new Date(year.toString(), now.getMonth(), now.getDate()); //取得今天日期
var data;
var config;

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
function getTimeSeries(symbol,interval) {
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
                /*console.log(dates);*/
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
function CircleChart(id, chartType, types, assetNets) {
const data = {
    labels: types,
    datasets: [{
        label: ['現值'],
        data: assetNets,
        backgroundColor: ["#b2e061", "#bd7ebe", "#ffb55a", "#ffee65", "#beb9db", "#fdcce5", "#8bd3c7", "#fd7f6f", "#7eb0d5"],
        //borderColor: [
        //    'rgba(54, 162, 235, 1)',
        //    'rgba(255, 206, 86, 1)',
        //    'rgba(75, 192, 192, 1)',
        //    'rgba(153, 102, 255, 1)',
        //    'rgba(255, 159, 64, 1)',
        //    'rgba(255, 26, 104, 1)'
        //],
        
    }]
};
    var legendON = assetNets.length<5?true:false;
    
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
const chart = new Chart(document.getElementById(id),
    config);
}

//基本線圖
function LineChart(id, dates, closes) {
   const data = {
        labels: dates,
        datasets: [{
            label: ['現值'],
            data: closes,
            borderWidth: 1,
            pointRadius: 0
        }]
    };
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
                    display: true,
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
    const chart = new Chart(document.getElementById(id),
        config);
}

//基本長條圖
function BarChart(id, dates, dividends) {
    const data = {
        labels: dates,
        datasets: [{
            label: symbolstr,
            data: dividends,
            //borderColor: Utils.CHART_COLORS.blue,
            //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.blue, 0.5),
            borderWidth: 2,
            borderRadius: 5,
            /*borderSkipped: false,*/
        }]
    };
    const config = {
        type: 'bar',
        data,
        options: {
            scales: {
                x: { grid: { drawOnChartArea: false } },
                y: {
                    beginAtZero: true,
                }
            },
            plugins: {
                legend: {
                    display: true,
                    onHover: (event, chartElement) => {
                        event.native.target.style.cursor = 'pointer';
                    },
                    onLeave: (event, chartElement) => {
                        event.native.target.style.cursor = 'default';
                    },
                    /*position: 'top'*/
                }
            }
        }
    };
    const chart = new Chart(document.getElementById(id),
        config);   
}


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
                /*console.log(data);*/
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
                /*console.log(data);*/
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
            /*console.log(closes);*/            

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

//年化報酬率(%)
function getCAGR(dates, prices) {
    const date1 = new Date(dates[0]);
    const date2 = new Date(dates[dates.length - 1]);
    const price1 = prices[0];
    const price2 = prices[prices.length - 1];
    const timeDiff = date2.getTime() - date1.getTime();
    const yearDiff = timeDiff / (1000 * 3600 * 24 * 365);
    const CAGR = ((Math.pow((price2 / price1), (1 / yearDiff))) - 1) * 100;
    return CAGR;
};
//總報酬率
function getGrossReturn(date1, date2, price1, price2) {
    const timeDiff = date2.getTime() - date1.getTime();
    const grossReturn = (price2 / price1) - 1;
    return grossReturn;
};

//取得今年交易日數
function getYearTradeDays() {
    
    const firstday = new Date(year.toString(), 0, 1);
    const today = new Date(year.toString(), now.getMonth(), now.getDate());
    const dayDiff = parseFloat((today.getTime() - firstday.getTime()) / (1000 * 3600 * 24));  //今年到今天有幾個整數天
    var tdays = Math.floor(dayDiff / 7)*5; //本週以前的交易天數
    tdays += now.getDay() < 6 ? now.getDay() : 5;  //判斷本週天數(週日從0開始)後加上去
    return tdays;
};

//再畫一次線圖
function updateChart(c) {
    chart.destroy();

    data = {
        labels: dates,
        datasets: [{
            label: [symbolstr],
            data: closes,
            borderWidth: 1,
            pointRadius: 0
        }]
    };
    config = {
        type: 'line',
        data,
        options: {
            animation: false,
            scales: {
                x: { grid: { drawOnChartArea: false } },
                y: {
                    beginAtZero: false,
                }
            },
            plugins: {
                legend: {
                    display: true,
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
    chart = new Chart(document.getElementById("LineChart"),
        config);
    console.log(c);
};