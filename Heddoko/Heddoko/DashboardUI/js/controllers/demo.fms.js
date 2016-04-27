/**
 * @file    fms.js
 * @brief   Controller for demo FMS tests. Kept separate so that the actual FMS tests can be
 *          developed independently.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    October 2015
 */
angular.module('app.controllers')

.controller('FMSDemoController', ['$scope', '$routeParams', 'FMSDemoFactory', 'Rover',
    function($scope, $routeParams, FMSDemoFactory, Rover) {

        // Dev.
        Rover.debug('FMSDemoController');
        $scope.isDemo = true;
        $scope.assetVersion = '0.5.6';

        // Scope parameters.
        $scope.params = $routeParams;
        $scope.params.step = $scope.params.step || 'test';

        // Other scope variables.
        $scope.isTestLive = false;
        $scope.fms = FMSDemoFactory.data;

        // Test-related methods.
        $scope.run =
        {
            name: null,

            // Records a test run.
            start: function()
            {
                if (!this.name) {
                    this.prepare();
                }

                Rover.debug('Starting run: ' + this.name);

                this.setStatus('live');
                $scope.isTestLive = true;

                // Play demo videos...
                $('.demo-test').each(function() {
                    this.currentTime = 0;
                    this.play();
                });
            },

            // Stops a test run.
            end: function()
            {
                Rover.debug('Ending run: ' + this.name);

                this.setStatus('saved');
                $scope.isTestLive = false;

                // Stop demo videos...
                $('.demo-test').each(function() {
                    this.pause();
                });

                this.moveToNextTrial();
            },

            // Stops a test run and flags it as "pain".
            pain: function()
            {
                Rover.debug('Ending run (with pain): ' + this.name);

                this.setStatus('pain');
                $scope.isTestLive = false;

                // Stop demo videos...
                $('.demo-test').each(function() {
                    this.pause();
                });

                this.moveToNextTrial();
            },

            // Stops a test run without saving data.
            fault: function()
            {
                Rover.debug('Restarting run: ' + this.name);

                this.setStatus('pending');
                $scope.isTestLive = false;
                this.getRun().numFaults++;

                // Stop demo videos...
                $('.demo-test').each(function() {
                    this.pause();
                    this.currentTime = 0;
                });
            },

            // Restarts a run.
            reset: function()
            {
                // Performance check.
                if (!this.exists()) {
                    return;
                }

                // Reset an existing run.
                this.getRun().status = 'pending';
                this.getRun().numFaults = 0;
            },

            // ...
            moveToNextTrial: function(iteration, skipOtherIterations)
            {
                // Loop through trials in current or specified iteration.
                var i, trial, name, run;
                for (i = 0; i < FMSDemoFactory.data.current.trials.length; i++)
                {
                    // Check if trial has already been run.
                    trial = FMSDemoFactory.data.current.trials[i];
                    name = this.getName(trial, iteration);
                    run = this.getRun(name);
                    if (!run || run.status == 'pending')
                    {
                        // Automatically setup next trial.
                        FMSDemoFactory.data.current.trial = trial;
                        FMSDemoFactory.data.current.iteration = iteration || FMSDemoFactory.data.current.iteration;
                        this.prepare();

                        // Reset demo videos...
                        $('.demo-test').each(function() {
                            this.currentTime = 0;
                        });

                        Rover.debug('Automatic next trial: ' + name);
                        return true;
                    }
                }

                // If all trials have been run, loop through iterations in this test.
                if (!skipOtherIterations && FMSDemoFactory.data.current.iterations.length > 1)
                {
                    for (i = 0; i < FMSDemoFactory.data.current.iterations.length; i++)
                    {
                        if (this.moveToNextTrial(FMSDemoFactory.data.current.iterations[i], true)) {
                            return true;
                        }
                    }
                }
            },

            submit: function()
            {
                Rover.debug('Submitting demo FMS data...');

                FMSDemoFactory.data.runs = {};
                FMSDemoFactory.data.current.isTestSubmitted = true;

                $('.demo-test').each(function() {
                    this.currentTime = 0;
                });
            },

            // ...
            prepare: function()
            {
                // First, make sure we have selected an interation and trial.
                if (!FMSDemoFactory.data.current.iteration || FMSDemoFactory.data.current.iteration.length < 1) {
                    FMSDemoFactory.data.current.iteration = 'main';
                }
                if (!FMSDemoFactory.data.current.trial) {
                    FMSDemoFactory.data.current.trial = FMSDemoFactory.data.current.trials[0];
                }

                // Next, we generate a unique key for this test run.
                var key = this.getName();
                Rover.debug('Preparing test run: ' + key);

                // Check if test has already been run.
                if (this.exists(key)) {
                    Rover.debug('Test has already been run.');
                    return;
                }

                // Finally, we create an object to store the test results.
                this.name = key;
                FMSDemoFactory.data.runs[key] = $.extend(true, {}, FMSDemoFactory.runDataTemplate, {});
            },

            // ...
            getName: function(trial, iteration, test)
            {
                // Retrieve default objects.
                trial = trial || FMSDemoFactory.data.current.trial || FMSDemoFactory.data.current.trials[0];
                iteration = iteration || FMSDemoFactory.data.current.iteration || 'main';
                test = test || FMSDemoFactory.data.current;

                // Name format: "{fms_id}.{iteration}.{trial}"
                return test.id + '.' + iteration + '.' + trial.name;
            },

            // Retrieves a test run.
            getRun: function(key)
            {
                key = key || this.getName();
                return FMSDemoFactory.data.runs[key] ? FMSDemoFactory.data.runs[key] : null;
            },
            getRunByTrial: function(trial) {
                return this.getRun(this.getName(trial));
            },

            // Determines if a test has already been run.
            exists: function(key)
            {
                key = key || this.getName();
                var run = this.getRun(key);

                return run ? (run.status != 'pending') : false;
            },

            setStatus: function(status)
            {
                if (!this.name) {
                    this.prepare();
                }

                FMSDemoFactory.data.runs[this.name].status = status;
            },

            getStatus: function(trial)
            {
                // Get the status of a specific trial.
                if (trial)
                {
                    var key = this.getName(trial),
                        run = this.getRun(key);

                    return run ? run.status : null;
                }

                // Or the current trial.
                return this.name ? this.getRun().status : null;
            }
        };

        // Analysis-related variables.
        $scope.analysis =
        {
            trialPane: false,
            planePane: false,
            playbackRatePane: false,
            playbackRate: 0.5,

            play: function()
            {
                // Play demo videos...
                $('.demo-analysis').each(function() {
                    this.currentTime = 0;
                    this.play();
                });
            },

            setPlaybackRate: function(rate)
            {
                this.playbackRate = rate;

                // Update playback rate on demo videos...
                $('.demo-analysis').each(function() {
                    this.playbackRate = rate;
                });
            },

            pause: function()
            {
                // Pause demo videos...
                $('.demo-analysis').each(function() {
                    this.pause();
                });
            },

            reset: function()
            {
                // Reset demo videos...
                $('.demo-analysis').each(function() {
                    this.pause();
                    this.currentTime = 0;
                });
            }
        };

        // Summary-related data.
        $scope.summary =
        {
            joints:
            {
                all: false,
                hip: true,
                knee: false,
                select: function(name)
                {
                    for (var joint in this)
                    {
                        if (typeof this[joint] == 'boolean')
                        {
                            // Select or unselect joint.
                            this[joint] = (name == joint || name == 'all');
                        }
                    }
                }
            }
        };

        // Select a default trial.
        if (!FMSDemoFactory.data.current.trial) {
            FMSDemoFactory.data.current.trial = FMSDemoFactory.data.current.trials[0];
        }
    }
]);
