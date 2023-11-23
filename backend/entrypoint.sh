#!/bin/bash

dotnet backend.dll &

sleep 10

sqlite3 movies.db < init.sql

tail -f /dev/null
