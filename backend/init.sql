CREATE TABLE "movies" (
	"id"	INTEGER,
	"title"	TEXT NOT NULL,
	"releaseYear"	INTEGER,
	"active"	INTEGER NOT NULL DEFAULT 1,
	"externalId"	INTEGER,
	PRIMARY KEY("id")
)