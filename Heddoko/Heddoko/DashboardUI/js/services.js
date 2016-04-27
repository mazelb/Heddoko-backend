
/**************************
 App ui Services

 loggit - Creates a logit type message for all logging

 **************************/

angular.module("app.ui.services", []).factory("loggit", [
    function() {
        var logIt;
        return toastr.options = {
            closeButton: !0,
            positionClass: "toast-bottom-left",
            timeOut: "3000"
        }, logIt = function(message, type) {
            return toastr[type](message);
        }, {
            log: function(message) {
                logIt(message, "info");
            },
            logWarning: function(message) {
                logIt(message, "warning");
            },
            logSuccess: function(message) {
                logIt(message, "success");
            },
            logError: function(message) {
                logIt(message, "error");
            }
        };
    }
]);

angular.module('app.controllers')

// MovementStore
.service('MovementStore', function() {

	var movement_screen_movements = [
		'Deep Squat',
		'Hurdle Step',
		'Inline Lunge',
		'Shoulder Mobility',
		'Impingement (C. Test)',
		'Active Straight Leg Raise',
		'Trunk Stability Pushup',
		'Press-up (C. Test)',
		'Rotary Stability',
		'Posterior Rocking (C. Test)'
	];

	var movement_screen_pages = {
		test: 0,
		analysis: 1,
		data: 2,
		summary: 3
	};

	var movement_pages = [];

	for (var i = 0; i < movement_screen_movements.length; i++)
	{
		var new_movement_page_entry = {};
		new_movement_page_entry.name = movement_screen_movements[i];
		new_movement_page_entry.url = new_movement_page_entry.name.toLowerCase().replace(' (c. test)', '').split(" ").join("_");
		new_movement_page_entry.latest_page = movement_screen_pages.test;
		new_movement_page_entry.score = Math.floor((Math.random() * 4) +1); //1 to 3 random val
		new_movement_page_entry.test_page_data = {};
		new_movement_page_entry.analysis_page_data = {};
		new_movement_page_entry.data_page_data = {};
		new_movement_page_entry.summary_page_data = {};

		//new_movement_page_entry.joints = [];

		if (i == 5) //ASLR
		{

			/*new_movement_page_entry.joints.push({name:'HIP', movements:[{name:'Flexion/Extension', left_deg:110, right_deg:98}]});
			new_movement_page_entry.joints.push({name:'KNEE', movements:[{name:'Flexion/Extension', left_deg:108, right_deg:97}]});

			new_movement_page_entry.summary_page_data.overall_score = 55;

			new_movement_page_entry.trials = [];
			new_movement_page_entry.trials.push({name:'trial ' + 1, status:'idle', active_graph_series:[]});
			new_movement_page_entry.trials.push({name:'trial ' + 2, status:'idle', active_graph_series:[]});
			new_movement_page_entry.trials.push({name:'trial ' + 3, status:'idle', active_graph_series:[]});

			new_movement_page_entry.active_trial = new_movement_page_entry.trials[0];

			new_movement_page_entry.sides = ['left', 'right'];
			new_movement_page_entry.sides[0].trials=[];
			new_movement_page_entry.sides[0].push({name:'trial ' + 1, status:'idle', active_graph_series:[]});
			new_movement_page_entry.sides[0].push({name:'trial ' + 2, status:'idle', active_graph_series:[]});
			new_movement_page_entry.sides[0].push({name:'trial ' + 3, status:'idle', active_graph_series:[]});*/

			var sides = ['LEFT', 'RIGHT'];
			var trials_count = 3;
			var joints = ['Hip', 'Knee'];

			new_movement_page_entry.sides = [];
			for (var l = 0; l < sides.length; l++)
			{
				new_movement_page_entry.sides.push({'name' : sides[l]});
			}

			new_movement_page_entry.active_side = new_movement_page_entry.sides[0];

			for (var j = 0; j < new_movement_page_entry.sides.length; j++)
			{
				new_movement_page_entry.sides[j].trials = [];

				for (var k = 0; k < trials_count; k++)
				{
					new_movement_page_entry.sides[j].trials.push({name:'Trial ' + (k + 1), status:'idle', joints:[{name:'HIP', movements:[{name:'Flexion/Extension', left_deg:108, right_deg:97, series_data: {name: 'Hip Flexion', data: [0, 15, 20, 60, 70, 90, 80, 75, 60, 45, 0]}} ]}, {name:'KNEE', movements:[{name:'Flexion/Extension', left_deg:108, right_deg:97, series_data: {name: 'Knee Flexion',data: [0, 5, 20, 50, 65, 80, 80, 80, 75, 60, 0]}} ]}]});
				}

				new_movement_page_entry.sides[j].active_trial = new_movement_page_entry.sides[j].trials[0];
			}

		}

        // Temp fix.
        else
        {
            new_movement_page_entry.sides = [
                {
                    name : 'TEST',
                    trials: [
                        {name: 'Trial 1', status: 'idle', joints: []},
                        {name: 'Trial 2', status: 'idle', joints: []}
                    ]
                }
            ];

            new_movement_page_entry.active_trial = new_movement_page_entry.sides[0].trials[0];

            new_movement_page_entry.disabled = true;
        }

		movement_pages.push(new_movement_page_entry);

	}

	return {movement_pages: movement_pages};

});
