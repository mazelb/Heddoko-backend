/**
 *
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 *
 * @brief   Angular directive for editable avatars.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 * @note    Used as:
 *          <ui-avatar
 *              data-upload-endpoint="uploadAvatarEndpoint"
 *              data-success-callback="uploadAvatarCallback"
 *              data-src="avatarSrc">
 *          </ui-avatar>
 */
angular.module('app.directives')

.directive('uiAvatar', function() {
    return {
        restrict: 'E',
        transclude: true,
        templateUrl: 'partials/directives/ui-avatar.html',
        scope: {
            avatarSrc: '=src',
            uploadEndpoint: '=',
            successCallback: '=',
            containerWidth: '@matchWidth'
        },
        controller: ['$scope', '$timeout', 'Upload', 'Rover', 'Utilities',
            function($scope, $timeout, Upload, Rover, Utilities) {

                // Represents current state of directive.
                $scope.status = $scope.avatarSrc ? 'uploaded' : 'none';

                // Resizes the avatar container.
                $scope.containerWidth = $scope.containerWidth || 'auto';

                // Uploads a photo.
                $scope.upload = function(image) {

                    // Performance check.
                    if (!image) {
                        return;
                    }

                    // Upload image.
                    $scope.status = 'uploading';
                    Utilities.debug('Uploading avatar...');
                    Upload.upload({
                        url: $scope.uploadEndpoint,
                        data: {'image': image}
                    }).then(

                        // Update image on success.
                        function(response) {

                            $scope.avatarSrc = response.data.avatarSrc;
                            $scope.status = 'uploaded';

                            // Call successCallback if one was provided.
                            if (typeof $scope.successCallback == 'function') {
                                $timeout(function() {
                                    $scope.successCallback.call(response.data);
                                });
                            }
                        },

                        // Notify user on failure.
                        function(response) {

                            Utilities.alert('Could not upload avatar. Please try again later.');
                            $scope.status = $scope.avatarSrc ? 'uploaded' : 'none';
                        }
                    );
                };
            }
        ]
    };
});
