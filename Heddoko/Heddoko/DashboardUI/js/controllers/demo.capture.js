/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for analysis demo.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    January 2016
 */
angular.module('app.controllers')

.controller('DemoCaptureController', ['$scope', '$timeout', 'Rover', 'Utilities',
    function($scope, $timeout, Rover, Utilities) {
        Utilities.debug('DemoCaptureController');

        // FMS movements.
        $scope.movements = [];
        $scope.movementNames = [
            'Deep Squat',
            'Hurdle Step L',
            'Hurdle Step R',
            'Inline Lunge L',
            'Inline Lunge R',
            'Shoulder Mobility L',
            'Shoulder Mobility R',
            'Impingment L',
            'Impingment R',
            'Active Straight-Leg Raise L',
            'Active Straight-Leg Raise R',
            'Trunk Stability Push-Up',
            'Spinal Expansion',
            'Rotary Stability L',
            'Rotary Stability R',
            'Posterior Rocking',
        ];

        angular.forEach($scope.movementNames, function(name) {
            $scope.movements.push({
                name: name,
                done: false,
                current: false,
                score: 0
            });
        });
        
        $scope.movements[0].current = true;
        $scope.currentMovementIndex = 0;

        // Demo videos.
        $scope.videos = [
            document.getElementById('sagittalVideoCapture'),
            document.getElementById('coronalVideoCapture'),
            document.getElementById('transverseVideoCapture')
        ];

        angular.forEach($scope.videos, function(video) {

            // Disable volume.
            video.volume = 0;

            // Add "ended" event listener.
            video.addEventListener('ended', function() {
                $timeout($scope.reset);
            }, true);
        });

        $scope.isPlaying = false;

        $scope.start = function() {

            $scope.isPlaying = true;

            angular.forEach($scope.videos, function(video, i) {
                video.play();
            });
        };

        $scope.done = function() {

            $scope.isPlaying = false;

            angular.forEach($scope.videos, function(video, i) {
                video.pause();
                video.currentTime = video.seekable.start(0);
            });

            // Reset demos.
            if ($scope.currentMovementIndex == $scope.movements.length - 1)
            {
                angular.forEach($scope.movements, function(movement) {
                    movement.done = false;
                    movement.current = false;
                });

                $scope.movements[0].current = true;
                $scope.currentMovementIndex = 0;
            }

            // Move on to next demo.
            else
            {
                $scope.movements[$scope.currentMovementIndex].done = true;
                $scope.movements[$scope.currentMovementIndex].current = false;
                $scope.movements[++$scope.currentMovementIndex].current = true;
            }
        };

        // Adjusts the height of the main video according to height of secondary videos.
        $scope.adjustHeight = function() {
            var height = $('#secondaryViewsContainer').height() - 15;

            // Make sure the main view has the same height as the secondary views
            $('#emphasizedViewContainer').css('height', height + 'px');
            Utilities.debug('Main container height: ' + height);

            // And that video height scales with container.
            $('#emphasizedViewContainer video').css('height', (height * 0.6) + 'px');
            Utilities.debug('Main video height: ' + (height * 0.6));
        };

        angular.element(document).ready(function () {
            $timeout($scope.adjustHeight, 1000);
        });
    }
]);
