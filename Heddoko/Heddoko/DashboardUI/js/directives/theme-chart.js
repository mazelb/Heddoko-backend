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

.directive('themeChart', function () {
    var baseWidth = 600;
    var baseHeight = 400;

    return {
        restrict: 'E',
        template: '<canvas></canvas>',
        scope: {
            chartObject: '=value',
            data: '=',
            options: '=?'
        },
        link: function (scope, element, attrs) {
            var canvas  = element.find('canvas')[0],
                context = canvas.getContext('2d'),
                options = scope.options || {},
                chart;

            // Default options.
            options.type = attrs.type || options.type || 'Line';
            options.width = attrs.width || options.width || baseWidth;
            options.height = attrs.height || options.height || baseHeight;

            // Chart object
            canvas.width = options.width;
            canvas.height = options.height;
            chart = new Chart(context);

            chart[options.type](scope.data, options);

            // Adds data points.

            // Updates the chart.
            var redraw = function(value) {
                console.log('redraw()');
                if(!value) return;
                console.log('drawing...');

                var chartType = options.type;
                chart[chartType](scope.chartObject.data, scope.chartObject.options);
            };

            // Update when charts data changes
            scope.$watch(function() { return scope.chartObject; }, redraw);
            scope.$watch('scope.data', redraw);
        }
    };
}).directive('themeChartTimeout', ['$timeout', function ($timeout) {
    var baseWidth = 600;
    var baseHeight = 400;

    return {
        restrict: 'E',
        template: '<canvas></canvas>',
        scope: {
            chartObject: '=value',
            data: '=',
            timeout: '=',
            options: '=?'
        },
        link: function (scope, element, attrs) {
            var canvas  = element.find('canvas')[0],
                context = canvas.getContext('2d'),
                options = scope.options || {},
                chart;

            // Default options.
            options.type = attrs.type || options.type || 'Line';
            options.width = attrs.width || options.width || baseWidth;
            options.height = attrs.height || options.height || baseHeight;

            // Chart object
            canvas.width = options.width;
            canvas.height = options.height;
            chart = new Chart(context);

            chart[options.type](scope.data, options);

            // Adds data points.

            // Updates the chart.
            var redraw = function(value) {
                console.log('redraw()');
                if(!value) return;
                console.log('drawing...');

                var chartType = options.type;
                chart[chartType](scope.chartObject.data, scope.chartObject.options);
            };

            // Update when charts data changes
            scope.$watch(function() { return scope.chartObject; }, redraw);
            scope.$watch('scope.data', redraw);
        }
    };
}]);
