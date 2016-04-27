/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for trends demo
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    January 2016
 */
angular.module('app.controllers')

.controller('DemoTrendsController', ['$scope', '$timeout', '$filter', 'DemoTrendsService', 'Rover', 'Utilities',
    function($scope, $timeout, $filter, DemoTrendsService, Rover, Utilities) {
        Utilities.debug('DemoTrendsController');

        $scope.profile = $scope.global.getSelectedProfile();

        $scope.thresholdValue = 2800;
        $scope.thresholdIncrement = 50;
        $scope.isFetchingData = false;
        $scope.metric = null;

        /**
         * Increases "return to play" threshold.
         */
        $scope.increaseThreshold = function() {
            $scope.thresholdValue =
                $scope.thresholdValue >= ($scope.flotOptions.yaxes[0].max - $scope.thresholdIncrement) ?
                    parseInt($scope.flotOptions.yaxes[0].max) :
                    parseInt($scope.thresholdValue) + $scope.thresholdIncrement;
        };

        /**
         * Decreases "return to play" threshold.
         */
        $scope.decreaseThreshold = function() {
            $scope.thresholdValue =
                $scope.thresholdValue <= ($scope.flotOptions.yaxes[0].min + $scope.thresholdIncrement) ?
                    parseInt($scope.flotOptions.yaxes[0].min) :
                    parseInt($scope.thresholdValue) - $scope.thresholdIncrement;
        };


        ///
        /// Demo: Metric Selection
        ///
        /////////////////////////////////////////////////////////////////


        $scope.selectizeMetricModel = 0;

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

        // Metrics
        $scope.selectizeMetricOptions = [
            // {id: 1, type: 'metric', title: 'Hawkins Impingement Test Results'},
            // {id: 2, type: 'metric', title: 'Neer Impingement Test Results'},
            {id: 3, type: 'metric', title: 'Peak Elbow Angular Velocity'},
            // {id: 4, type: 'metric', title: 'Peak Shoulder Internal Rotation Velocity'},
            {id: 5, type: 'metric', title: 'Shoulder Range of Motion'},
            // {id: 6, type: 'metric', title: 'Shoulder Strength Ratings'},
        ];



        ///
        /// Demo: Percent recovery (easypie)
        ///
        /////////////////////////////////////////////////////////////////


        /**
         * Returns a colour depending on the percent recovery.
         *
         * @param int percent   A number between 0 and 100.
         */
        $scope.easypiePercentColor = function(percent) {
            if (percent < 85) {
                return '#db5031';
            } else if (percent < 95) {
                return '#fabd39';
            }

            return '#3bd6b2';
        };

        $scope.easypie = {
            percent: Math.min(100, Math.round(2750 * 100 / $scope.thresholdValue)),
            options: {
                animate: {
                    duration: 1e2,
                    enabled: !0
                },
                barColor: $scope.easypiePercentColor,
                lineCap: 'round',
                lineWidth: 8,
                scaleColor: false,
                size: 130,
                trackColor: false
            }
        };

        // Threshold watcher.
        $scope.$watch('thresholdValue', function(newThreshold, oldThreshold) {

            // Performance check.
            if (newThreshold == oldThreshold) {
                return;
            }

            // Update easypie chart.
            $scope.easypie.percent = Math.min(100, Math.round(2750 * 100 / $scope.thresholdValue));
        });


        ///
        /// Demo: Peak Elbow Angular Velocity
        ///
        /////////////////////////////////////////////////////////////////


        $scope.flotOptions = {
            grid: {
                borderWidth: 1,
                borderColor: 'rgba(0, 0, 0, 0)',
                clickable: false,
                hoverable: true,
                thresholdZones: {
                    min: 500,
                    max: 3500,
                    total: 5
                }
            },
            legend: {
                show: false
            },
            series: {
                bars: {
                    barWidth: 0.5,
                    fill: 1,
                    show: true
                },
                hoverable: true
            },
            xaxis: {
                color: 'rgba(0, 0, 0, 0)',
                ticks: []
            },
            yaxes: [
                {
                    color: 'rgba(91, 112, 125, 0.4)',
                    min: 500,
                    max: 3500,
                    position: 'left'
                }
            ],
            tooltip: !0,
            tooltipOpts: {
                defaultTheme: !1
            },
            colors: [
                '#22aaaa',      // Week 1
                '#6ae1e1',      // Week 2
                '#22aaaa',      // Week 3
                '#6ae1e1',      // Week 4
                '#22aaaa',      // Week 5
                '#6ae1e1',      // Week 6
                '#22aaaa',      // Week 7
                'rgba(255, 255, 255, 0.5)',     // Median line
                Utilities.color.orange,        // Return to play
            ]
        };

        $scope.flotData = [

            {data: [], highlightColor: '#fff', yaxis: 1},   // Week 1
            {data: [], highlightColor: '#fff', yaxis: 1},   // Week 2
            {data: [], highlightColor: '#fff', yaxis: 1},   // Week 3
            {data: [], highlightColor: '#fff', yaxis: 1},   // Week 4
            {data: [], highlightColor: '#fff', yaxis: 1},   // Week 5
            {data: [], highlightColor: '#fff', yaxis: 1},   // Week 6
            {data: [], highlightColor: '#fff', yaxis: 1},   // Week 7

            // Median line.
            {
                bars: {show: false},
                data: [],
                lines: {
                    color: '#fff',
                    fill: false,
                    lineWidth: 2,
                    show: true
                },
                points: {
                    fill: true,
                    fillColor: '#fff',
                    radius: 5,
                    show: true
                },
                yaxis: 1
            },

            // Return to play.
            {
                bars: {show: false},
                clickable: true,
                data: [],
                label: 'Return to play',
                lines: {
                    fill: false,
                    lineWidth: 2,
                    show: true
                },
                isThresholdSeries: true,
                updateColor: function(val) {

                    // Return a colour depending on the current value of the threshold.
                    return $scope.easypiePercentColor(Math.min(100, Math.round(2750 * 100 / val)));
                },
                yaxis: 1
            }
        ];

        $scope.flotPlotHover = function (event, pos, item) {

            // Display tooltip for data point.
			if (item)
            {
				$("#demo-flot-tooltip")
                    .html(
                        '<b>' + $filter('number')(item.datapoint[1], 0) + ' &deg;/s</b>' +
                        '<br>' +
                        '<span style="text-transform: uppercase;">' +
                            Math.min(100, Math.round(item.datapoint[1] * 100 / $scope.thresholdValue)) +
                            '% of baseline' +
                        '</span>'
                    )
					.css({
                        top: item.pageY - $('#demo-flot-tooltip').height() - 35,
                        left: item.pageX - 85
                        // 'background-color': item.series.color
                        // 'background': 'linear-gradient(to right, rgba(91, 112, 125, 0.8) 0%, transparent 60%)'
                    })
					.fadeIn(200);
			}

            // Hide tooltip if no element is selected.
            else {
				$('#demo-flot-tooltip').hide();
			}
		};

        // Create tooltip template element.
        $('<div id="demo-flot-tooltip"></div>').css({
            position: 'absolute',
            display: 'none',
            padding: '5px 10px',
            color: '#fff',
            'border': '1px solid ' + Utilities.color.textColorBlue,
            'background-color': Utilities.color.darkBlue,
            'text-align': 'center',
            opacity: 1
        }).appendTo('body');

        //
        // Flot chart data generation.
        //

        $scope.dataMax = 2800;
        $scope.dataPointsInitialDensity = 5;
        $scope.dataMedians = [1250, 1320, 1400, 1590, 1960, 2240, 2750];

        // Indicates index of first dataset in $scope.flotData
        $scope.flotDataStartIndex = 0;

        /**
         * Generates a random data point, within the specified range.
         *
         * @param int min
         * @param int max
         * @return int
         */
        $scope.randomDataPoint = function(min, max) {
            return Math.floor(Math.random() * (max - min)) + min;
        };

        //
        var i, j, density, midPoint, label, min, max, x = 0, y;
        for (i = 0; i < $scope.dataMedians.length; i++)
        {
            x++;

            // Set density and midpoint for given week.
            density = $scope.dataPointsInitialDensity + Math.floor(Math.pow((i * $scope.dataPointsInitialDensity), 1.1));
            midPoint = Math.floor(density/2) + x;

            // Add ticks on x-axis.
            label = i > 1 ? 'Week ' + (i + 1) : (i + 1);
            $scope.flotOptions.xaxis.ticks.push([midPoint, label]);

            // Generate ideal return-to-play line.
            $scope.flotData[$scope.flotDataStartIndex + $scope.dataMedians.length].data.push([
                midPoint,
                $scope.dataMedians[i]
            ]);

            // Add data points.
            for (j = 0; j < density; j++)
            {
                // Data point offset on x-axis.
                // x = 1 + i + j + $scope.dataPointsPerWeek * i;

                // Data point value.
                min = $scope.dataMedians[i] - 100 + j * 2;
                max = $scope.dataMedians[i] + 20 + j * 2;
                y = $scope.randomDataPoint(min, max);

                //
                $scope.flotData[$scope.flotDataStartIndex + i].data.push([x++, y]);
            }
        }

        //
        // Flot chart setup finalization.
        //

        $scope.dataLastPoint = x;

        // Return to play line.
        $scope.flotData[$scope.flotData.length - 1].data.push([0, 2800]);
        $scope.flotData[$scope.flotData.length - 1].data.push([$scope.dataLastPoint, 2800]);


        ///
        /// Demo: Shoulder Range Of Motion
        ///
        /////////////////////////////////////////////////////////////////


        $scope.selectizeRomOptions = [
            {id: 1, title: 'Passive Range of Motion'},
            {id: 2, title: 'Active Range of Motion'},
        ];

        $scope.selectedRom = $scope.selectizeRomOptions[0];

        $scope.selectizeRomModel = 1;

        $scope.selectizeRomConfig = {
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
                angular.forEach($scope.selectizeRomOptions, function(rom) {
                    if (rom.id == id) {
                        $timeout(function() {
                            $scope.rom = rom;
                        });
                    }
                });
            }
        };

        $scope.colors = {
            extRot: '#28b9d7',      // External Rotation.
            intRot: '#469db1',      // Internal Rotation.
            flexion: '#3fd5ae',     // Flexion.
            extension: '#3ca88e',   // Extension.
            abduction: '#488bb7',   // Abduction.
            adduction: '#336180',   // Adduction.
        };

        $scope.romWeeks = [
            [1, 'Week 3'],
            [5, 'Week 4'],
            [9, 'Week 5'],
            [13, 'Week 6'],
            [17, 'Week 7'],
            [21, 'Week 8'],
            [25, 'Week 9'],
            [29, 'Week 10'],
            [33, 'Week 11'],
            [37, 'Week 12'],
            [41, 'Week 13'],
            [45, 'Week 14'],
        ];

        $scope.romScale = [
            [-100, '100%'],
            [-80, '80%'],
            [-60, '60%'],
            [-40, '40%'],
            [-20, '20%'],
            [0, '0%'],
            [20, '20%'],
            [40, '40%'],
            [60, '60%'],
            [80, '80%'],
            [100, '100%'],
        ];

        $scope.flotRomLabels = [
            // {
            //     color: Utilities.color.darkBlue,
            //     point: {x: 0, y: 97},
            //     lineWidth: 3,
            //     styles: {
            //         padding: '5px 10px',
            //         color: Utilities.color.silver,
            //         'border-left': '4px solid #fff',
            //         'background': 'linear-gradient(to right, '+ Utilities.color.textColorBlue +' 0%, transparent 30%)'
            //     },
            //     text: 'Passive Range of Motion'
            // }
        ];

        $scope.flotRomOptions = {
            grid: {
                borderWidth: 0,
                borderColor: 'rgba(0, 0, 0, 0)',
                clickable: false,
                hoverable: true,
                markings: [
                    // {
                    //     // color: Utilities.color.textColorBlue,
                    //     color: 'rgba(91, 112, 125, 0.5)',
                    //     xaxis: {from: 0, to: 15}
                    // },
                    // {
                    //     // color: Utilities.color.textColorBlue,
                    //     color: 'rgba(91, 112, 125, 0.4)',
                    //     xaxis: {from: 16, to: 47}
                    // },
                    // {
                    //     color: '#888',
                    //     lineWidth: 5,
                    //     yaxis: {from: 0, to: 0}
                    // },
                ],
                // thresholdZones: {
                //     mirror: true,
                //     min: 0,
                //     max: 100,
                //     total: 5
                // }
            },
            legend: {
                show: false
            },
            series: {
                bars: {
                    barWidth: 0.95,
                    fill: 1,
                    show: true
                },
                hoverable: true
            },
            xaxis: {
                color: 'transparent',
                min: -1,
                max: 48,
                ticks: $scope.romWeeks
            },
            yaxis: {
                color: 'rgba(91, 112, 125, 0.4)',
                min: -110,
                max: 110,
                ticks: $scope.romScale
            },
            tooltip: true,
            tooltipOpts: {
                defaultTheme:false
            },
            colors: [
                $scope.colors.extRot,
                $scope.colors.intRot,
                $scope.colors.flexion,
                $scope.colors.extension,
                $scope.colors.abduction,
                $scope.colors.adduction,
                '#ddd'
            ]
        };

        $scope.flotRomData = [

            // External Rotation.
            {
                data: [
                    [0, 58],
                    [4, 63],
                    [8, 71],
                    [12, 89],
                    [16, 91],
                    [20, 90],
                    [24, 96],
                    [28, 99],
                    [32, 97],
                    [36, 101],
                    [40, 103],
                    [44, 100],
                ]
            },

            // Internal Rotation.
            {
                data: [
                    [0, -48],
                    [4, -61],
                    [8, -74],
                    [12, -89],
                    [16, -96],
                    [20, -98],
                    [24, -94],
                    [28, -99],
                    [32, -102],
                    [36, -98],
                    [40, -101],
                    [44, -97],
                ]
            },

            // Flexion.
            {
                data: [
                    [1, 51],
                    [5, 60],
                    [9, 73],
                    [13, 91],
                    [17, 90],
                    [21, 93],
                    [25, 99],
                    [29, 100],
                    [33, 104],
                    [37, 99],
                    [41, 101],
                    [45, 99],
                ]
            },

            // Extension.
            {
                data: [
                    [1, -29],
                    [5, -33],
                    [9, -38],
                    [13, -57],
                    [17, -64],
                    [21, -70],
                    [25, -86],
                    [29, -92],
                    [33, -87],
                    [37, -93],
                    [41, -99],
                    [45, -102],
                ]
            },

            // Abduction.
            {
                data: [
                    [2, 12],
                    [6, 20],
                    [10, 22],
                    [14, 39],
                    [18, 48],
                    [22, 63],
                    [26, 58],
                    [30, 72],
                    [34, 78],
                    [38, 81],
                    [42, 84],
                    [46, 87],
                ]
            },

            // Adduction.
            {
                data: [
                    [2, -31],
                    [6, -33],
                    [10, -38],
                    [14, -42],
                    [18, -44],
                    [22, -49],
                    [26, -58],
                    [30, -62],
                    [34, -65],
                    [38, -69],
                    [42, -74],
                    [46, -83],
                ]
            },

            // 0 line
            {
                bars: {show: false},
                lines: {
                    color: '#ddd',
                    lineWidth: 2,
                    show: true
                },
                data: [
                    [-1, 0],
                    [48, 0],
                ]
            }
        ];

        $scope.flotActiveRomLabels = [
            // {
            //     color: Utilities.color.darkBlue,
            //     point: {x: 0, y: 97},
            //     lineWidth: 3,
            //     styles: {
            //         padding: '5px 10px',
            //         color: Utilities.color.silver,
            //         'border-left': '4px solid #fff',
            //         'background': 'linear-gradient(to right, '+ Utilities.color.textColorBlue +' 0%, transparent 30%)'
            //     },
            //     text: 'Active Range of Motion'
            // }
        ];

        $scope.flotActiveRomOptions = {
            grid: {
                borderWidth: 0,
                borderColor: 'rgba(0, 0, 0, 0)',
                clickable: false,
                hoverable: true,
                markings: [
                    // {
                    //     color: '#888',
                    //     lineWidth: 5,
                    //     yaxis: {from: 0, to: 0}
                    // },
                ],
                // thresholdZones: {
                //     mirror: true,
                //     min: 0,
                //     max: 100,
                //     total: 5
                // }
            },
            legend: {
                show: false
            },
            series: {
                bars: {
                    barWidth: 0.95,
                    fill: 1,
                    show: true
                },
                hoverable: true
            },
            xaxis: {
                color: 'rgba(0, 0, 0, 0)',
                min: -1,
                max: 48,
                ticks: $scope.romWeeks
            },
            yaxis: {
                color: 'rgba(91, 112, 125, 0.4)',
                min: -110,
                max: 110,
                ticks: $scope.romScale
            },
            tooltip: true,
            tooltipOpts: {
                defaultTheme:false
            },
            colors: [
                $scope.colors.extRot,
                $scope.colors.intRot,
                $scope.colors.flexion,
                $scope.colors.extension,
                $scope.colors.abduction,
                $scope.colors.adduction,
                '#ddd'
            ]
        };

        $scope.flotActiveRomData = [

            // External Rotation.
            {
                data: [
                    // [0, 0],
                    [4, 28],
                    [8, 36],
                    [12, 55],
                    [16, 68],
                    [20, 81],
                    [24, 91],
                    [28, 89],
                    [32, 96],
                    [36, 97],
                    [40, 99],
                    [44, 100],
                ]
            },

            // Internal Rotation.
            {
                data: [
                    // [0, -0],
                    [4, -27],
                    [8, -30],
                    [12, -53],
                    [16, -66],
                    [20, -73],
                    [24, -82],
                    [28, -91],
                    [32, -88],
                    [36, -94],
                    [40, -94],
                    [44, -97],
                ]
            },

            // Flexion.
            {
                data: [
                    // [1, 0],
                    [5, 31],
                    [9, 36],
                    [13, 61],
                    [17, 66],
                    [21, 79],
                    [25, 93],
                    [29, 91],
                    [33, 94],
                    [37, 99],
                    [41, 101],
                    [45, 99],
                ]
            },

            // Extension.
            {
                data: [
                    // [1, -0],
                    [5, -29],
                    [9, -38],
                    [13, -57],
                    [17, -64],
                    [21, -70],
                    [25, -86],
                    [29, -92],
                    [33, -87],
                    [37, -93],
                    [41, -99],
                    [45, -102],
                ]
            },

            // Abduction.
            {
                data: [
                    // [2, 0],
                    [6, 19],
                    [10, 22],
                    [14, 39],
                    [18, 48],
                    [22, 63],
                    [26, 58],
                    [30, 72],
                    [34, 78],
                    [38, 81],
                    [42, 84],
                    [46, 87],
                ]
            },

            // Adduction.
            {
                data: [
                    // [2, -0],
                    [6, -10],
                    [10, -18],
                    [14, -27],
                    [18, -36],
                    [22, -49],
                    [26, -58],
                    [30, -71],
                    [34, -78],
                    [38, -77],
                    [42, -84],
                    [46, -89],
                ]
            },

            // 0 line
            {
                bars: {show: false},
                lines: {
                    color: '#ddd',
                    lineWidth: 2,
                    show: true
                },
                data: [
                    [-1, 0],
                    [48, 0],
                ]
            },
        ];

        $scope.flotRomHover = function (event, pos, item) {

            // Display tooltip for data point.
			if (item)
            {
                var sign = item.datapoint[1]/Math.abs(item.datapoint[1]),
                    top = sign > 0 ?
                        item.pageY - $('#demo-rom-tooltip').height() - 25 :
                        item.pageY + 15;

				$("#demo-rom-tooltip")
                    .html(
                        '<b>' + Math.abs($filter('number')(item.datapoint[1], 0)) + ' %</b>'
                    )
					.css({
                        top: top,
                        left: item.pageX - 17
                        // 'background-color': item.series.color
                        // 'background': 'linear-gradient(to right, rgba(91, 112, 125, 0.8) 0%, transparent 60%)'
                    })
					.fadeIn(200);

                $('#romPercent')
                    .html('<b>' + Math.abs($filter('number')(item.datapoint[1], 0)) + ' %</b>')
					.fadeIn(200);

                $timeout(function() {
                    $scope.romGaugeValue = ((item.datapoint[1] + 100) * 1000 / 200) > 1000 ?
                        1000 : (item.datapoint[1] + 100) * 1000 / 200;
                    $scope.romGaugeValuePercent = $filter('number')(item.datapoint[1], 0) + ' %';
                });
			}

            // Hide tooltip if no element is selected.
            else {
				$('#romPercent').fadeOut(200);
				$('#demo-rom-tooltip').hide();

                $timeout(function() {
                    $scope.romGaugeValue = 500;
                    $scope.romGaugeValuePercent = '';
                });
			}
		};

        // Create tooltip template element.
        $('<div id="demo-rom-tooltip"></div>').css({
            position: 'absolute',
            display: 'none',
            padding: '5px 10px',
            color: '#fff',
            'border': '1px solid ' + Utilities.color.textColorBlue,
            'background-color': Utilities.color.darkBlue,
            'text-align': 'center',
            opacity: 1
        }).appendTo('body');

        // Gauge
        $scope.romGaugeValue = 500;
        $scope.romGauge =
        {
            data: {
                maxValue: 1000,
                animationSpeed: 4,
                val: $scope.romGaugeValue
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
                    [0, '#6eb4d2'],
                    [0.5, '#6eb4d2'],
                    [1, '#6eb4d2']
                ]
            }
        };


        ///
        /// Demo: Simulate data fetching.
        ///
        /////////////////////////////////////////////////////////////////


        $scope.fetchDataDemo = function(newData, oldData) {

            // Performance check.
            if ($scope.isFetchingData || !newData) {
                return;
            }

            $scope.isFetchingData = true;

            $timeout(function() {
                $scope.isFetchingData = false;
            }, 800);
        };

        $scope.$watch('metric', $scope.fetchDataDemo);
    }
]);
