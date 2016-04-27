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

.directive('themeFlotChart', ['Utilities',
    function(Utilities) {
        return {
            restrict: 'AE',
            scope: {
                data: '=',
                options: '=',
                plotClick: '=?',
                plotHover: '=?',
                plotLabels: '=?',
                threshold: '=?',
                thresholdLabel: '=?'
            },
            link: function(scope, element) {

                // Add hook for creating threshold label.
                if (scope.thresholdLabel)
                {
                    /**
                     * Adds a label to the threshold line.
                     *
                     * @param plot
                     * @param canvascontext
                     */
                    var writeThresholdLabel = function(plot, canvascontext) {

                        // Label colour and position.
                        var color = '#ddd',
                            top = 0,
                            left = plot.getPlotOffset().left,
                            plotData = plot.getData();

                        // Find threshold series.
                        angular.forEach(plot.getData(), function(series) {
                            if (series.isThresholdSeries)
                            {
                                // Check that threshold value is valid.
                                if (series.data[0][1] < plotData[0].yaxis.min || series.data[0][1] > plotData[0].yaxis.max) {
                                    top = 0;
                                    return;
                                }

                                // Set text colour for label.
                                if (series.updateColor) {
                                    series.color = series.updateColor(series.data[0][1]);
                                }
                                color = series.color;

                                // Calculate pixels from top for where label should appear.
                                top = (1 -
                                        (series.data[0][1] - plotData[0].yaxis.min) /
                                        (plotData[0].yaxis.max - plotData[0].yaxis.min)
                                    ) * (
                                        plot.getPlaceholder().height() -
                                        plot.getPlotOffset().top -
                                        plot.getPlotOffset().bottom
                                    ) + 10;
                            }
                        });

                        // Create label
                        if (!scope.thresholdLabelElement)
                        {
                            scope.thresholdLabelElement =
                            $('<div id="theme-flot-chart-threshold-label"></div>')
                                .html(scope.thresholdLabel)
                                .css({
                                    position: 'absolute',
                                    display: 'inline-block',
                                    padding: '5px 10px',
                                    'background-color': 'transparent',
                                    'text-shadow': '2px 2px #333',
                                    opacity: 0.9
                                })
                                .appendTo(plot.getPlaceholder());
                        }

                        // Set position.
                        scope.thresholdLabelElement.css({
                            top: top,
                            left: left,
                            color: color
                        });

                        if (top === 0) {
                            scope.thresholdLabelElement.fadeOut(200);
                        } else {
                            scope.thresholdLabelElement.fadeIn(200);
                        }
                    };

                    // Add hook.
                    scope.options.hooks = scope.options.hooks || {};
                    scope.options.hooks.draw = scope.options.hooks.draw || [];
                    scope.options.hooks.draw.push(writeThresholdLabel);
                }

                // Create hook for static labels.
                if (scope.plotLabels)
                {
                    /**
                     * Adds static labels for plot
                     *
                     * @param plot
                     * @param canvascontext
                     */
                    var writeStaticLabel = function(plot, canvascontext) {

                        // Create label containers.
                        if (!scope.staticLabelElements)
                        {
                            scope.staticLabelElements = [];

                            angular.forEach(scope.plotLabels, function(label, index) {

                                var offset = plot.pointOffset(label.point);
                                var styles = label.styles || {
                                    padding: '5px 10px',
                                    color: label.color || '#333',
                                    'background-color': label.backgroundColor || 'transparent',
                                    opacity: label.opacity || 0.9
                                };

                                scope.staticLabelElements.push(
                                    $('<div id="theme-flot-chart-label-'+ index +'"></div>')
                                    .html(label.text)
                                    .css($.extend(styles, {
                                        position: 'absolute',
                                        top: offset.top,
                                        left: offset.left,
                                        display: 'inline-block'
                                    }))
                                    .appendTo(plot.getPlaceholder()));
                            });
                        }
                    };

                    // Add hook.
                    scope.options.hooks = scope.options.hooks || {};
                    scope.options.hooks.draw = scope.options.hooks.draw || [];
                    scope.options.hooks.draw.push(writeStaticLabel);
                }

                // Create hook for drawing threshold zones.
                if (scope.options.grid.thresholdZones)
                {
                    scope.options.grid.markings = scope.options.grid.markings || [];

                    /**
                     * Adds static labels for plot
                     *
                     * @param plot
                     * @param convascontext
                     */
                    var drawThresholdZones = function(plot, convascontext) {

                        // Remove existing threshold zones.
                        var originalMarkings = [],
                            options = plot.getOptions(),
                            plotData = plot.getData(),
                            tzIncrement;
                        angular.forEach(options.grid.markings, function(marking) {
                            if (!marking.isThresholdZone) {
                                originalMarkings.push(marking);
                            }
                        });
                        options.grid.markings = originalMarkings;

                        // Calculate zone increments
                        tzIncrement = Math.round(
                            ((scope.threshold || options.grid.thresholdZones.max) - options.grid.thresholdZones.min) /
                            options.grid.thresholdZones.total
                        );

                        // Add threshold zones.
                        for (var i = 0; i < options.grid.thresholdZones.total; i++)
                        {
                            options.grid.markings.push({
                                isThresholdZone: true,
                                color: 'rgba(91, 112, 125, '+ (i * 0.5 / options.grid.thresholdZones.total) +')',
                                yaxis:
                                {
                                    from: i * tzIncrement + options.grid.thresholdZones.min,
                                    to: (i + 1) * tzIncrement + options.grid.thresholdZones.min
                                }
                            });

                            // Mirror the zone.
                            if (options.grid.thresholdZones.mirror)
                            {
                                options.grid.markings.push({
                                    isThresholdZone: true,
                                    color: 'rgba(91, 112, 125, '+ (i * 0.5 / options.grid.thresholdZones.total) +')',
                                    yaxis:
                                    {
                                        from: -1 * i * tzIncrement - options.grid.thresholdZones.min,
                                        to: -1 * i * tzIncrement - tzIncrement - options.grid.thresholdZones.min
                                    }
                                });
                            }
                        }
                    };

                    // Add hook.
                    scope.options.hooks = scope.options.hooks || {};
                    scope.options.hooks.drawBackground = scope.options.hooks.drawBackground || [];
                    scope.options.hooks.drawBackground.push(drawThresholdZones);
                }

                // Draw plot.
                var plot = $.plot(element[0], scope.data, scope.options);

                // Bind plot hover function.
                if (typeof scope.plotHover == 'function') {
                    $(element[0]).bind('plothover', scope.plotHover);
                }

                // Bind plot click function.
                if (typeof scope.plotClick == 'function') {
                    $(element[0]).bind('plotclick', scope.plotClick);
                }

                // If plot has a moveable threshold, redraw on update.
                if (scope.threshold)
                {
                    scope.$watch('threshold', function(newThreshold) {

                        // Update plot.
                        angular.forEach(scope.data, function(series) {

                            // Performance check.
                            if (series.isThresholdSeries)
                            {
                                series.data[0] = [0, newThreshold];
                                series.data[1] = [series.data[1][0], newThreshold];

                                if (series.updateColor) {
                                    series.color = series.updateColor(newThreshold);
                                }
                            }
                        });

                        // Redraw plot.
                        plot.setData(scope.data, scope.options);
                        plot.draw();
                    });
                }
            }
        };
    }
]);
