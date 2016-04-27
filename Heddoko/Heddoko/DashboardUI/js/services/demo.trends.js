/**
 * @brief   The FMSDemoFactory is a temporary factory used for the demo "live FMS" screens.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    January 2016
 */
angular.module('app.services')

.service('DemoTrendsService', ['Rover',
    function(Rover) {

        //
        // Data.
        //
        this.trend = {};
        this.gauge = {};

        //
        // Original dummy data.
        //
        this.trend.dummyData = [];

        // Data for first dummy trendline.
        this.trend.dummyData.push({
            data: [
                [1, 15],
                [2, 20],
                [3, 14],
                [4, 10],
                [5, 10],
                [6, 20],
                [7, 28],
                [8, 26],
                [9, 22],
                [10, 23],
                [11, 24]
            ],
            label: 'New visitors',
            lines: {
                fill: !0
            }
        });

        // Data for second dummy trendline.
        this.trend.dummyData.push({
            data: [
                [1, 9],
                [2, 15],
                [3, 17],
                [4, 21],
                [5, 16],
                [6, 15],
                [7, 13],
                [8, 15],
                [9, 29],
                [10, 21],
                [11, 29]
            ],
            label: 'Returning visitors',
            lines: {
                fill: !1
            }
        });

        // Options for dummy trendline.
        this.trend.options = {
            series: {
                lines: {
                    show: !0,
                    fill: !1,
                    lineWidth: 3,
                    fillColor: {
                        colors: [{
                            opacity: 0.3
                        }, {
                            opacity: 0.3
                        }]
                    }
                },
                points:
                {
                    show: !0,
                    lineWidth: 3,
                    fill: !0,
                    fillColor: "#ffffff",
                    symbol: "circle",
                    radius: 5
                },
                shadowSize: 0

            },
            colors: ["#c1bfc0", "#db5031"],
            tooltip: !0,
            tooltipOpts: {
                defaultTheme: !1
            },
            grid: {
                hoverable: !0,
                clickable: !0,
                tickColor: "#f9f9f9",
                borderWidth: 1,
                borderColor: "#eeeeee"
            },
            xaxis: {
                ticks: [
                    [1, "Jan."],
                    [2, "Feb."],
                    [3, "Mar."],
                    [4, "Apr."],
                    [5, "May"],
                    [6, "June"],
                    [7, "July"],
                    [8, "Aug."],
                    [9, "Sept."],
                    [10, "Oct."],
                    [11, "Nov."],
                    [12, "Dec."]
                ]
            }
        };

        // Data for dummy gauge meter.
        this.gauge.dummyData = {
            maxValue: 3e3,
            animationSpeed: 100,
            val: 1300
        };
    }
]);
