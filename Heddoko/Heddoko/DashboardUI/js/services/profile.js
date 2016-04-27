/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   This service handles profile-related HTTP requests.
 * @author  Francis Amankrah (frank@heddoko.com)
 * @date    November 2015
 */
angular.module('app.services')

.factory('ProfileService', ['$http', '$filter', 'Utilities', 'Rover', 'apiEndpoint',
    function($http, $filter, Utilities, Rover, apiEndpoint) {

        return {

            /**
             * Base endpoint.
             */
            endpoint: apiEndpoint + '/profiles',

            /**
             * Gets a list of profiles.
             *
             * @param int groupId
             * @param array|string embed
             * @return object $http
             */
            list: function(groupId, embed) {

                // Build request parameters.
                var config = {
                    params: {
                        embed: Utilities.getEmbedParameter(embed)
                    }
                };

                // Add group ID to request parameters.
                if (groupId) {
                    config.params.groupId = groupId;
                }

                return $http.get(this.endpoint, config);
            },

            /**
             * Gets the details for a specific profile.
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
             * Stores the details for a new profile.
             *
             * @param object data
             * @param array|string embed
             * @return object $http
             */
            create: function(data, embed)
            {
                return $http.post(this.endpoint, data, {
                    params: {
                        embed: Utilities.getEmbedParameter(embed)
                    }
                });
    		},

            /**
             * Updates the details for a given profile.
             *
             * @param int id
             * @param object data
             * @param array|string embed
             * @return object $http
             */
            update: function(id, data, embed)
            {
                return $http.put(this.endpoint + '/' + id, data, {
                    params: {
                        embed: Utilities.getEmbedParameter(embed)
                    }
                });
    		},

            /**
             * Removes the profile details from the database.
             *
             * @param int id
             * @return object $http
             */
            destroy: function(id) {
    			return $http.delete(this.endpoint + '/' + id);
    		},

            /**
             * Removes a profile avatar.
             *
             * @param int id
             * @return ...
             */
            destroyAvatar: function(id) {
    			return $http.delete(this.endpoint + '/' + id + '/avatar');
    		},

            /**
             * Updates the avatar.
             *
             * @param int id
             * @param object fileData
             * @return $http
             */
            setAvatar: function(id, fileData) {
                return $http.post(this.endpoint + '/' + id +'/avatar', {image: fileData});
            },

            /**
             * Formats profile details to be displayed in the UI.
             *
             * @param object profile
             * @return object
             */
            format: function(profile) {

                // Format "createdAt" date.
                profile.createdAt = profile.createdAt || '';
                profile.createdAt_formatted = profile.createdAt.length > 0 ?
                    $filter('date')(profile.createdAt.substr(0, 10), 'MMM d, yyyy') : '';

                // // Calculate the amount of feet in the total height.
                // profile.feet = profile.height > 0 ?
                //     Math.floor(profile.height / 0.3048) : '';
                //
                // // Calculate the amount of inches in the remaining height.
                // profile.inches = profile.height > 0 ?
                //     Math.round((profile.height / 0.3048 - profile.feet) * 12) : '';
                //
                // // Calculate the weight in pounds.
                // profile.weight_lbs = profile.mass > 0 ? Math.round(profile.mass / 0.453592) : '';

                // Make sure meta data array is not null.
                profile.meta.data = $.extend({}, profile.meta.data);

                // Append meta data to profile object.
                angular.forEach(profile.meta, function(data, key) {
                    profile[key] = data;
                });

                //

                return profile;
            },

            /**
             * Formats profile data before saving to database.
             *
             * @param object profile
             * @return object
             */
            formatForStorage: function(profile) {

                // Only copy relevant details.
                var formatted =
                {
                    id: profile.id,
                    firstName: profile.firstName || '',
                    lastName: profile.lastName || '',
                    mainTagId: profile.mainTagId || '',
                    meta: {
                        height: profile.height || 0.0,
                        mass: profile.mass || 0.0,
                        dob: profile.dob || '',
                        gender: profile.gender || '',
                        phone: profile.phone || '',
                        email: profile.email || '',
                        data: profile.data || {}
                    }
                };

                // Format height into meters.
                if (profile.feet > 0 && profile.inches) {
                    formatted.meta.height = (profile.feet + profile.inches / 12) * 0.3048;
                }

                // Format mass in kg.
                if (profile.weightInPounds > 0) {
                    formatted.meta.mass = profile.weightInPounds * 0.453592;
                }

                // Format groups into an array of IDs.
                if (profile.groups && profile.groups.length > 0) {
                    formatted.groups = profile.groups.map(Utilities.getId);
                }

                // Make sure primary tag is an ID. If we have a newly created tag without an ID,
                // we'll let the API know we want to create a new tag in the process.
                if (profile.primaryTag && profile.primaryTag.length)
                {
                    if (Utilities.getId(profile.primaryTag) > 0) {
                        formatted.tagId = Utilities.getId(profile.primaryTag);
                    }

                    else {
                        formatted.primaryTagTitle = profile.primaryTag;
                    }
                }

                // Format secondary tags into an array of IDs.
                if (profile.secondaryTags && profile.secondaryTags.length > 0)
                {
                    formatted.secondaryTags = [];
                    formatted.secondaryTagTitles = [];
                    angular.forEach(profile.secondaryTags, function(tag) {

                        // If tag exists, retrieve its ID.
                        if (Utilities.getId(tag) > 0) {
                            formatted.secondaryTags.push(Utilities.getId(tag));
                        }

                        // Else, let API know we want to create new tags.
                        else {
                            formatted.secondaryTagTitles.push(tag);
                        }
                    });
                    // formatted.secondary_tags = profile.secondary_tags.map(Utilities.getId);
                }

                return formatted;
            }
        };
    }
]);
