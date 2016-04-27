/**
 * @file    backend.js
 * @brief   This file defines the Factories for interfacing with the back-end (CRUD)
 * @author  Maxwell Mowbray (max@heddoko.com); Francis Amankrah (frank@heddoko.com)
 * @date    June 2015
 */

angular.module('backendHeddoko', [])

/**
 * Teams factory.
 */
// .factory('Teams', ['$http', function($http) {
//
// 	return {
//
// 		/**
// 		* @brief Teams.get method used for fetching the active user's teams
// 		* @param void
// 		* @return list of user's teams
// 		*/
//
// 		get : function() {
// 			return $http.get('/api/teams');
// 		},
//
// 		/**
// 		* @brief Teams.create method used for creating a new team under the active user
// 		* @param form data pertaining to a new team entry
// 		* @return upon a successful addition of a new team, the back-end returns an updated teams list
// 		*/
//
// 		create : function(new_team_form_data) {
// 			return $http({
// 				method: 'POST',
// 				url: '/api/teams',
// 				headers: { 'Content-Type' : 'application/x-www-form-urlencoded' },
// 				data: $.param(new_team_form_data)
// 			});
// 		},
//
// 		/**
// 		* @brief This is the Teams.update method used for updating an existing team's details
// 		* @param form data pertaining to updated team details
// 		* @return upon a successful update of a team, the back-end returns a updated teams list
// 		*/
//
// 		update : function(team_id, updated_team_form_data) {
// 			return $http({
// 				method: 'PUT',
// 				url: '/api/teams/' + team_id,
// 				headers: { 'Content-Type' : 'application/x-www-form-urlencoded' },
// 				data: $.param(updated_team_form_data)
// 			});
// 		},
//
// 		/**
// 		* @brief This is the Teams.destroy method used for removing a team under the active user
// 		* @param id of the team to be destroyed
// 		* @return void
// 		*/
//
// 		destroy : function(team_id) {
// 			return $http.delete('/api/teams/' + team_id);
// 		},
//
//         /**
//          * Uploads an avatar.
//          *
//          * @param object group
//          * @param object fileData
//          * @return $http
//          */
//         avatar: function(groupId, fileData) {
//             return $http.post('/api/teams/'+ groupId +'/photo', {photo: fileData});
//         }
// 	};
// }])

/**
 * Athletes factory.
 */
// .factory('Athletes', ['$http', function($http) {
//
// 	return {
//
// 		/**
// 		* @brief Athletes.get method used for fetching the athletes belonging to a given team
// 		* @param team id
// 		* @return list of athletes belonging to supplied team
// 		*/
//
// 		get : function(team_id) {
// 			return $http.get('/api/teams/' + team_id + '/athletes');
// 		},
//
// 		/**
// 		* @brief Athletes.create method used for creating a new Athlete, belonging to an existing team
// 		* @param the id of the team under which to add the new athlete, and the new athlete's details
// 		* @return upon a successful addition of a new team, the back-end returns an updated athlete's list
// 		*/
//
// 		create : function(team_id, new_athlete_form_data) {
// 		    console.log(new_athlete_form_data);
// 			    return $http({
// 				    method: 'POST',
// 				    url: '/api/teams/' + team_id + '/athletes',
// 				    headers: { 'Content-Type' : 'application/x-www-form-urlencoded' },
// 				    data: $.param(new_athlete_form_data)
// 			});
// 		},
//
// 		update : function(profile) {
// 			return $http({
// 				method: 'PUT',
// 				url: '/api/teams/'+ profile.group_id +'/athletes/'+ profile.id,
// 				headers: { 'Content-Type' : 'application/x-www-form-urlencoded' },
// 				data: $.param(profile)
// 			});
// 		},
//
// 		destroy : function(team_id, id) {
// 			return $http.delete('/api/teams/' + team_id + '/athletes/' + id);
// 		},
//
//         /**
//          * Uploads an avatar.
//          *
//          * @param object group
//          * @param object fileData
//          * @return $http
//          */
//         avatar: function(groupId, profileId, fileData) {
//             return $http.post('/api/teams/'+ groupId +'/athletes/'+ profileId +'/photo', {photo: fileData});
//         }
// 	};
// }])

