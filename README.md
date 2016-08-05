# _A Concert Tracker Program with C#, Nancy, Razor, and SQL Databases_

#### _Project Specifications_

#### By _**Sid Benavente**_

## Description/Specs

A program which allows management of salon Stylists and their associated Clients.
#### Data Model
The data model consists of two classes, Band and Venue which share a many:many relationships.

The bands data table contains the following fields:
* id
* name

The venues data table contains the following fields:
* id
* name

A join table - events - joins the bands and venues tables and contains the following fields:
* id
* band_id
* venue_id



#### Program Routing

| Method       | Route           | Action  | Model/Comments |
| ------------- |:-------------:| -----:| -----:|
| GET| /| Returns index.cshtml, Model| Model is a list of all Stylist instances |
| POST| /| Returns index.cshtml, Model| Model is a list of all Stylist instances, after saving a new stylist |
| GET|/stylists/{id}| Returns stylist.cshtml, Model | Model is a dictionary containing the selected Stylist instance, and all associated Client instances |
| GET| /stylist/edit/{id}| Returns stylist_edit.cshtml, Model| Model is a found Stylist instance to be updated |
| PATCH| /stylist/edit/{id}| Returns success.cshtml| Updates Stylist information, routes to simple confirmation page with link to home page |
| POST| /stylist/new| Returns success.cshtml | Creates and saves a new stylist, routes to simple confirmation page with link to home page |
| GET| /stylist/delete/{id}| Returns stylist_delete.cshtml, Model| Model is a found Stylist instance to be deleted, along with all associated clients |
| DELETE| /stylist/delete/{id}| Returns success.cshtml| Deletes a single Stylist instance, routes to simple confirmation page with link to home page |

#### User Scenarios
1. A program administrator can create a new database entry for a Venue. The admin can also edit and delete any and all venue records.
2. A program administrator can create a new database entry for a band.
3. A program administrator can view all venues, and select individual venues to view the details of which bands have played at that venue.
4. A program administrator can view all bands, and select individual bands to view at which venues that particular band has played.
5. A program administrator can create additional event records, tracking when a particular band has played at a particular venue.


#### Use this program
Clone this repository.

Part 1. Prepare the database.

The root folder of this project contains two database scripting files (band_tracker_test_script.sql for testing, band_tracker_test_script.sql for production). To import these databases, do the following:
* Open SSMS
* Select File > Open > File and select the appropriate .sql file.
* Click !Execute.
* The databases should appear in your database listing.

Part 2. Run server and access the web app.

Prepare your machine to run the Kestrel server by following the [instructions here.](https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c)
To start the local server, type in "DNX Kestrel" at a command prompt in the root directory of your project. Navigate in your browser to "LocalHost:5004" to view the homepage.

#### Known Bugs / Unimplemented Features
*TBD

#### Support and contact details
Please contact the authors if you have any questions or comments.

#### Technologies Used
This web application was created using C#, Nancy, Razor, SQL, CSS, and Bootstrap.

#### License
Copyright (c) 2016 _**Sid Benavente**_

This software is licensed under the MIT license.
