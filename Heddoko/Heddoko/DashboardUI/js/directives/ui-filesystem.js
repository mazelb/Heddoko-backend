/**
 * Copyright Heddoko(TM) 2016, all rights reserved.
 *
 * @brief   Angular directive for filesystem layouts
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    February 2016
 * @note    Use as:
 *              <div
 *                  data-ui-filesystem
 *                  data-id="myFilesystemContainer"
 *                  data-files="myFiles">
 *              </div>
 */
angular.module('app.directives')

// Main container
.directive('uiFilesystem', function() {
    return {
        restrict: 'AE',
        transclude: true,
        scope: {
            id: '@?',

            files: '=',
            folders: '=',
            parentFolder: '=?',
            path: '=?',

            config: '=?',

            defaultLayout: '@?',
            hideLocation: '=?',
            hideToolbar: '=?',
            hideLargeTilesLayout: '=?',
            hideSmallTilesLayout: '=?',
            isLoading: '=?'
        },
        controller: ['$scope', '$timeout', 'Utilities', function($scope, $timeout, Utilities) {

            // Storage namespace.
            var namespace = $scope.namespace = $scope.id || 'ui-filesystem';

            // Configuration.
            var layoutKey = namespace + '-layout',
                selectedFilesKey = namespace + '-selected-files',
                selectedFoldersKey = namespace + '-selected-folders';
            $scope.config = $scope.config || {};
            $scope.config.toolbar = $scope.config.toolbar || {};
            $scope.config.onDeleteFile = $scope.config.onDeleteFile || false;
            $scope.config.onDeleteFolder = $scope.config.onDeleteFolder || false;
            $scope.config.onDeleteSelected = $scope.config.onDeleteSelected || false;
            $scope.config.onSelect = $scope.config.onSelect || false;
            $scope.config.detailsLayoutTitles = $scope.config.detailsLayoutTitles || [
                {
                    key: 'title',
                    title: 'Title'
                },
                {
                    key: 'updatedAt',
                    title: 'Modified'
                },
            ];

            // Defaults.
            $scope.defaultLayout = $scope.defaultLayout || 'details';
            $scope.hideLocation = ($scope.hideLocation);
            $scope.hideToolbar = ($scope.hideToolbar);
            $scope.hideLargeTilesLayout = ($scope.hideLargeTilesLayout);
            $scope.hideSmallTilesLayout = ($scope.hideSmallTilesLayout);
            $scope.isLoading = $scope.isLoading || false;

            // Setup layout data.
            $scope.layout = {
                name: '',
                list: []
            };

                // Large tile layout.
                if (!$scope.hideLargeTilesLayout) {
                    $scope.layout.list.push({
                        name: 'large-tiles',
                        icon: 'th-large'
                    });
                }

                // Small tile layout.
                if (!$scope.hideSmallTilesLayout) {
                    $scope.layout.list.push({
                        name: 'small-tiles',
                        icon: 'th'
                    });
                }

                // List layout (default).
                $scope.layout.list.push({
                    name: 'details',
                    icon: 'list'
                });

                // Selected layout.
                $scope.layout.name = Utilities.getConfig(layoutKey, $scope.defaultLayout);

            /**
             * Updates the current layout.
             *
             * @param string layout
             */
            $scope.setLayout = function(layout) {
                $scope.layout.name = layout;
                Utilities.setConfig(layoutKey, layout);
            };

            /**
             * Selects a file.
             *
             * @param object file
             */
            $scope.toggleFile = function(file) {
                file.selected = !file.selected;

                if ($scope.config.onSelect) {
                    $scope.config.onSelect('file', file);
                }
            };

            /**
             * Selects a folder.
             *
             * @param object folder
             */
            $scope.toggleFolder = function(folder) {
                folder.selected = !folder.selected;

                if ($scope.config.onSelect) {
                    $scope.config.onSelect('folder', folder);
                }
            };

            /**
             * Selects all files and folders.
             */
            $scope.toggleAll = function() {

                // Select all files.
                for (var i = 0; i < $scope.files.length; i++)
                {
                    $scope.files[i].selected = !$scope.files[i].selected;

                    if ($scope.config.onSelect) {
                        $scope.config.onSelect('file', $scope.files[i]);
                    }
                }

                // Select all folders.
                for (i = 0; i < $scope.folders.length; i++)
                {
                    $scope.folders[i].selected = !$scope.folders[i].selected;

                    if ($scope.config.onSelect) {
                        $scope.config.onSelect('folder', $scope.folders[i]);
                    }
                }
            };

            /**
             * Calls a method while preventing bubbling.
             *
             * @param object $event
             * @param function callback
             * @param array args
             */
            $scope.call = function($event, callback, args) {

                // Stop propagation.
                $event.preventDefault();

                // Call intended method.
                callback.apply(callback, args);
            };
        }],
        templateUrl: 'partials/directives/ui-filesystem/container.html'
    };
});
