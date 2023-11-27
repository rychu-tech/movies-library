# Documentation
This repository contains simple movie management system. User is able to view movie list, add, edit, delete (soft delete) and restore movies. There is also external API integration from which user is able to import movies.

## Setting Up
To run this project you will need Docker installed. Clone this repository and navigate to main folder in which there is backend and frontend folder. Then run command:
```
docker-compose up --build
```
After running this command wait until all containers are running.

## Access
Backend is running on port 8081 and the documentation of used endpoints is here: http://localhost:8081/swagger/index.html
Frontend is running on port 8080 inside docker, however you can access the app: http://localhost:5173/
Database is a simple one-table SQLite file database.

## Everything else you need to know
* There is backend.Tests folder that is another project added to sln that contains simple tests for most frequently used API endpoints. You can run it from Visual Studio IDE (that's how I did it, not sure if you can do it from command line or from inside Docker container).
* init.sql contains database setup
* movies.db is a database file
* entrypoint.sh contains running sql script to database
* I chose SQLite because the size of the project is really small and it was the fastest way possible to dive deep into coding other stuff
* If setting up docker fails, I am sorry, but I had a lot of fun troubleshooting this :) especially putting Vue.js app on Docker is really funny :) Just run it locally via IDE.
