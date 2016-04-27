/**
 * @file    demo.js
 * @brief   The FMSDemoFactory is a temporary factory used for the demo "live FMS" screens.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    October 2015
 */
angular.module('app.services')

.service('FMSDemoFactory', ['Rover',
    function(Rover) {

        // Setups up the FMS data.
        Rover.state.fms_demo = Rover.state.fms_demo || {};
        this.data = Rover.state.fms_demo;

        // Individual test runs are stored by key, in the format "{fms_id}.{iteration}.{trial}".
        // e.g. "aslr.left.1"
        this.data.runs = this.data.runs || {};

        // Each test run will store results about the test. The following object serves as a
        // template.
        this.runDataTemplate =
        {
            status: 'pending',
            numFaults: 0
        };

        //
        var dataTemplate =
        {
            isTestLive: false,
            isTestSubmitted: false,
            iterations: [],
            iteration: '',
            trials: [
                {
                    name: 'Trial 1',
                    status: 'pending'
                },
                {
                    name: 'Trial 2',
                    status: 'pending'
                },
                {
                    name: 'Trial 3',
                    status: 'pending'
                }
            ],
            testRuns: {}
        };

        // List of demo FMS tests.
        this.data.list = [

            // Active Straight-Leg Raise
            $.extend(true, {}, dataTemplate, {
                id: 'aslr',
                name: 'Active Straight-Leg Raise',
                iterations: ['left', 'right'],
                iteration: 'left'
            })
        ];

        // Default demo: ASLR.
        this.data.current = this.data.list[0];

        // Default screen selection.
        this.data.views = ['sagittal', 'coronal', 'transverse'];
    }
]);
