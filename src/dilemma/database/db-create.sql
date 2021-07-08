CREATE TABLE IF NOT EXISTS topic
(
	id   uuid NOT NULL
		CONSTRAINT topic_pkey
			PRIMARY KEY,
	name text
);


CREATE TABLE IF NOT EXISTS poster
(
	id  uuid      NOT NULL
		CONSTRAINT poster_pkey
			PRIMARY KEY,
	dob timestamp NOT NULL
);

CREATE TABLE IF NOT EXISTS dilemma
(
	id             uuid         NOT NULL
		CONSTRAINT dilemma_pkey
			PRIMARY KEY,
	topic_id       uuid         NOT NULL
		CONSTRAINT fk_topic
			REFERENCES topic,
	question       varchar(140) NOT NULL,
	posted_date    timestamp    NOT NULL,
	withdrawn_date timestamp,
	poster_id      uuid
		CONSTRAINT fk_poster
			REFERENCES poster
);

CREATE TABLE IF NOT EXISTS option
(
	id          uuid NOT NULL
		CONSTRAINT option_pkey
			PRIMARY KEY,
	description varchar(40),
	dilemma_id  uuid NOT NULL
		CONSTRAINT fk_dilemma
			REFERENCES dilemma
);
