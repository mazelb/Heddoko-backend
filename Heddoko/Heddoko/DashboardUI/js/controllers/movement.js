/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Controller for movement data.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    December 2015
 */
angular.module('app.controllers')

.controller('MovementController', ['$scope', '$routeParams', '$window', 'FolderService', 'MovementService', 'Rover', 'Utilities',
    function($scope, $routeParams, $window, FolderService, MovementService, Rover, Utilities) {
        Utilities.info('MovementController');

        // Setup controller.
        $scope.folderId = $routeParams.folderId;
        $scope.newFolder = {
            name: ''
        };

        // Config for uiFilesystem.
        $scope.path = '/';
        $scope.files = [];
        $scope.folders = [];
        $scope.parentFolder = false;
        $scope.uiFilesystemConfig = {
            toolbar: {
                createModal: $routeParams.rootId ? 'createFolderForm' : false,
                createModalIcon: 'plus'
            },

            /**
             * Selects a resource
             *
             * @param stinrg type
             * @param object resource
             */
            onSelect: function(type, resource) {
                var namespace, data;

                switch (type)
                {
                    case 'file':
                        namespace = 'selectedMovementFiles';
                        break;

                    case 'folder':
                        namespace = 'selectedMovementFolders';
                        break;

                    default:
                        Utilities.log('Invalid resource type: ' + type);
                        return;
                }

                // Update selected list.
                Utilities.setData(namespace, resource.id, (resource.selected ? {id: resource.id} : null));
            },

            /**
             * Opens the movement playback.
             *
             * @param object movement
             */
            onAnalyzeFile: function(movement) {
                Rover.browseTo.path('/movements/analyze/' + movement.id);
            },

            /**
             * Opens the movement comparison screen.
             *
             * @param object movement
             */
            onCompareFile: function(movement) {
                // TODO
            },

            /**
             * Edits a movement.
             *
             * @param object movement
             */
            onEditFile: function(movement) {
                // TODO
            },

            /**
             * Edits a folder name.
             *
             * @param object folder
             */
            onEditFolder: $routeParams.rootId ? function(folder) {
                // TODO
            } : false,

            /**
             * Shares a movement.
             *
             * @param object file
             */
            onShareFile: function(file) {
                Utilities.log('Sharing movement: ' + file.title);

                Utilities.alert('In Development.');
            },

            /**
             * Shares a folder.
             *
             * @TODO
             *
             * @param object folder
             */
            onShareFolder: function(folder) {
                Utilities.log('Sharing folder: ' + folder.title);

                Utilities.alert('In Development.');
            },

            /**
             * Deletes the specified movements.
             *
             * @param int|array IDs
             */
            onDeleteFile: function(IDs) {

                // Confirm.
                if ($window.confirm('Delete movement(s)?'))
                {
                    Utilities.time('Deleting Movements');

                    // Turn on flag
                    Utilities.data.isFetchingMovementData = true;

                    // Make sure we have an array.
                    IDs = typeof IDs == 'object' ? IDs : [IDs];

                    MovementService.destroy(IDs.join()).then(

                        // On success, update profile list and browse to selected group.
                        function(response) {
                            Utilities.timeEnd('Deleting Movements');

                            // Remove deleted movements.
                            var newList = [], i;
                            for (i = 0; i < $scope.files.length; i++)
                            {
                                Utilities.setData('selectedMovementFiles', IDs[i], null);

                                if (IDs.indexOf($scope.files[i].id) === -1) {
                                    newList.push($scope.files[i]);
                                }
                            }

                            $scope.files = newList;

                            Utilities.data.isFetchingMovementData = false;
                        },

                        // On failure.
                        function(response) {
                            Utilities.timeEnd('Deleting Movements');
                            Utilities.error('Could not delete movements: ' + response.responseText);
                            Utilities.alert('Could not delete movements. Please try again later.');
                            Utilities.data.isFetchingMovementData = false;
                        }
                    );
                }
            },

            /**
             * Deletes the specified folders.
             *
             * @param int|array IDs
             */
            onDeleteFolder: $routeParams.rootId ? function(IDs) {

                // Confirm.
                if ($window.confirm('Delete folder(s)?'))
                {
                    Utilities.time('Deleting Folders');

                    // Turn on flag
                    Utilities.data.isFetchingMovementData = true;

                    // Make sure we have an array.
                    IDs = typeof IDs == 'object' ? IDs : [IDs];

                    FolderService.destroy($scope.rootProfile.id, IDs.join()).then(

                        // On success, update profile list and browse to selected group.
                        function(response) {
                            Utilities.timeEnd('Deleting Folders');

                            // Remove deleted folders.
                            var newList = [], i;
                            for (i = 0; i < $scope.folders.length; i++)
                            {
                                Utilities.setData('selectedMovementFolders', IDs[i], null);

                                if (IDs.indexOf($scope.folders[i].id) === -1) {
                                    newList.push($scope.folders[i]);
                                }
                            }

                            $scope.folders = newList;

                            Utilities.data.isFetchingMovementData = false;
                        },

                        // On failure.
                        function(response) {
                            Utilities.timeEnd('Deleting Folders');
                            Utilities.error('Could not delete folders: ' + response.responseText);
                            Utilities.alert('Could not delete folders. Please try again later.');
                            Utilities.data.isFetchingMovementData = false;
                        }
                    );
                }
            } : false,

            /**
             *
             */
            onDeleteSelected: function() {
                Utilities.log('Deleting selected resources...');

                // Delete folders.
                if (Utilities.getDataLength('selectedMovementFolders')) {
                    this.onDeleteFolder(Utilities.getDataArray('selectedMovementFolders').map(Utilities.getId));
                }

                // Delete movements.
                if (Utilities.getDataLength('selectedMovementFiles')) {
                    this.onDeleteFile(Utilities.getDataArray('selectedMovementFiles').map(Utilities.getId));
                }
            }
        };

        // Fetching movement data flag.
        Utilities.data.isFetchingMovementData = false;

        // Setup namespaces.
        Utilities.createDataNamespace('selectedMovementFiles');
        Utilities.createDataNamespace('selectedMovementFolders');

        $scope.rootProfile = false;

        /**
         * Generates the location hash for a folder.
         *
         * @param object folder
         * @param object parent
         */
        $scope.getHash = function(folder, parent) {

            var hash = '#/movements/' + folder.profileId + '/';

            // Add the folder ID.
            hash += parent ? parent.id : folder.id;

            // Add the pathname.
            if (folder.path && folder.path.length > 1) {
                hash += '/' + folder.path.substr(1).replace('/', '_').replace(/\s+/g, '-');
            }

            // If no parent was passed, include the current folder name.
            if (!parent && folder.name) {
                hash += (folder.path == '/' ? '/' : '_') + folder.name.replace(/\s+/g, '-');
            }

            return hash;
        };

        /**
         * Updates the view with the root folders (one for each profile).
         *
         * @param object profiles
         */
        $scope.updateRootFolders = function(profiles) {

            // List of profiles.
            profiles = profiles || Utilities.getDataList('profile');

            // Reset data.
            $scope.files = [];
            $scope.folders = [];
            Utilities.resetDataNamespace('selectedMovementFiles');
            Utilities.resetDataNamespace('selectedMovementFolders');

            if (profiles.length)
            {
                for (var key in profiles)
                {
                    if (profiles[key].id && profiles[key].id > 0)
                    {
                        $scope.folders.push({
                            title: profiles[key].firstName + ' ' + profiles[key].lastName,
                            createdAt: profiles[key].createdAt,
                            updatedAt: profiles[key].updatedAt,
                            href: '#/movements/' + profiles[key].id
                        });
                    }
                }
            }

            Utilities.data.isFetchingMovementData = false;
        };

        /**
         * Updates the view with non-root folders.
         *
         * @param array folders
         */
        $scope.updateFolders = function(folders) {

            // Reset data.
            $scope.folders = [];
            Utilities.resetDataNamespace('selectedMovementFolders');

            if (folders && folders.length)
            {
                for (var i = 0; i < folders.length; i++)
                {
                    $scope.folders.push({
                        id: folders[i].id,
                        name: folders[i].name,
                        title: folders[i].name,
                        createdAt: folders[i].createdAt,
                        updatedAt: folders[i].updatedAt,
                        href: $scope.getHash(folders[i]),
                    });
                }
            }
        };

        /**
         * Retrieves movement data from the API.
         *
         * @param int folderId
         */
        $scope.fetchMovementData = function(folderId) {
            Utilities.time('Fetching Movement Data');

            Utilities.data.isFetchingMovementData = true;
            folderId = folderId || 0;

            FolderService.get($scope.rootProfile.id, folderId, ['parent', 'children', 'movements']).then(
                function(response) {
                    Utilities.timeEnd('Fetching Movement Data');

                    // Update folders.
                    $scope.updateFolders(response.data.children);

                    // Set parent folder
                    $scope.parentFolder = {
                        href: '#/movements'
                    };
                    if (folderId > 0) {
                        $scope.parentFolder.href += '/' + $scope.rootProfile.id;
                    }

                    if (response.data.parent)
                    {
                        $scope.parentFolder.name = response.data.parent.name;
                        $scope.parentFolder.href = $scope.getHash(response.data, response.data.parent);
                    }

                    // Update virtual path.
                    if (response.data.path)
                    {
                        if (response.data.path.length > 1)
                        {
                            $scope.path += response.data.path.replace('/', ' / ') + ' / ' +
                                            response.data.name;
                        }
                        else
                        {
                            $scope.path += ' / ' + response.data.name;
                        }
                    }

                    // Update movement data.
                    $scope.files = [];
                    Utilities.resetDataNamespace('selectedMovementFiles');
                    for (var i = 0; i < response.data.movements.length; i++)
                    {
                        $scope.files.push({
                            id: response.data.movements[i].id,
                            title: response.data.movements[i].title,
                            createdAt: response.data.movements[i].createdAt,
                            updatedAt: response.data.movements[i].updatedAt,
                            href: '#/movements/analyze/' + response.data.movements[i].id,
                        });
                    }

                    Utilities.data.isFetchingMovementData = false;
                },
                function(response) {
                    Utilities.timeEnd('Fetching Movement Data');
                    Utilities.alert('Could not retrieve movement data. Please try again later.');
                    Utilities.data.isFetchingMovementData = false;
                }
            );
        };

        /**
         * Opens the thumbnail overlay.
         */
        $scope.selectThumbnail = function() {
            Rover.openThumbnailSelector();
        };

        /**
         * Opens the movement editor overlay.
         */
        $scope.editMovement = function() {
            Rover.openMovementEditor();
        };

        /**
         * TODO: share movement.
         *
         * @param int id
         */
        $scope.shareMovement = function(id) {
            Utilities.log('Sharing movement #' + id);

            // TODO

            Utilities.alert('In Development.');
        };

        /**
         * Creates a new folder.
         *
         * @param string name
         */
        $scope.createFolder = function(name) {

            // Performance check.
            if (name.length < 1) {
                return;
            }

            // Reference to $scope.
            var scope = this;

            Utilities.time('Creating Folder');
            Utilities.data.isFetchingMovementData = true;

            Utilities.log('Creating folder "'+ name +'"');
            Utilities.log('Under the parent "'+ scope.folderId +'"');
            Utilities.log('For the profile "'+ scope.rootProfile.id +'"');

            FolderService.create(scope.rootProfile.id, {
                name: name,
                folderId: scope.folderId
            }).then(

                // On success
                function(response) {
                    Utilities.timeEnd('Creating Folder');

                    // Add new folder to folder list.
                    scope.folders.push(response.data);
                    scope.updateFolders(scope.folders);

                    Utilities.data.isFetchingMovementData = false;
                },

                // On failure.
                function(response) {
                    Utilities.timeEnd('Creating Folder');
                    Utilities.error('Could not create folder: ' + response.responseText);
                    Utilities.alert('Could not create folder. Please try again later.');
                    Utilities.data.isFetchingMovementData = false;
                }
            );
        }.bind($scope);

        // If a root folder was selected, try to display its contents.
        if ($routeParams.rootId)
        {
            if (Utilities.hasData('profile', $routeParams.rootId))
            {
                // Update path name and retrive movement data.
                $scope.rootProfile = Utilities.getData('profile', $routeParams.rootId);
                $scope.fetchMovementData($routeParams.folderId);
                $scope.path += ' ' + $scope.rootProfile.firstName + ' ' + $scope.rootProfile.lastName;
            }

            // If we're still loading profiles, wait for the results.
            else if (Utilities.data.isFetchingProfiles)
            {
                Rover.waitForFlag('isFetchingProfiles', false, $scope, function() {

                    // Update path name and retrive movement data.
                    $scope.rootProfile = Utilities.getData('profile', $routeParams.rootId);
                    $scope.fetchMovementData($routeParams.folderId);
                    $scope.path += ' ' + $scope.rootProfile.firstName + ' ' + $scope.rootProfile.lastName;
                });
            }

            // If a folder was selected, but the profile doesn't exit,
            // redirect the user to the root folders.
            else {
                Rover.browseTo.path('/movements');
            }
        }

        // If no root folder was selected, show the root folders.
        else
        {
            Rover.waitForFlag('isFetchingProfiles', false, $scope, $scope.updateRootFolders);
        }
    }
]);
