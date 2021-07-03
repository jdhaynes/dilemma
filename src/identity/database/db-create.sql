CREATE TABLE "user"
(
	id                 uuid         NOT NULL,
	email              varchar(255) NOT NULL,
	dob                timestamp    NOT NULL,
	account_closed     timestamp,
	account_registered timestamp    NOT NULL,
	first_name         varchar(50)  NOT NULL,
	last_name          varchar(50)  NOT NULL,
	password           bytea        NOT NULL,
	salt               bytea        NOT NULL
);

CREATE UNIQUE INDEX user_email_uindex
	ON "user" (email);

CREATE UNIQUE INDEX user_id_uindex
	ON "user" (id);

ALTER TABLE "user"
	ADD CONSTRAINT user_pk
		PRIMARY KEY (id);

