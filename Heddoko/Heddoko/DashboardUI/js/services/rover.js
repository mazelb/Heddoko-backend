/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   The rover service is used throughout the app and should be made available to other
 *          modules and controllers through dependency injection.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 *
 *  TODO: Move helper methods to Utilities service (e.g. getState, etc.)
 *  TODO: Keep only UI-changing methods (e.g. openOverlay, etc.) and programmatic methods.
 */
angular.module('app.rover', [])

.service('Rover', ['$window', '$localStorage', '$sessionStorage', '$route', '$location', '$anchorScroll', '$log', '$timeout', 'Utilities',
    function($window, $localStorage, $sessionStorage, $route, $location, $anchorScroll, $log, $timeout, Utilities) {

        // Dev variables.
        this.timestamp = Date.now();
        this.isLocal =
            (window.location.hostname == 'localhost' ||
                window.location.hostname.match(/.*\.local$/i) ||
                window.location.hostname.match(/.*\.vagrant$/i)) ? true : false;

        // User-specific hash. Used for user-specific data.
        // @deprecated
        this.userHash = Utilities.userHash;

        // User-namespaced storage.
        // @deprecated
        this.store = Utilities.store;
        this.state = Utilities.state;

        // Counts the # of requests being made, and displays the loading icon accordingly.
        // We start the counter at 1 and decrement it once the application is running.
        // TODO: show a visual representation of the backgroundProcessCount variable.
        this.backgroundProcessCount = 1;
        this.addBackgroundProcess = function() {

            this.backgroundProcessCount++;
            Utilities.info('Background processes: ' + this.backgroundProcessCount);

            // Show loading animation.
            if (this.backgroundProcessCount === 1) {
                Utilities.showLoading();
            }
        }.bind(this);
        this.doneBackgroundProcess = function() {

            if (this.backgroundProcessCount > 0) {
                this.backgroundProcessCount--;
            }

            Utilities.info('Background processes: ' + this.backgroundProcessCount);

            // Remove loading animation. We delay this by half a second to let the app's
            // bindings to update
            if (this.backgroundProcessCount < 1)
            {
                $timeout(function() {
                    Utilities.hideLoading();
                }, 500);

                return;
            }
        }.bind(this);

        /**
         * Calls a method once a global flag becomes true or false.
         *
         * @param string flag
         * @param object $scope
         * @param mixed expectedStatus
         * @param function callback
         */
        this.waitForFlag = function(flag, expectedStatus, $scope, callback) {

            // Performance check.
            if (typeof Utilities.data[flag] === undefined) {
                return;
            }

            // If flag is already set to expected status, call callback function.
            if (Utilities.data[flag] == expectedStatus) {
                callback();
                return;
            }

            // If not, we setup a temporary watcher.
            var stopWatching = $scope.$watch('global.data.' + flag, function(status) {
                if (status == expectedStatus)
                {
                    // Run method.
                    callback();

                    // Remove binding.
                    stopWatching();
                }
            });
        };

        // Shortcut to browse through app.
        this.browseTo = {

            /**
             * Dashboard index page.
             */
            dashboard: function() {
                $location.path('/dashboard');
            }.bind(this),

            /**
             * Group listing page.
             */
            groups: function() {
                $location.path('/group');
            }.bind(this),

            /**
             * Group page.
             *
             * @param object|int group
             */
            group: function(group) {

                // Update the selected group.
                if (group !== undefined) {
                    Utilities.store.groupId = Utilities.getId(group);
                }

                $location.path('/groups/' + Utilities.store.groupId);
            }.bind(this),

            /**
             * Profile page.
             *
             * @param object|int profile
             */
            profile: function(profile) {

                // Update the selected profile.
                if (profile !== undefined) {
                    Utilities.store.profileId = Utilities.getId(profile);
                }

                $location.path('/profiles/' + Utilities.store.profileId);
            }.bind(this),

            /**
             * General page.
             *
             * @param string path
             */
            path: function(path) {
                $location.path(path);
            }.bind(this),

            hash: function(hash) {

                // Update location hash.
                if ($location.hash() !== hash) {
                    $location.hash(hash);
                }

                // Or scroll to current hash.
                else {
                    $anchorScroll();
                }
            }
        };

        //
        // Overlays.
        //

        // Overlay data.
        this.overlayData = {
            title: '',
            bodyTemplate: '',
            footerTemplate: ''
        };

        /**
         * Opens the movement thumbnail overlay.
         */
        this.openThumbnailSelector = function() {
            this.openOverlay(
                'Choose Thumbnail Cover',
                'overlay/thumbnail-selector/index.html',
                'overlay/thumbnail-selector/footer.html'
            );
        };

        /**
         * Opens the movement editor overlay.
         */
        this.openMovementEditor = function() {
            this.openOverlay(
                'Movement Editor',
                'overlay/movement-editor/index.html',
                'overlay/movement-editor/footer.html'
            );
        };

        /**
         * Opens the overlay screen (modal).
         *
         * @param string title
         * @param string bodyTemplate
         * @param stirng footerTemplate
         */
        this.openOverlay = function(title, bodyTemplate, footerTemplate) {

            // Update overlay data.
            Utilities.data.overlay = {
                title: title,
                bodyTemplate: bodyTemplate,
                footerTemplate: footerTemplate
            };

            // Open overlay.
            $('#overlay-screen').modal();
        };

        //
        // Events.
        //

        // Stores all event callbacks.
        this._events = {
            onEndSession: []
        };

        /**
         * @param string name
         */
        this.fireEvent = function(name) {
            Utilities.info('Firing event: ' + name);
        };

        // Performs final tasks before logging out.
        this.onEndSession = function()
        {

        };
        // TODO: implement a hooks system, where each controller can add their methods.
        this.endSession = function()
        {
            // Clear the sessionStorage.
            $sessionStorage[this.userHash] = {};

            Utilities.debug('Ending session...');

            $window.location.href = '/logout';

        }.bind(this);

        /**
         * @deprecated
         */
        this.hasState = function(namespace, id) {
            Utilities.debug('Rover.hasState is deprecated...');

            return namespace == 'profile' ?
                    Utilities.hasDataKey(namespace, id) :
                    Utilities.hasStateKey(namespace, id);
        };
        this.getState = function(namespace, id, def) {
            Utilities.debug('Rover.getState is deprecated...');

            return namespace == 'profile' ?
                    Utilities.getData(namespace, id, def) :
                    Utilities.getState(namespace, id, def);
        };
        this.setState = function(namespace, id, value) {
            Utilities.debug('Rover.setState is deprecated...');

            return namespace == 'profile' ?
                    Utilities.setVar(namespace, id, value) :
                    Utilities.setState(namespace, id, value);
        };
        this.debug = function(msg) {
            Utilities.debug('Rover.debug is deprecated...');
            Utilities.debug(msg);
        };
        this.error = function(msg) {
            Utilities.debug('Rover.error is deprecated...');
            Utilities.error(msg);
        };
        this.alert = function(msg) {
            Utilities.debug('Rover.alert is deprecated...');
            Utilities.alert(msg);
        };
        this.getConfig = function(key, defaultValue) {
            Utilities.debug('Rover.getConfig is deprecated...');
            return Utilities.getConfig(key, defaultValue);
        };
        this.setConfig = function(key, value) {
            Utilities.debug('Rover.setConfig is deprecated...');
            return Utilities.setConfig(key, value);
        };
        this.getId = function(obj) {
            Utilities.debug('Rover.getId is deprecated...');
            return Utilities.getId(obj);
        };
    }
]);
