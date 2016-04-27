/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for analysis demo.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    January 2016
 */
angular.module('app.controllers')

.controller('DemoAnalysisController', ['$scope', '$timeout', 'Rover', 'Utilities',
    function($scope, $timeout, Rover, Utilities) {
        Utilities.debug('DemoAnalysisController');

        // Demo videos.
        $scope.videos = [
            document.getElementById('sagittalVideo'),
            document.getElementById('coronalVideo'),
            document.getElementById('transverseVideo')
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

        $scope.play = function() {
            Utilities.debug('play()');

            $scope.isPlaying = true;

            angular.forEach($scope.videos, function(video, i) {
                Utilities.debug('Playing video #' + i);
                video.play();
            });
        };

        $scope.pause = function() {
            Utilities.debug('pause()');

            $scope.isPlaying = false;

            angular.forEach($scope.videos, function(video, i) {
                Utilities.debug('Pausing video #' + i);
                video.pause();
            });
        };

        $scope.reset = function() {
            Utilities.debug('reset()');

            $scope.pause();

            angular.forEach($scope.videos, function(video, i) {
                Utilities.debug('Resetting video #' + i);

                video.currentTime = video.seekable.start(0);
            });
        };

        $scope.forward = function() {
            Utilities.debug('forward()');

            $scope.pause();

            angular.forEach($scope.videos, function(video, i) {
                Utilities.debug('Forwarding video #' + i);

                video.currentTime = video.currentTime + 1 >= video.seekable.end(0) ?
                    video.seekable.end(0) : video.currentTime + 1;
            });
        };

        $scope.backward = function() {
            Utilities.debug('backward()');

            $scope.pause();

            angular.forEach($scope.videos, function(video, i) {
                Utilities.debug('Backwarding video #' + i);

                video.currentTime = video.currentTime - 1 <= video.seekable.start(0) ?
                    video.seekable.start(0) :
                    video.currentTime - 1;
            });
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
