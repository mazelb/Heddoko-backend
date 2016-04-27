/**
 *
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 *
 * @brief   Transfered and adapted from Admin Box Theme by Joao Garin
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    January 2016
 */
angular.module('app.directives')

.directive('themeFlotChartRealtime', [
    function() {
        return {
            restrict: 'A',
            link: function(scope, ele) {
                var data, getRandomData, plot, totalPoints, update, updateInterval;

                return data = [], totalPoints = 300, getRandomData = function() {
                    var i, prev, res, y;
                    for (data.length > 0 && (data = data.slice(1)); data.length < totalPoints;){
                        if(data.length > 0){
                            prev = data[data.length - 1];
                        }
                        else{
                            prev = 50;
                        }
                        y = prev + 10 * Math.random() - 5;
                        if(0 > y){
                            y = 0;
                        }else{
                            if(y > 100){
                                y = 100;
                            }
                        }
                        data.push(y);
                    }
                    for (res = [], i = 0; i < data.length;){
                        res.push([i, data[i]]);
                        ++i;
                    }
                    return res;
                }, update = function() {
                    plot.setData([getRandomData()]);
                    plot.draw();
                    setTimeout(update, updateInterval);
                }, data = [], totalPoints = 300, updateInterval = 200, plot = $.plot(ele[0], [getRandomData()], {
                    series: {
                        lines: {
                            show: !0,
                            fill: !0
                        },
                        shadowSize: 0
                    },
                    yaxis: {
                        min: 0,
                        max: 100,
                        show: !0,
                        color:"#f5f5f5"
                    },
                    xaxis: {
                        show: !0,
                        color:"#f5f5f5"
                    },
                    grid: {
                        hoverable: !0,
                        borderWidth: 1,
                        borderColor: "#fff"
                    },
                    colors: ["#383d43"]
                }), update();
            }
        };
    }
]);