.factory('FMSForm', ['$http', function($http) {

	return {

		/**
		* @brief FMSForm.get method used for fetching the fmsforms belonging to a supplied athlete
		* @param id of athlete
		* @return list of FMS Forms belonging to supplied athlete
		*/

		get : function(athlete_id) {
			return $http.get('/api/athletes/' + athlete_id + '/fmsforms');
		},

		/**
		* @brief FMSForm.create method used for creating a new FMS Form, belonging to an existing athlete
		* @param the id of the athlete under which to add the new FMS Form, and the new FMS Form details
		* @return upon a successful addition of a new team, the back-end returns an updated FMS Form list
		*/

		create : function(athlete_id, form_data, fms_form_movement_files) {

			var fd = new FormData();

			if (fms_form_movement_files){
				if (fms_form_movement_files.deepsquat_movement_file) fd.append('deepsquat_movement_file', fms_form_movement_files.deepsquat_movement_file[0]);
				if (fms_form_movement_files.Lhurdle_movement_file) fd.append('Lhurdle_movement_file', fms_form_movement_files.Lhurdle_movement_file[0]);
				if (fms_form_movement_files.Rhurdle_movement_file) fd.append('Rhurdle_movement_file', fms_form_movement_files.Rhurdle_movement_file[0]);
				if (fms_form_movement_files.Llunge_movement_file) fd.append('Llunge_movement_file', fms_form_movement_files.Llunge_movement_file[0]);
				if (fms_form_movement_files.Rlunge_movement_file) fd.append('Rlunge_movement_file', fms_form_movement_files.Rlunge_movement_file[0]);
				if (fms_form_movement_files.Lshoulder_movement_file) fd.append('Lshoulder_movement_file', fms_form_movement_files.Lshoulder_movement_file[0]);
				if (fms_form_movement_files.Rshoulder_movement_file) fd.append('Rshoulder_movement_file', fms_form_movement_files.Rshoulder_movement_file[0]);
				if (fms_form_movement_files.Limpingement_movement_file) fd.append('Limpingement_movement_file', fms_form_movement_files.Limpingement_movement_file[0]);
				if (fms_form_movement_files.Rimpingement_movement_file) fd.append('Rimpingement_movement_file', fms_form_movement_files.Rimpingement_movement_file[0]);
				if (fms_form_movement_files.Lactive_movement_file) fd.append('Lactive_movement_file', fms_form_movement_files.Lactive_movement_file[0]);
				if (fms_form_movement_files.Ractive_movement_file) fd.append('Ractive_movement_file', fms_form_movement_files.Ractive_movement_file[0]);
				if (fms_form_movement_files.trunk_movement_file) fd.append('trunk_movement_file', fms_form_movement_files.trunk_movement_file[0]);
				if (fms_form_movement_files.press_movement_file) fd.append('press_movement_file', fms_form_movement_files.press_movement_file[0]);
				if (fms_form_movement_files.Lrotary_movement_file) fd.append('Lrotary_movement_file', fms_form_movement_files.Lrotary_movement_file[0]);
				if (fms_form_movement_files.Rrotary_movement_file) fd.append('Rrotary_movement_file', fms_form_movement_files.Rrotary_movement_file[0]);
				if (fms_form_movement_files.posterior_movement_file) fd.append('posterior_movement_file', fms_form_movement_files.posterior_movement_file[0]);
			}

			//attach the numerical values of the fms form

			fd.append('deepsquat', form_data.deepsquat);
			fd.append('deepsquatcomments', form_data.deepsquatcomments || '');
			fd.append('Lhurdle', form_data.Lhurdle);
			fd.append('Rhurdle', form_data.Rhurdle);
			fd.append('hurdlecomments', form_data.hurdlecomments || '');
			fd.append('Llunge', form_data.Llunge);
			fd.append('Rlunge', form_data.Rlunge);
			fd.append('lungecomments', form_data.lungecomments || '');
			fd.append('Lshoulder', form_data.Lshoulder);
			fd.append('Rshoulder', form_data.Rshoulder);
			fd.append('shouldercomments', form_data.shouldercomments || '');
			fd.append('Limpingement', form_data.Limpingement);
			fd.append('Rimpingement', form_data.Rimpingement);
			fd.append('impingementcomments', form_data.impingementcomments || '');
			fd.append('Lactive', form_data.Lactive);
			fd.append('Ractive', form_data.Ractive);
			fd.append('activecomments', form_data.activecomments || '');
			fd.append('trunk', form_data.trunk);
			fd.append('trunkcomments', form_data.trunkcomments || '');
			fd.append('press', form_data.press);
			fd.append('presscomments', form_data.presscomments || '');
			fd.append('Lrotary', form_data.Lrotary);
			fd.append('Rrotary', form_data.Rrotary);
			fd.append('rotarycomments', form_data.rotarycomments || '');
			fd.append('posterior', form_data.posterior);
			fd.append('posteriorcomments', form_data.posteriorcomments || '');

			fd.append('comment', form_data.comment);

			fd.append('totalscore', form_data.totalscore);

			return $http.post('/api/athletes/' + athlete_id + '/fmsforms', fd, {
				transformRequest: angular.identity,
				headers: {'Content-Type': undefined}
			});

		},

		/**
		* @brief This is the FMSForm.update method used for updating an existing FMS Form
		* @param the id of the athlete that the FMS Form belongs to, and the form data pertaining to updated team details
		* @return upon a successful update of a team, the back-end returns a updated FMS Form list
		*/

		update : function(athlete_id, updated_fms_form) {
			return $http({
				method: 'PUT',
				url: '/api/athletes/' + athlete_id + '/fmsforms/' + updated_fms_form.id,
				headers: { 'Content-Type' : 'application/x-www-form-urlencoded' },
				data: $.param(updated_fms_form)
			});
		},

		/**
		* @brief This is the FMSForm.destroy method used for deleting an FMS Form
		* @param id of the FMS Form to be destroyed
		* @return upon a successful deletion of an FMS Form, the back-end returns a updated FMS Form list
		*/

		destroy : function(athleteid, form_id) {
			return $http.delete('/api/athletes/' + athleteid + '/fmsforms/' + form_id);
		}
	};

}])

