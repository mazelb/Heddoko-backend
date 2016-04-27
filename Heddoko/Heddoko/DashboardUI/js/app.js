/**
 * Copyright Heddoko(TM) 2015, all rights reserved.
 *
 * @brief   Main angular application.
 * @author  Maxwell (max@heddoko.com) & Francis Amankrah (frank@heddoko.com)
 */

// Initializes the AngularJS application.
var app = angular.module('app', [

    // External dependencies.
    'ngAnimate', 'ngFileUpload', 'ngRoute', 'ngStorage', 'selectize', 'truncate',

    // General dependencies.
    'backendHeddoko',
    'app.controllers', 'app.directives', 'app.filters', 'app.rover', 'app.services', 'app.utilities',
    'app.views',

    // TODO: review what these dependencies do and whether they can be removed or not.
    "app.ui.services", "app.ui.form.ctrls", "app.ui.form.directives", "app.ui.ctrls",
    "ui.bootstrap",
    "easypiechart",
    // "mgo-angular-wizard",
    // "textAngular",
    // "app.form.validation", "app.tables",
    // "app.task",
    // "countTo",
    // "angular-chartist",
    // "app.map",
    // "ui.tree",
    // "ngMap",
    "app.chart.ctrls",
    "app.chart.directives"
]);

// Defines some constants.
var _appIsLocal =
    (
        window.location.hostname == 'localhost' ||
        window.location.hostname.match(/.*\.local$/i) ||
        window.location.hostname.match(/.*\.vagrant$/i)
    ) ? true : false;
var _apiEndpoint = '/api/v1';

app.constant('isLocalEnvironment', _appIsLocal).constant('apiEndpoint', _apiEndpoint);

// Initializes the 'app.services' module so we can add factories & services from separate files.
var appServices = angular.module('app.services', ['app.utilities']);

// Initializes the 'app.directives' module so we can add directives from separate files.
var appDirectives = angular.module('app.directives', ['app.utilities']);

// Initializes the 'app.filters' module so we can add filters from separate files.
var appFilters = angular.module('app.filters', ['app.utilities']);

// Configures the application.
app.config(['$routeProvider', 'isLocalEnvironment',
    function($routeProvider, isLocalEnvironment) {

        if (isLocalEnvironment) {
            console.log('Configuring App...');
        }

        //
        // Landing page.
        //
        return $routeProvider.when('/',{
            redirectTo: '/dashboard'
        })
        .when('/dashboard', {
            templateUrl: 'dashboard.html',
            controller: 'DashboardController'
		})

        //
        // Group routes.
        //
        .when('/group', {
            templateUrl: 'group/list.html',
            controller: 'GroupController'
		})
        .when('/groups/list', {
            redirectTo: '/group'
		})
        // .when('/groups/create', {
		// 	templateUrl: 'group/create.html',
        //     controller: 'GroupController'
		// })
        .when('/groups/edit', {
			templateUrl: 'group/edit.html',
            controller: 'GroupController'
		})
        .when('/groups/:groupId', {
            templateUrl: 'group/view.html',
            controller: 'GroupController'
		})

        //
        // Movement routes.
        //
        .when('/movements/analyze/:movementId', {
			templateUrl: 'movements/analysis/index.html',
            controller: 'AnalysisController'
		})
        .when('/movements/analyze/demo', {
			templateUrl: 'movements/analysis-demo/index.html',
            controller: 'DemoAnalysisController'
		})
        .when('/movements/compare', {
			templateUrl: 'movements/comparison/index.html',
            controller: 'TestController'
		})
        .when('/movements/upload', {
			templateUrl: 'import/index.html',
            controller: 'ImportController'
		})
        .when('/movements/:rootId?/:folderId?/:path?', {
			templateUrl: 'movements/explorer/index.html',
            controller: 'MovementController'
		})

        //
        // Profile routes.
        //
        .when('/profiles', {
            templateUrl: 'profile/list.html',
            controller: 'ProfileController'
		})
        .when('/profiles/list', {
            redirectTo: '/profile'
		})
        .when('/profiles/create', {
			templateUrl: 'profile/create.html',
            controller: 'ProfileController'
		})
        .when('/profiles/edit', {
			templateUrl: 'profile/edit.html',
            controller: 'ProfileController'
		})
        .when('/profiles/:profileId', {
			templateUrl: 'profile/view.html',
            controller: 'ProfileController'
		})

        //
        // Live capture routes.
        //
        .when('/capture', {
			templateUrl: 'capture/index.html',
            controller: 'TestController'
		})
        .when('/capture/demo', {
			templateUrl: 'capture-demo/index.html',
            controller: 'DemoCaptureController'
		})

        //
        // Screening routes.
        //
        .when('/screenings', {
			templateUrl: 'screenings/index.html',
            controller: 'ScreeningController'
		})
        .when('/screenings/live', {
			templateUrl: 'screenings/live/index.html',
            controller: 'LiveScreeningController'
		})
        .when('/screenings/:screeningId/:screeningPage?', {
			templateUrl: 'screenings/view.html',
            controller: 'ScreeningController'
		})

        //
        // Other routes.
        //
        .when('/account', {
			templateUrl: 'account.html',
            controller: 'UserController'
		})
        .when('/config', {
			templateUrl: 'configuration.html',
            controller: 'TestController'
		})

        //
        // Demo routes.
        //
        .when('/fms/demo/:name?/:step?',
        {
            templateUrl: function(params)
            {
                var tmpl = 'index';
                if (params.step) {
                    tmpl = params.step;
                } else if (params.name) {
                    tmpl = 'test';
                }

                return 'fms-bak/demo/'+ tmpl +'.html';
            },
            controller: 'FMSDemoController'
        })
        .when('/fms/live/:name?/:step?',
        {
            templateUrl: function(params)
            {
                var tmpl = 'index';
                if (params.step) {
                    tmpl = params.step;
                } else if (params.name) {
                    tmpl = 'test';
                }

                return 'fms-bak/live/'+ tmpl +'.html';
            },
            controller: 'FMSController'
        })
        .when('/demo/trends', {
			templateUrl: 'demo/trends/index.html',
            controller: 'DemoTrendsController'
        })
        .when("/fmstest", {
			templateUrl: "fmstest.html"
		}).when("/fmsdata", {
			templateUrl: "fmsdata.html"
		}).when("/fmsresults", {
			templateUrl: "fmsresults.html"
		}).when("/movementscreen", {
			templateUrl: "movementscreen.html"
		}).otherwise({
			redirectTo: '/dashboard'
		});
    }
])

