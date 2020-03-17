module.exports = function(grunt){
  grunt.initConfig({
    copy: {
      main: {
        files: [
          {
            expand: true,
            flatten: true,
            cwd: 'node_modules/',
            src: [
            ],
            dest: 'AQSOwnerCheckIn/public/vendor/'
          },        
        ]
      }
    },
    uglify: {
      options: {
        beautify: false,
        mangle: true,
        quoteStyle: 3
      },
      dist: {
        files: {
          'AQSOwnerCheckIn/public/build/app.js': [
            
          ]
        }
      }
    },
    cssmin: {
      options: {
        mergeIntoShorthands: false,
        roundingPrecision: -1
      },
      target: {
        files: {
          'AQSOwnerCheckIn/public/build/app.css': [

          ]
        }
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-cssmin');

  grunt.registerTask('build', ['copy', 'uglify', 'cssmin'])
}