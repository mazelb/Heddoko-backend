/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for trends demo
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    January 2016
 */
angular.module('app.controllers')

.controller('DemoTrainingSessionController', ['$scope', '$timeout', '$filter', 'DemoTrendsService', 'Rover', 'Utilities',
    function($scope, $timeout, $filter, DemoTrendsService, Rover, Utilities) {
        Utilities.debug('DemoTrainingSessionController');

        $scope.metric = null;
        $scope.session = null;
        $scope.isFetchingSessionData = false;
        $scope.isFetchingSelectedMetricData = false;
        $scope.isSessionDataLoaded = false;
        $scope.colours = [Utilities.color.heddokoGreen, '#79d9d8', '#6eb4d2'];
        $scope.fakeData = {};



        ///
        /// General options for flot charts
        ///
        /////////////////////////////////////////////////////////////////



        var flotOptions =
        {
            grid:
            {
                backgroundColor: 'rgba(0, 0, 0, 0)',
                borderColor: 'rgba(0, 0, 0, 0)',
                clickable: false,
                hoverable: true
            },
            legend: {show: false},
            series: {hoverable: true},
            xaxis: {
                tickColor: 'rgba(91, 112, 125, 0.4)'
            },
            yaxis: {
                tickColor: 'rgba(0, 0, 0, 0)',
                position: 'left'
            },
            tooltip: true,
            tooltipOpts: {
                defaultTheme: true
            }
        },
        flotTooltipStyles = {
            position: 'absolute',
            display: 'none',
            padding: '5px 10px',
            color: Utilities.color.textColor,
            border: '1px solid ' + Utilities.color.textColorBlue,
            'background-color': Utilities.color.darkBlue,
            'text-align': 'center',
            opacity: 0.9
        },
        flotTicksX = [
            // [0, '0'],
            [1, '10'],
            [2, '20'],
            [3, '30'],
            [4, '40'],
            [5, '50'],
            [6, '60'],
            [7, '70'],
            [8, '80'],
            [9, '90'],
            [10, '100'],
            [11, '110'],
            [12, '120'],
            [13, '130'],
            [14, '140'],
            [15, '150'],
            [16, 'Throws']
        ];



        ///
        /// Demo: Peak Elbow Angular Velocity
        ///
        /////////////////////////////////////////////////////////////////



        // Markings.
        var flotElbowLabelTmpl = {
            color: Utilities.color.orange,
            lineWidth: 3
        },
        flotElbowDrops = [
            {
                num: 109,
                sup: 'th'
            },
            {
                num: 104,
                sup: 'th'
            },
            {
                num: 117,
                sup: 'th'
            },
            {
                num: 109,
                sup: 'th'
            },
            {
                num: 97,
                sup: 'th'
            },
        ],
        flotElbowLabelText = function(index) {
            return 'Drop in velocity<br>around <b>'+ flotElbowDrops[index].num +
                    '<sup>'+ flotElbowDrops[index].sup +'</sup></b> throw';
        };

        $scope.flotElbowDrops = flotElbowDrops;

        $scope.flotElbowLabels = [

            [   // Jan 28
                $.extend({}, flotElbowLabelTmpl, {
                    point: {x: 10.9, y: 2900},
                    text: flotElbowLabelText(0)
                })
            ],

            [   // Jan 29
                $.extend({}, flotElbowLabelTmpl, {
                    point: {x: 10.4, y: 2900},
                    text: flotElbowLabelText(1)
                })
            ],

            [   // Feb 1
                $.extend({}, flotElbowLabelTmpl, {
                    point: {x: 11.7, y: 2900},
                    text: flotElbowLabelText(2)
                })
            ],

            [   // Feb 2
                $.extend({}, flotElbowLabelTmpl, {
                    point: {x: 10.9, y: 2900},
                    text: flotElbowLabelText(3)
                })
            ],

            [   // Feb 3
                $.extend({}, flotElbowLabelTmpl, {
                    point: {x: 9.7, y: 2900},
                    text: flotElbowLabelText(4)
                })
            ],
        ];

        // Options
        var flotElbowOptionsTmpl = $.extend({}, flotOptions, {
            xaxis: {ticks: flotTicksX},
            yaxis: {
                min: 1000,
                max: 3000
            },
            colors: [
                '#4dc5de',
                '#4dc5de',
                '#fff'
            ]
        });

        $scope.flotElbowOptions = [

            // Jan 28
            $.extend(true, {}, flotElbowOptionsTmpl, {
                grid: {
                    markings: [
                        {
                            color: Utilities.color.orange,
                            lineWidth: 2,
                            xaxis: {from: 10.9, to: 10.9}
                        }
                    ]
                },
            }),

            // Jan 29
            $.extend(true, {}, flotElbowOptionsTmpl, {
                grid: {
                    markings: [
                        {
                            color: Utilities.color.orange,
                            lineWidth: 2,
                            xaxis: {from: 10.4, to: 10.4}
                        }
                    ]
                },
            }),

            // Feb 1
            $.extend(true, {}, flotElbowOptionsTmpl, {
                grid: {
                    markings: [
                        {
                            color: Utilities.color.orange,
                            lineWidth: 2,
                            xaxis: {from: 11.7, to: 11.7}
                        }
                    ]
                },
            }),

            // Feb 2
            $.extend(true, {}, flotElbowOptionsTmpl, {
                grid: {
                    markings: [
                        {
                            color: Utilities.color.orange,
                            lineWidth: 2,
                            xaxis: {from: 10.9, to: 10.9}
                        }
                    ]
                },
            }),

            // Feb 3
            $.extend(true, {}, flotElbowOptionsTmpl, {
                grid: {
                    markings: [
                        {
                            color: Utilities.color.orange,
                            lineWidth: 2,
                            xaxis: {from: 9.7, to: 9.7}
                        }
                    ]
                },
            }),
        ];

        // Data points
        var flotElbowDataPointsData = [

            [   // Jan 28
                [0, 2550],
                [1, 2540],
                [2, 2578],
                [3, 2660],
                [4, 2750],
                [5, 2790],
                [6, 2810],
                [7, 2792],
                [8, 2740],
                [9, 2657],
                [10, 2530],
                [11, 2430],
                [12, 2368],
                [13, 2275],
                [14, 2263],
                [15, 2189],
                [16, 2175]
            ],

            [   // Jan 29
                [0, 2550],
                [1, 2570],
                [2, 2579],
                [3, 2660],
                [4, 2740],
                [5, 2756],
                [6, 2741],
                [7, 2724],
                [8, 2694],
                [9, 2657],
                [10, 2530],
                [11, 2275],
                [12, 2100],
                [13, 2003],
                [14, 1911],
                [15, 1822],
                [16, 1737]
            ],

            [   // Feb 1
                [0, 2784],
                [1, 2779],
                [2, 2748],
                [3, 2661],
                [4, 2750],
                [5, 2790],
                [6, 2810],
                [7, 2792],
                [8, 2740],
                [9, 2657],
                [10, 2597],
                [11, 2530],
                [12, 2302],
                [13, 2213],
                [14, 2187],
                [15, 2196],
                [16, 2192]
            ],

            [   // Feb 2
                [0, 2550],
                [1, 2578],
                [2, 2599],
                [3, 2660],
                [4, 2750],
                [5, 2733],
                [6, 2777],
                [7, 2792],
                [8, 2740],
                [9, 2657],
                [10, 2598],
                [11, 2468],
                [12, 2318],
                [13, 2296],
                [14, 2306],
                [15, 2293],
                [16, 2275]
            ],

            [   // Feb 3
                [0, 2550],
                [1, 2540],
                [2, 2578],
                [3, 2660],
                [4, 2750],
                [5, 2790],
                [6, 2810],
                [7, 2792],
                [8, 2740],
                [9, 2657],
                [10, 2530],
                [11, 2275],
                [12, 2100],
                [13, 2003],
                [14, 1961],
                [15, 1899],
                [16, 1879]
            ],
        ];


        // Data objects
        var flotElbowDataThresholdTmpl = {
            bars: {show: false},
            lines: {
                fill: true,
                fillColor: {colors: ['rgba(77, 197, 222, 0)', 'rgba(77, 197, 222, 0.3)']},
                lineWidth: 2,
                show: true
            },
            data: [
                [0, 2100],
                [16, 2100]
            ],
            isThresholdSeries: true
        },
        flotElbowDataLinesTmpl = {
            bars: {show: false},
            lines: {
                color: '#ddd',
                fill: false,
                lineWidth: 4,
                show: true
            }
        },
        flotElbowDataPointsTmpl = {
            bars: {show: false},
            points: {
                color: '#ddd',
                lineWidth: 4,
                show: true
            }
        },

        flotElbowDataThresholds = [
            $.extend({}, flotElbowDataThresholdTmpl),
            $.extend({}, flotElbowDataThresholdTmpl),
            $.extend({}, flotElbowDataThresholdTmpl),
            $.extend({}, flotElbowDataThresholdTmpl),
            $.extend({}, flotElbowDataThresholdTmpl),
        ];

        $scope.flotElbowData = [];

        for (var iElbow = 0; iElbow < 5; iElbow++)
        {
            $scope.flotElbowData.push([
                flotElbowDataThresholds[iElbow],
                $.extend({}, flotElbowDataLinesTmpl, {
                    data: flotElbowDataPointsData[iElbow]
                }),
                $.extend({}, flotElbowDataPointsTmpl, {
                    data: flotElbowDataPointsData[iElbow]
                })
            ]);
        }

        $scope.flotElbowHover = function (event, pos, item) {

            // Display tooltip for data point.
			if (item)
            {
				$('#flot-elbow-velocity-tooltip')
                    .html(
                        '<b>' + $filter('number')(item.datapoint[1], 0) + ' &deg;/s</b>'
                    )
					.css({
                        top: item.pageY - $('#flot-elbow-velocity-tooltip').height() - 45,
                        left: item.pageX - 40,
                        // 'background-color': item.series.color
                        'background-color': Utilities.color.darkBlue
                    })
					.fadeIn(300);
			}

            // Hide tooltip if no element is selected.
            else {
				$('#flot-elbow-velocity-tooltip').hide();
			}
		};

        // Create tooltip template element.
        $('<div id="flot-elbow-velocity-tooltip"></div>').css(flotTooltipStyles).appendTo('body');

        // Gauge charts.
        $scope.elbowMinVel = 0;
        $scope.elbowAvgVel = 0;
        $scope.elbowMaxVel = 0;
        $scope.calcElbowGauge = function() {

            var dataPoints = flotElbowDataPointsData[$scope.session.id - 1],
                data = dataPoints.map(function(point) {
                    return point[1];
                }),
                sum = data.reduce(function(previous, current, index, data) {
                    return previous + current;
                });

            $scope.elbowMinVel = Math.min.apply(Math, data);
            $scope.elbowAvgVel = Math.round(sum / data.length);
            $scope.elbowMaxVel = Math.max.apply(Math, data);
        };

        $scope.elbowMinGauge =
        {
            data: {
                maxValue: 1000,
                animationSpeed: 20,
                val: 630
            },
            options: {
                lines: 12,
                angle: 0,
                lineWidth: 0.3,
                pointer: {
                    length: 0,
                    strokeWidth: 0
                },
                limitMax: 'false',
                strokeColor: Utilities.color.blue,
                generateGradient: false,
                percentColors: [
                    [0, $scope.colours[2]],
                    [0.5, $scope.colours[2]],
                    [1, $scope.colours[2]]
                ]
            }
        };

        $scope.elbowAvgGauge =
        {
            data: {
                maxValue: 1000,
                animationSpeed: 20,
                val: 834
            },
            options: {
                lines: 12,
                angle: 0,
                lineWidth: 0.3,
                pointer: {
                    length: 0,
                    strokeWidth: 0
                },
                limitMax: 'false',
                strokeColor: Utilities.color.blue,
                generateGradient: false,
                percentColors: [
                    [0, $scope.colours[1]],
                    [0.5, $scope.colours[1]],
                    [1, $scope.colours[1]]
                ]
            }
        };

        $scope.elbowMaxGauge =
        {
            data: {
                maxValue: 1000,
                animationSpeed: 20,
                val: 940
            },
            options: {
                lines: 20,
                angle: 0,
                lineWidth: 0.3,
                pointer: {
                    length: 0,
                    strokeWidth: 0
                },
                limitMax: 'false',
                strokeColor: Utilities.color.blue,
                generateGradient: false,
                percentColors: [
                    [0, Utilities.color.heddokoGreen],
                    [0.5, Utilities.color.heddokoGreen],
                    [1, Utilities.color.heddokoGreen]
                ]
            }
        };



        ///
        /// Demo: Shoulder External Rotation
        ///
        /////////////////////////////////////////////////////////////////



        // Options
        var flotShoulderOptionsTmpl = $.extend(true, {}, flotOptions, {
            grid: {
                color: 'rgba(91, 112, 125, 1)',
                markings: [
                    {
                        color: 'rgba(77, 197, 222, 0.3)',
                        yaxis: {from: 70, to: 100}
                    },
                ]
            },
            xaxis: {
                ticks: flotTicksX
            },
            yaxis: {
                ticks: [
                    [0, '0'],
                    [20, '20'],
                    [40, '40'],
                    [60, '60'],
                    [80, '80'],
                    [100, '100'],
                    [120, '%'],
                ]
            },
            colors: [
                '#4dc5de',
                '#fff',
            ]
        });

        $scope.flotShoulderOptions = [
            $.extend({}, flotShoulderOptionsTmpl),
            $.extend({}, flotShoulderOptionsTmpl),
            $.extend({}, flotShoulderOptionsTmpl),
            $.extend({}, flotShoulderOptionsTmpl),
            $.extend({}, flotShoulderOptionsTmpl),
        ];

        $scope.flotShoulderDataPoints = [

            [   // Jan 28
                [0, 82],
                [1, 89],
                [2, 91],
                [3, 90],
                [4, 88],
                [5, 91],
                [6, 94],
                [7, 91],
                [8, 85],
                [9, 81],
                [10, 76],
                [11, 73],
                [12, 64],
                [13, 62],
                [14, 58],
                [15, 53],
                [16, 44]
            ],

            [   // Jan 29
                [0, 82],
                [1, 89],
                [2, 91],
                [3, 90],
                [4, 88],
                [5, 91],
                [6, 94],
                [7, 91],
                [8, 85],
                [9, 67],
                [10, 63],
                [11, 61],
                [12, 64],
                [13, 62],
                [14, 58],
                [15, 53],
                [16, 60]
            ],

            [   // Feb 1
                [0, 82],
                [1, 89],
                [2, 91],
                [3, 90],
                [4, 88],
                [5, 91],
                [6, 94],
                [7, 91],
                [8, 85],
                [9, 86],
                [10, 82],
                [11, 79],
                [12, 81],
                [13, 77],
                [14, 74],
                [15, 73],
                [16, 69]
            ],

            [   // Feb 2
                [0, 82],
                [1, 79],
                [2, 81],
                [3, 86],
                [4, 88],
                [5, 91],
                [6, 94],
                [7, 91],
                [8, 85],
                [9, 82],
                [10, 77],
                [11, 71],
                [12, 67],
                [13, 62],
                [14, 58],
                [15, 58],
                [16, 60]
            ],

            [   // Feb 3
                [0, 82],
                [1, 89],
                [2, 91],
                [3, 90],
                [4, 88],
                [5, 85],
                [6, 82],
                [7, 76],
                [8, 75],
                [9, 67],
                [10, 63],
                [11, 61],
                [12, 59],
                [13, 52],
                [14, 54],
                [15, 53],
                [16, 60]
            ],
        ];

        var flowShoulderDataLinesTmpl = {
            bars: {show: false},
            lines: {
                color: '#4dc5de',
                fill: false,
                lineWidth: 4,
                show: true
            }
        },
        flotShoulderDataPointsTmpl = {
            bars: {show: false},
            points: {
                color: '#fff',
                lineWidth: 4,
                show: true
            }
        };

        $scope.flotShoulderData = [];

        for (var iShoulder = 0; iShoulder < 5; iShoulder++)
        {
            $scope.flotShoulderData.push([

                // Data line
                $.extend({}, flowShoulderDataLinesTmpl, {
                    data: $scope.flotShoulderDataPoints[iShoulder]
                }),

                // Data points
                $.extend({}, flotShoulderDataPointsTmpl, {
                    data: $scope.flotShoulderDataPoints[iShoulder]
                }),
            ]);
        }

        // Chart tooltips
        $scope.flotShoulderRotHover = function (event, pos, item) {

            // Display tooltip for data point.
			if (item)
            {
				$("#flot-shoulder-rot-tooltip")
                    .html(
                        '<b>' + $filter('number')(item.datapoint[1], 0) + ' %</b>'
                    )
					.css({
                        top: item.pageY - $('#flot-shoulder-rot-tooltip').height() - 45,
                        left: item.pageX - 40,
                        // 'background-color': item.series.color
                        'background-color': Utilities.color.darkBlue
                    })
					.fadeIn(300);

                $timeout(function() {
                    $scope.demoGaugeOneValue = (item.datapoint[1]) * 1000 / 100;
                    $scope.demoGaugeOneValuePercent = $filter('number')(item.datapoint[1], 0) + ' %';
                });
			}

            // Hide tooltip if no element is selected.
            else {
				$('#flot-shoulder-rot-tooltip').hide();

                $timeout(function() {
                    $scope.demoGaugeOneValue = 500;
                    $scope.demoGaugeOneValuePercent = '';
                });
			}
		};
        $('<div id="flot-shoulder-rot-tooltip"></div>').css(flotTooltipStyles).appendTo('body');


        ///
        /// Demo: Stride Length
        ///
        /////////////////////////////////////////////////////////////////


        var flotStrideOptionsTmpl = $.extend(true, {}, flotOptions, {
            grid: {
                markings: [
                    {
                        color: 'rgba(63, 213, 174, 0.3)',
                        yaxis: {from: 26, to: 30}
                    },
                ]
            },
            xaxis: {
                ticks: flotTicksX
            },
            yaxis: {
                ticks: [
                    [20, '20'],
                    [22, '22'],
                    [24, '24'],
                    [26, '26'],
                    [28, '28'],
                    [30, '30'],
                    [32, '32'],
                    [34, '34'],
                    [36, 'in.'],
                ]
            },
            colors: [
                'rgba(63, 213, 174, 1)',
                '#fff',
            ]
        });

        $scope.flotStrideOptions = [
            $.extend({}, flotStrideOptionsTmpl),
            $.extend({}, flotStrideOptionsTmpl),
            $.extend({}, flotStrideOptionsTmpl),
            $.extend({}, flotStrideOptionsTmpl),
            $.extend({}, flotStrideOptionsTmpl),
        ];

        $scope.flowStrideDataPoints = [

            [   // Jan 28
                [0, 27.4],
                [1, 28],
                [2, 28.2],
                [3, 30.2],
                [4, 29.3],
                [5, 27.9],
                [6, 28.1],
                [7, 28.7],
                [8, 28.3],
                [9, 27.9],
                [10, 27.4],
                [11, 26.1],
                [12, 25.3],
                [13, 25],
                [14, 24.6],
                [15, 24.4],
                [16, 23.9]
            ],

            [   // Jan 29
                [0, 27.4],
                [1, 28],
                [2, 26.5],
                [3, 25.9],
                [4, 26.9],
                [5, 27.1],
                [6, 27.3],
                [7, 27],
                [8, 27.4],
                [9, 27.2],
                [10, 26.3],
                [11, 25.7],
                [12, 24.8],
                [13, 24.4],
                [14, 24.6],
                [15, 24.9],
                [16, 24.6]
            ],

            [   // Feb 1
                [0, 27.4],
                [1, 26.8],
                [2, 26.8],
                [3, 26.3],
                [4, 26.9],
                [5, 27.9],
                [6, 28.1],
                [7, 28.7],
                [8, 28.3],
                [9, 27.9],
                [10, 27.4],
                [11, 26.9],
                [12, 27.3],
                [13, 27.9],
                [14, 27.5],
                [15, 26.9],
                [16, 26.2]
            ],

            [   // Feb 2
                [0, 26.9],
                [1, 27.7],
                [2, 27.9],
                [3, 28.2],
                [4, 28.6],
                [5, 28.3],
                [6, 28.1],
                [7, 28.7],
                [8, 28.3],
                [9, 27.9],
                [10, 27.4],
                [11, 26.1],
                [12, 25.3],
                [13, 25],
                [14, 24.6],
                [15, 24.8],
                [16, 24.9]
            ],

            [   // Feb 3
                [0, 27.4],
                [1, 28],
                [2, 26.5],
                [3, 25.9],
                [4, 26.9],
                [5, 27.9],
                [6, 28.1],
                [7, 28.7],
                [8, 28.3],
                [9, 27.9],
                [10, 27.4],
                [11, 26.1],
                [12, 25.3],
                [13, 25],
                [14, 24.6],
                [15, 23.1],
                [16, 22.9]
            ],
        ];

        var flotStrideDataLinesTmpl = {
            bars: {show: false},
            lines: {
                fill: false,
                lineWidth: 4,
                show: true
            }
        }, flotStrideDataPointsTmpl = {
            bars: {show: false},
            points: {
                fill: true,
                lineWidth: 4,
                show: true
            }
        };

        $scope.flotStrideData = [];

        for (var iStride = 0; iStride < 5; iStride++)
        {
            $scope.flotStrideData.push([

                // Data line.
                $.extend({}, flotStrideDataLinesTmpl, {
                    data: $scope.flowStrideDataPoints[iStride]
                }),

                // Data scatter.
                $.extend({}, flotStrideDataPointsTmpl, {
                    data: $scope.flowStrideDataPoints[iStride]
                })
            ]);
        }

        // Chart tooltips
        $scope.flotStrideHover = function (event, pos, item) {

            // Display tooltip for data point.
			if (item)
            {
				$("#flot-stride-tooltip")
                    .html(
                        '<b>' + $filter('number')(item.datapoint[1], 1) + '"</b>'
                    )
					.css({
                        top: item.pageY - $('#flot-stride-tooltip').height() - 45,
                        left: item.pageX - 40,
                        // 'background-color': item.series.color
                        'background-color': Utilities.color.darkBlue
                    })
					.fadeIn(200);

                $timeout(function() {
                    $scope.demoGaugeTwoValue = (item.datapoint[1] - 20) * 1000 / 10;
                    $scope.demoGaugeTwoValuePercent = $filter('number')(item.datapoint[1], 0) + '"';
                });
			}

            // Hide tooltip if no element is selected.
            else {
				$('#flot-stride-tooltip').hide();

                $timeout(function() {
                    $scope.demoGaugeTwoValue = 500;
                    $scope.demoGaugeTwoValuePercent = '';
                });
			}
		};
        $('<div id="flot-stride-tooltip"></div>').css(flotTooltipStyles).appendTo('body');


        ///
        /// Demo: Gauge
        ///
        /////////////////////////////////////////////////////////////////


        $scope.demoGaugeOneValue = 500;
        $scope.demoGaugeOne =
        {
            data: {
                maxValue: 1000,
                animationSpeed: 4,
                val: $scope.demoGaugeOneValue
            },
            options: {
                lines: 12,
                angle: 0,
                lineWidth: 0.3,
                pointer: {
                    length: 0,
                    strokeWidth: 0
                },
                limitMax: 'false',
                strokeColor: Utilities.color.blue,
                generateGradient: false,
                percentColors: [
                    [0, '#4dc5de'],
                    [0.5, '#4dc5de'],
                    [1, '#4dc5de']
                ]
            }
        };

        $scope.demoGaugeTwoValue = 500;
        $scope.demoGaugeTwo =
        {
            data: {
                maxValue: 1000,
                animationSpeed: 4,
                val: $scope.demoGaugeTwoValue
            },
            options: {
                lines: 12,
                angle: 0,
                lineWidth: 0.3,
                pointer: {
                    length: 0,
                    strokeWidth: 0
                },
                limitMax: 'false',
                strokeColor: Utilities.color.blue,
                generateGradient: false,
                percentColors: [
                    [0, '#3FD5AE'],
                    [0.5, '#3FD5AE'],
                    [1, '#3FD5AE']
                ]
            }
        };



        ///
        /// Demo: Selectize
        ///
        /////////////////////////////////////////////////////////////////



        $scope.selectizeSessionModel = 0;
        $scope.selectizeMetricModel = 0;

        $scope.selectizeSessionConfig = {
            create: false,
            valueField: 'id',
            labelField: 'title',
            searchField: ['title'],
            maxItems: 1,

            /**
             * Called anytime the value of the input changes.
             *
             * @param array data
             */
            onChange: function(id) {
                angular.forEach($scope.selectizeSessionOptions, function(option) {

                    if (option.id == id) {
                        $timeout(function() {
                            $scope.session = option;
                        });
                    }
                });
            }
        };

        $scope.selectizeMetricConfig = {
            create: false,
            valueField: 'id',
            labelField: 'title',
            searchField: ['title'],
            maxItems: 1,

            /**
             * Called anytime the value of the input changes.
             *
             * @param array data
             */
            onChange: function(id) {
                angular.forEach($scope.selectizeMetricOptions, function(option) {
                    if (option.id == id) {
                        $timeout(function() {
                            $scope.metric = option;
                        });
                    }
                });
            }
        };

        $scope.selectizeSessionOptions = [
            {id: 5, type: 'session', title: 'February 3, 2016'},
            {id: 4, type: 'session', title: 'February 2, 2016'},
            {id: 3, type: 'session', title: 'February 1, 2016'},
            {id: 2, type: 'session', title: 'January 29, 2016'},
            {id: 1, type: 'session', title: 'January 28, 2016'}
        ];

        // Metrics
        $scope.selectizeMetricOptions = [
            // {id: 1, type: 'metric', title: 'Kinematic Sequence'},
            {id: 2, type: 'metric', title: 'Peak Elbow Angular Velocity'},
            // {id: 3, type: 'metric', title: 'Peak Forearm Snap Velocity'},
            {id: 4, type: 'metric', title: 'Shoulder External Rotation'},
            {id: 5, type: 'metric', title: 'Stride Length'},
            // {id: 6, type: 'metric', title: 'Stride Timing'},
            // {id: 7, type: 'metric', title: 'Torso Rotation'}
        ];



        ///
        /// Demo: Simulate data fetching.
        ///
        /////////////////////////////////////////////////////////////////



        $scope.fetchDataDemo = function(newData, oldData) {

            // Wait for both the session and metric to be selected.
            if (!$scope.session || !$scope.metric || $scope.isFetchingSessionData || !newData) {
                return;
            }

            // Update metric plots.
            if (newData.type == 'metric' && $scope.isSessionDataLoaded)
            {
                $scope.isFetchingSelectedMetricData = true;

                $timeout(function() {
                    $scope.isFetchingSelectedMetricData = false;
                }, 800);
            }

            // Update session data.
            else
            {
                $scope.isFetchingSessionData = true;
                $scope.calcElbowGauge();

                $timeout(function() {
                    $scope.isSessionDataLoaded = true;
                    $scope.isFetchingSessionData = false;
                }, 800);
            }
        };

        $scope.$watch('session', $scope.fetchDataDemo);
        $scope.$watch('metric', $scope.fetchDataDemo);
    }
]);
