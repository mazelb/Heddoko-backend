/*jslint node: true */
"use strict";

/**
 * Copyright Heddoko(TM) 2015, all rights reserved
 */

module.exports = function(grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        // bower: {
        //     install: {
        //         options: {
        //             install: true,
        //             copy: false,
        //             targetDir: './libs',
        //             cleanTargetDir: true
        //         }
        //     }
        // },

        clean: {
            temp: {
                src: [ 'tmp' ]
            },
            dist: {
                src: ['fonts', 'Content/images']
            }
        },

        copy: {
            dist: {
                files: [
                    // Bootstrap fonts.
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/bootstrap/dist',
                        src: ['fonts/*.*'],
                        dest: 'Content'
                    },

                    // Font-Awesome.
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/fontawesome',
                        src: ['fonts/*.*'],
                        dest: 'Content'
                    },

                    // App fonts.
                    {
                        expand: true,
                        dot: true,
                        cwd: 'DashboardUI',
                        src: ['fonts/*.*'],
                        dest: 'Content'
                    },

                    // App images.
                    {
                        expand: true,
                        dot: true,
                        cwd: 'DashboardUI/images',
                        src: ['*.*','**/*.*'],
                        dest: 'Content/images'
                    }
                ]
            }
        },

        jshint: {
            dist: [
                'Gruntfile.js',
                'DashboardUI/js/*.js',
                'DashboardUI/js/**/*.js'
            ]
        },

        uglify: {
            options: {
                mangle: {
                    except: ['jQuery', '$']
                }
            },
            dist: {
                files: {
                    'DashboardUI/build/scripts.js': [
                        'DashboardUI/js/**/*.js',
                        'DashboardUI/js/*.js'
                    ]
                }
            }
        },

        html2js: {
            options: {
                base: 'DashboardUI/angular-views',
                module: 'app.views'
            },
            dist: {
                src: [
                    'DashboardUI/angular-views/*.html',
                    'DashboardUI/angular-views/**/*.html'
                ],
                dest: 'DashboardUI/build/views.js'
            }
        },

		sass: {
            dist: {
                files: {
                    'DashboardUI/build/styles.css': 'DashboardUI/sass/main.scss',
                    'DashboardUI/build/admin.css': 'DashboardUI/sass/admin.scss'
                }
            }
        },
        
        // assets_versioning: {
        //     options: {
        //         versionsMapFile: 'resources/assets/rev.json'
        //     },
        //     js: {
        //         files: {
        //             'public/js/scripts.js': ['public/js/scripts.js']
        //         }
        //     },
        //     css: {
        //         files: {
        //             'public/css/styles.css': ['public/css/styles.css']
        //         }
        //     }
        // },

        watch: {
            dist: {
                files: [
                    'Gruntfile.js',
                    'DashboardUI/angular-views/*.html',
                    'DashboardUI/angular-views/**/*.html',
                    'DashboardUI/images/*.*',
                    'DashboardUI/images/**/*.*',
                    'DashboardUI/js/*.js',
                    'DashboardUI/js/**/*.js',
                    'DashboardUI/sass/*.scss',
                    'DashboardUI/sass/**/*.scss'
                ],
                tasks: [
                    // 'clean:temp',
                    'jshint:dist',
                    'uglify:dist',
                    'html2js:dist',
                    'sass',
                    'clean:dist',
                    'copy:dist'
                ],
                options: {
                    atBegin: true
                }
            }
            // dev: {
            //     files: [
            //         'Gruntfile.js',
            //         'resources/assets/js/*.js',
            //         'resources/assets/js/**/*.js',
            //         'angular-app/styles/*.scss'
            //     ],
            //     // tasks: [ 'jshint', 'html2js:dist', 'copy:main', 'concat:dist', 'clean:temp', 'sass', 'cssmin' ],
            //     tasks: [
            //         'jshint',
            //         'concat:dist',
            //         'sass',
            //         'cssmin'
            //     ],
            //     options: {
            //         atBegin: true
            //     }
            // },
            // min: {
            //     files: [
            //         'Gruntfile.js',
            //         'resources/assets/js/*.js',
            //         'resources/assets/js/**/*.js',
            //         'angular-app/styles/*.scss'
            //     ],
            //     tasks: [
            //         'jshint',
            //         'html2js:dist',
            //         'copy:main',
            //         'concat:dist',
            //         'clean:temp',
            //         'uglify:dist',
            //         'cssmin'
            //     ],
            //     options: {
            //         atBegin: true
            //     }
            // }
        },

        compress: {
            dist: {
                options: {
                    archive: 'dist/<%= pkg.name %>-<%= pkg.version %>.zip'
                },
                files: [{
                    src: [ 'index.html' ],
                    dest: '/'
                }, {
                    src: [ 'app/**' ],
                    dest: 'app/'
                }, {
                    src: [ 'app/**' ],
                    dest: 'app/'
                }, {
                    src: [ 'app/**' ],
                    dest: 'app/'
                }]
            }
        }
    });

    // grunt.loadNpmTasks('grunt-assets-versioning');
    grunt.loadNpmTasks('grunt-bower-task');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-compress');
    grunt.loadNpmTasks('grunt-contrib-jshint');
	grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-html2js');

    // grunt.registerTask('dev', [ 'bower', 'watch:dev' ]);
    // grunt.registerTask('production', [ 'bower', 'watch:min' ]);

    grunt.registerTask('css', ['sass']);
    grunt.registerTask('js', ['jshint', 'uglify', 'html2js']);
    grunt.registerTask('default', ['js', 'css', 'clean:dist', 'copy:dist']);
};