// Runs the application.
.run(['$rootScope', '$location', 'Rover', 'Utilities',
    function ($rootScope, $location, Rover, Utilities) {

        Utilities.info('Running app...');

        $(document).ready(function() {

            Utilities.info('DOM ready');

            // Removes the loading animation.
            Rover.doneBackgroundProcess();

            // Enables tooltips.
            $(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
        });
    }
]);


/**************************
 App Map
 **************************/

angular.module("app.map", []).directive("uiJqvmap", [
        function() {
            return {
                restrict: "A",
                scope: {
                    options: "="
                },
                link: function(scope, ele) {
                    var options;
                    return options = scope.options, ele.vectorMap(options);
                }
            };
        }
    ]).controller("jqvmapCtrl", ["$scope",
        function($scope) {
            var sample_data;
            return sample_data = {
                af: "16.63",
                al: "11.58",
                dz: "158.97",
                ao: "85.81",
                ag: "1.1",
                ar: "351.02",
                am: "8.83",
                au: "1219.72",
                at: "366.26",
                az: "52.17",
                bs: "7.54",
                bh: "21.73",
                bd: "105.4",
                bb: "3.96",
                by: "52.89",
                be: "461.33",
                bz: "1.43",
                bj: "6.49",
                bt: "1.4",
                bo: "19.18",
                ba: "16.2",
                bw: "12.5",
                br: "2023.53",
                bn: "11.96",
                bg: "44.84",
                bf: "8.67",
                bi: "1.47",
                kh: "11.36",
                cm: "21.88",
                ca: "1563.66",
                cv: "1.57",
                cf: "2.11",
                td: "7.59",
                cl: "199.18",
                cn: "5745.13",
                co: "283.11",
                km: "0.56",
                cd: "12.6",
                cg: "11.88",
                cr: "35.02",
                ci: "22.38",
                hr: "59.92",
                cy: "22.75",
                cz: "195.23",
                dk: "304.56",
                dj: "1.14",
                dm: "0.38",
                "do": "50.87",
                ec: "61.49",
                eg: "216.83",
                sv: "21.8",
                gq: "14.55",
                er: "2.25",
                ee: "19.22",
                et: "30.94",
                fj: "3.15",
                fi: "231.98",
                fr: "2555.44",
                ga: "12.56",
                gm: "1.04",
                ge: "11.23",
                de: "3305.9",
                gh: "18.06",
                gr: "305.01",
                gd: "0.65",
                gt: "40.77",
                gn: "4.34",
                gw: "0.83",
                gy: "2.2",
                ht: "6.5",
                hn: "15.34",
                hk: "226.49",
                hu: "132.28",
                is: "12.77",
                "in": "1430.02",
                id: "695.06",
                ir: "337.9",
                iq: "84.14",
                ie: "204.14",
                il: "201.25",
                it: "2036.69",
                jm: "13.74",
                jp: "5390.9",
                jo: "27.13",
                kz: "129.76",
                ke: "32.42",
                ki: "0.15",
                kr: "986.26",
                undefined: "5.73",
                kw: "117.32",
                kg: "4.44",
                la: "6.34",
                lv: "23.39",
                lb: "39.15",
                ls: "1.8",
                lr: "0.98",
                ly: "77.91",
                lt: "35.73",
                lu: "52.43",
                mk: "9.58",
                mg: "8.33",
                mw: "5.04",
                my: "218.95",
                mv: "1.43",
                ml: "9.08",
                mt: "7.8",
                mr: "3.49",
                mu: "9.43",
                mx: "1004.04",
                md: "5.36",
                mn: "5.81",
                me: "3.88",
                ma: "91.7",
                mz: "10.21",
                mm: "35.65",
                na: "11.45",
                np: "15.11",
                nl: "770.31",
                nz: "138",
                ni: "6.38",
                ne: "5.6",
                ng: "206.66",
                no: "413.51",
                om: "53.78",
                pk: "174.79",
                pa: "27.2",
                pg: "8.81",
                py: "17.17",
                pe: "153.55",
                ph: "189.06",
                pl: "438.88",
                pt: "223.7",
                qa: "126.52",
                ro: "158.39",
                ru: "1476.91",
                rw: "5.69",
                ws: "0.55",
                st: "0.19",
                sa: "434.44",
                sn: "12.66",
                rs: "38.92",
                sc: "0.92",
                sl: "1.9",
                sg: "217.38",
                sk: "86.26",
                si: "46.44",
                sb: "0.67",
                za: "354.41",
                es: "1374.78",
                lk: "48.24",
                kn: "0.56",
                lc: "1",
                vc: "0.58",
                sd: "65.93",
                sr: "3.3",
                sz: "3.17",
                se: "444.59",
                ch: "522.44",
                sy: "59.63",
                tw: "426.98",
                tj: "5.58",
                tz: "22.43",
                th: "312.61",
                tl: "0.62",
                tg: "3.07",
                to: "0.3",
                tt: "21.2",
                tn: "43.86",
                tr: "729.05",
                tm: 0,
                ug: "17.12",
                ua: "136.56",
                ae: "239.65",
                gb: "2258.57",
                us: "14624.18",
                uy: "40.71",
                uz: "37.72",
                vu: "0.72",
                ve: "285.21",
                vn: "101.99",
                ye: "30.02",
                zm: "15.69",
                zw: "5.57"
            }, $scope.worldMap = {
                map: "world_en",
                backgroundColor: null,
                color: "#ffffff",
                hoverOpacity: 0.7,
                selectedColor: "#db5031",
                hoverColor: "#db5031",
                enableZoom: !0,
                showTooltip: !0,
                values: sample_data,
                scaleColors: ["#F1EFF0", "#c1bfc0"],
                normalizeFunction: "polynomial"
            }, $scope.USAMap = {
                map: "usa_en",
                backgroundColor: null,
                color: "#ffffff",
                selectedColor: "#db5031",
                hoverColor: "#db5031",
                enableZoom: !0,
                showTooltip: !0,
                selectedRegion: "MO"
            }, $scope.europeMap = {
                map: "europe_en",
                backgroundColor: null,
                color: "#ffffff",
                hoverOpacity: 0.7,
                selectedColor: "#db5031",
                hoverColor: "#db5031",
                enableZoom: !0,
                showTooltip: !0,
                values: sample_data,
                scaleColors: ["#F1EFF0", "#c1bfc0"],
                normalizeFunction: "polynomial"
            };
        }
    ]);

/**************************
 Timer
 **************************/
angular.module('countTo', []).controller("countTo", ["$scope",
        function($scope) {

            return $scope.countersmall1 = {
                countTo: 20,
                countFrom: 0
            },$scope.countersmall2 = {
                countTo: 42,
                countFrom: 0
            },$scope.countersmall3 = {
                countTo: 90,
                countFrom: 0
            },$scope.countersmall1dash = {
                countTo: 420,
                countFrom: 0
            },$scope.countersmall2dash = {
                countTo: 742,
                countFrom: 0
            },$scope.countersmall3dash = {
                countTo: 100,
                countFrom: 0
            };

        }]).directive('countTo', ['$timeout', function ($timeout) {
        return {
            replace: false,
            scope: true,
            link: function (scope, element, attrs) {

                var e = element[0];
                var num, refreshInterval, duration, steps, step, countTo, value, increment;

                var calculate = function () {
                    refreshInterval = 30;
                    step = 0;
                    scope.timoutId = null;
                    countTo = parseInt(attrs.countTo) || 0;
                    scope.value = parseInt(attrs.value, 10) || 0;
                    duration = (parseFloat(attrs.duration) * 1000) || 0;

                    steps = Math.ceil(duration / refreshInterval);
                    increment = ((countTo - scope.value) / steps);
                    num = scope.value;
                };

                var tick = function () {
                    scope.timoutId = $timeout(function () {
                        num += increment;
                        step++;
                        if (step >= steps) {
                            $timeout.cancel(scope.timoutId);
                            num = countTo;
                            e.textContent = countTo;
                        } else {
                            e.textContent = Math.round(num);
                            tick();
                        }
                    }, refreshInterval);

                };

                var start = function () {
                    if (scope.timoutId) {
                        $timeout.cancel(scope.timoutId);
                    }
                    calculate();
                    tick();
                };

                attrs.$observe('countTo', function (val) {
                    if (val) {
                        start();
                    }
                });

                attrs.$observe('value', function (val) {
                    start();
                });

                return true;
            }
        };

    }]);
