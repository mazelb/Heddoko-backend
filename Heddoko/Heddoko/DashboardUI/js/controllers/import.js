/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for movement data import.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.controllers')

.controller('ImportController', ['$scope', '$timeout', 'MovementService', 'Upload', 'Rover', 'Utilities',
    function($scope, $timeout, MovementService, Upload, Rover, Utilities) {
        Utilities.info('ImportController');

        // Setup import data.
        Utilities.data.import = Utilities.data.import || {};
        Utilities.data.import.progress = Utilities.data.import.progress || 0;
        Utilities.data.import.queue = Utilities.data.import.queue || [];
        Utilities.data.import.queueTotal = Utilities.data.import.queueTotal || 0;
        Utilities.data.import.status = Utilities.data.import.status || '';
        Utilities.data.isImporting = Utilities.data.isImporting || false;
        if (!Utilities.hasDataNamespace('importedMovements')) {
            Utilities.createDataNamespace('importedMovements');
        }

        /**
         * Launches the "import chain" to import several movement files. We avoid using
         * HTML5-specific methods for backwards compatibility (e.g. IE8, IE9).
         *
         * @param array files
         */
        $scope.startImport = function(files) {

            // Performance check.
            if (!files) {
                return;
            }

            // Turn on "uploading" flag.
            Utilities.data.isImporting = true;
            Utilities.data.import.queue = files;
            Utilities.log('Uploading ' + files.length + ' movement files...');

            // Start uploading files.
            Utilities.data.import.queueTotal = files.length;
            Utilities.data.import.progress = 0;
            $scope.chainImport();
        };

        /**
         * Uploads data files to the server.
         */
        $scope.chainImport = function() {

            // Performance check.
            if (Utilities.data.import.queue.length < 1) {
                Utilities.data.isImporting = false;
                return Utilities.log('Uploading done.');
            }

            // Retrieve file to be uploaded.
            var file = Utilities.data.import.queue[0];
            Utilities.log('Uploading "' + file.name + '"...');
            Utilities.data.import.status = 'Importing "' + file.name + '"...';

            Upload.upload({
                url: '/api/v1/movements',
                data: {
                    file: file,
                    profileId: $scope.global.getSelectedProfile().id
                }
            }).then(
                function (response) {

                    // Add the new movement to the top of the list.
                    Utilities.setData('importedMovements', response.data.id, response.data);
                    // Utilities.data.import.imported.unshift(response.data);

                    // Update the import progress and continue uploading.
                    Utilities.data.import.queue.splice(0, 1);
                    Utilities.data.import.progress =
                        Math.round(
                            (Utilities.data.import.queueTotal -
                                Utilities.data.import.queue.length) * 100 /
                                Utilities.data.import.queueTotal);
                    $scope.chainImport();
                },
                function (response) {
                    Utilities.error(response.status + ': ' + response.data);
                    Utilities.alert('Could not import "' + file.name + '". Please try again later.');

                    // Update import progress.
                    Utilities.data.import.queue.splice(0, 1);
                    Utilities.data.import.progress =
                        Math.round(
                            (Utilities.data.import.queueTotal -
                                Utilities.data.import.queue.length) * 100 /
                                Utilities.data.import.queueTotal);

                    // Continue with the import process.
                    $scope.chainImport();
                },
                function (event) {

                    // We use "event" to try and make the import progress more accurate.
                    Utilities.data.import.progress =
                        Math.round(
                            (Utilities.data.import.queueTotal -
                                Utilities.data.import.queue.length +
                                Math.min(1, event.loaded / event.total)) * 100 /
                                Utilities.data.import.queueTotal);
                }
            );
        };

        // Opens the thumbnail overlay.
        $scope.selectThumbnail = function() {
            Rover.openThumbnailSelector();
        };

        // Opens the movement editor overlay.
        $scope.editMovement = function() {
            Rover.openMovementEditor();
        };

        // Deletes a movement.
        $scope.deleteMovement = function(movement) {
            Utilities.time('Deleting Movement');

            // Turn on "updating" flag
            movement.isUpdating = true;

            MovementService.destroy(movement.id).then(

                // On success, update profile list and browse to selected group.
                function(response) {
                    Utilities.timeEnd('Deleting Movement');

                    // Remove deleted movements.
                    Utilities.setData('importedMovements', movement.id, null);
                },

                // On failure.
                function(response) {
                    Utilities.timeEnd('Deleting Movement');
                    Utilities.error('Could not delete movement: ' + response.responseText);
                    Utilities.alert('Could not delete movement file. Please try again later.');
                    movement.isUpdating = false;
                }
            );
        };
    }
]);
