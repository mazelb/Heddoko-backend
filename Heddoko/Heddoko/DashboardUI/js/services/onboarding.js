/**
 *
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 *
 * @brief   This service takes care of onboarding visuals using Intro.js
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.services')

.service('OnboardingService', ['$timeout', 'Rover',
    function($timeout, Rover) {

        // Shortcut to retrieve onboarding elements and create the options object for Intro.js
        this.getOptions = function(elements) {

            var rawItems = [],
                sortedItems = [],
                genericStepCount = $(elements).length,
                nodePriority;

            // Loop through onboarding items and try to sort them by priority.
            angular.forEach($(elements), function(node) {

                // Set node priority.
                nodePriority = $(node).data('step') ? $(node).data('step') : genericStepCount;

                // Format items by priority.
                rawItems[nodePriority] = {
                    element: node,
                    intro: $(node).data('intro'),
                    position: $(node).data('position') || 'bottom'
                };

                genericStepCount++;
            });

            // Rebuild the list with the right order.
            angular.forEach(rawItems, function(item) {
                sortedItems.push(item);
            });

            // Return the default options.
            return {
                steps: sortedItems,
                showStepNumbers: false,
                showBullets: true,
                exitOnOverlayClick: true,
                exitOnEsc: true,
                nextLabel: '<i class="fa fa-angle-double-right"></i>',
                prevLabel: '<i class="fa fa-angle-double-left"></i>',
                skipLabel: 'I\'ll do this later',
                doneLabel: 'Thanks!'
            };
        };

        // General onboarding.
        this.general = function(step) {

            // Intro.js object.
            var intro = introJs();

            // Intro.js options.
            intro.setOptions(this.getOptions('.onboarding-general'));

            // Launch onboarding.
            if (typeof(step) === 'number') {
                intro.goToStep(step).start();
            } else {
                intro.start();
            }
        }.bind(this);
    }
]);
