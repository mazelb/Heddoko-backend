/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for main landing page.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    December 2015
 */
angular.module('app.controllers')

.controller('DashboardController', ['$scope', 'Rover', 'Utilities',
    function($scope, Rover, Utilities) {
        Utilities.info('DashboardController');

        // Greeting.
        var hour = new Date().getHours();
        $scope.greeting = 'Good Morning';
        if (hour > 11 && hour < 17) {
            $scope.greeting = 'Good Afternoon';
        } else if (hour >= 17) {
            $scope.greeting = 'Good Evening';
        }

        // Bookmarks.
        // NOTE: these might be dynamically loaded in the furture
        $scope.bookmarks = [
            {
                title: 'Upload a Movement',
                uri: '#/movements/upload',
                icon: 'cloud-upload'
            },
            {
                title: 'Record a Movement',
                uri: '#/capture',
                icon: 'video-camera'
            },
            {
                title: 'Movement Tests',
                uri: '#/screenings',
                icon: 'pencil-square-o'
            },
            {
                title: 'Analyze a Movement',
                uri: '#/movements/analyze',
                icon: 'line-chart'
            }
        ];
    }
]);
