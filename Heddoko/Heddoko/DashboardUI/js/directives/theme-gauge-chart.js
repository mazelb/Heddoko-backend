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

.directive('themeGaugeChart', ['Utilities', function(Utilities) {
    return {
        scope: {
            gaugeData: "=",
            liveGaugeValue: "=?",
            gaugeOptions: "="
        },
        link: function(scope, ele) {
            var data, gauge, options;

            data = scope.gaugeData;
            options = scope.gaugeOptions;

            gauge = new Gauge(ele[0]).setOptions(options);
            gauge.maxValue = data.maxValue;
            gauge.animationSpeed = data.animationSpeed;
            gauge.set(data.val);

            if (scope.liveGaugeValue)
            {
                scope.$watch('liveGaugeValue', function(newVal) {
                    gauge.set(newVal);
                });
            }
        }
    };
}]);