// .factory('Sports', ['$http', function($http) {
//
// 	return {
//
// 		/**
// 		* @brief SportCategories.get method used for fetching the sports categories
// 		* @param void
// 		* @return list of sports categories
// 		*/
//
// 		get : function() {
// 			return $http.get('/api/sports');
// 		}
// 	};
//
// }])

// .factory('SportMovements', ['$http', function($http) {
//
// 	return {
//
// 		/**
// 		* @brief SportMovements.get method used for fetching the movements under a given sport category
// 		* @param id of sport category
// 		* @return list of sport movements belonging to supplied sport category
// 		*/
//
// 		get : function(sport_id) {
// 			return $http.get('/api/sports/' + sport_id + '/sportmovements');
// 		}
//
// 	};
// }])

// .factory('Movements', ['$http', function($http) {
//
// 	return {
//
// 		/**
// 		* @brief Movements.upload method used for uploading multiple movement files to the back-end
// 		* @param id of athlete who conducted the movement(s), and the array of files to upload.
// 		* @return null
// 		*/
//
// 		upload : function(athlete_id, sport_id, form_data) {
//
// 			var fd = new FormData();
//
// 			fd.append('sportID', sport_id);
// 			fd.append('comment', form_data.comment);
//
// 			for (i = 0; i < form_data.movement_files.length; i++) {
// 				fd.append('movements[]', form_data.movement_files[i]);
// 			}
//
// 			return $http.post('/api/athletes/' + athlete_id + '/movements', fd, {
// 				transformRequest: angular.identity,
// 				headers: {'Content-Type': undefined}
// 			});
// 		}
// 	};
// }])

;
