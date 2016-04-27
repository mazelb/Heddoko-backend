/**
 * @file    fms.js
 * @brief
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.services')

.factory('FMSService', ['$http',
    function($http) {

        return {

            /**
             *
             */
            get: function(profileId) {

                // Add profile ID to request parameters.
                var config = profileId ? {params: {profile_id: profileId}} : {};

                return $http.get('/api/fms', config);
            },

            /**
             *
             */
            create: function(data) {
                return $http.post('/api/fms', data);
            },

            /**
             *
             */
            update: function(id, data) {
                return $http.put('/api/fms/' + id, data);
            },

            /**
             *
             */
            destroy: function(id) {
                return $http.delete('/api/fms/' + id);
            },
        };
    }
]);
