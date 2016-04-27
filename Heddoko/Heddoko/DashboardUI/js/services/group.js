/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   This service handles group-related HTTP requests.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.services')

.factory('GroupService', ['$http', 'apiEndpoint', 'Utilities',
    function($http, apiEndpoint, Utilities) {

        return {

            /**
             * Base endpoint.
             */
            endpoint: apiEndpoint + '/groups',

            /**
             * Gets a list of groups.
             *
             * @param array|string embed
             * @return object $http
             */
            list: function(embed) {

                // Build request parameters.
                var config = {
                    params: {
                        embed: Utilities.getEmbedParameter(embed)
                    }
                };

                return $http.get(this.endpoint, config);
            },

            /**
             * Gets the details for a specific group.
             *
             * @param int id
             * @param array|string embed
             * @return object $http
             */
            get: function(id, embed)
            {
    			return $http.get(this.endpoint + '/' + id, {
                    params: {
                        embed: Utilities.getEmbedParameter(embed)
                    }
                });
    		},

            /**
             *
             */
            create: function(data) {
                return $http.post(this.endpoint, data);
    		},

            /**
             *
             */
            update: function(id, data) {
                return $http.put(this.endpoint + '/' + id, data);
    		},

            /**
             *
             */
            destroy: function(id) {
    			return $http.delete(this.endpoint + '/' + id);
    		},

            /**
             * Updates the avatar.
             *
             * @param int id
             * @param object fileData
             * @return $http
             */
            setAvatar: function(id, fileData) {
                return $http.post(this.endpoint + '/' + id + '/avatar', {image: fileData});
            }
        };
    }
]);
