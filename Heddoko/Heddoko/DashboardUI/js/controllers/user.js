/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for user page.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.controllers')

.controller('UserController', ['$scope', 'UserService', 'Rover', 'Utilities',
    function($scope, UserService, Rover, Utilities) {
        Utilities.debug('UserController');

        // POST endpoint for avatar uploads.
        $scope.uploadAvatarEndpoint = '/api/v1/users/'+ Rover.userHash +'/avatar';

        // Computes the width of the avatar depending on the height of the details panel.
        $scope.calculateAvatarHeight = function() {
            return $('#userDetails') ? $('#userDetails').css('height') : 0;
        };

        // Callback for avatar uploads.
        $scope.uploadAvatarCallback = function() {

            // Update user data.
            $scope.user = Rover.state.user = this;
        };

        // Retrieve user data.
        $scope.user = Rover.state.user || {id: 0};
        if ($scope.user.id === 0)
        {
            Utilities.debug('Retrieving user details');

            UserService.get(Rover.userHash).then(

                // Update user data.
                function(response) {
                    $scope.user = Rover.state.user = response.data;
                },

                function(response) {
                    Utilities.alert('Could not retrieve user details. Please try again later.');
                    $scope.user = Rover.state.user = {id: 0};
                }
            );
        }

        // Saves user details.
        $scope.saveUserDetails = function() {
            return UserService.update(Rover.userHash, $scope.user);
        };

        // Callback for uiEditableFields directive.
        $scope.saveUserDetailsCallback = function(detailsSaved) {

            if (detailsSaved) {
                // ...
            }

            //
            else {
                Utilities.alert('Could not save your details. Please try again later.');
            }
        };
    }
]);
