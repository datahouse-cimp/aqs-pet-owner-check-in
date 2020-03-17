AQ APP - CHANGELOG
==================


v1.0.5 - Current
----------------

2018-Jan-16

* Updated "Pets at AAQHF" and "Scheduled to Arrive" lists to list 100 records by default.
* Added extra pagination bar to the top of the "Pets at AAQHF" and "Scheduled to Arrive" lists
assist users. 
* Updated all lists to have horizontal drag scrolling capabilities (able to scroll horizontally
using the mouse to click and drag).
* Removed HTML5 form validation from login page (so that Angular validation could display).
* Cosmetic fixes for responsive design on Entry Application dialog.
* Updated package.json file to use fixed version numbers.
* Updated label in "Search Entry Applications" and "Pets in Transit/Transfer" lists for 
Changing the Transit/Transfer status of selected records.
* Moved GetTotalFeesOwed method from UseApplicationService to EntryApplication model and 
removed UseApplicationService from project.
* Added more logging around GetTotalFeesOwed for troubleshooting.

v1.0.4
------

2018-Jan-15

* Updated default configurations for logging in web.config

v1.0.3
------

2018-Jan-11

* Updated filters for Pets at AAQHF list.
* Added "Last Update Date" field to entry application schema and linked to disabled field on entry application dialog page.
* Fixed SQL query for Pet search so it can properly search for any microchip number in the comma separated ALLMCNO column.
* Added this changelog to the project.

v1.0.2
------------------

2017-Dec-22

* Removed live search feature.
* Added auto-focus to search field for list pages.
* Updated pet search query to work with new database search table.
* Made Initials field in Entry Application dialog required.
* Cosmetic fixes.


v1.0.1
-------------------

2017-Dec-15

* Update/Add packages for production deployment compatibility.
* Added logging to Global.asax.cs and WebApiConfig.cs