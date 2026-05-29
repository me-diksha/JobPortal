-- public.candidate_experience definition

-- Drop table

-- DROP TABLE public.candidate_experience;

CREATE TABLE public.candidate_experience (
	id serial4 NOT NULL,
	userid int4 NOT NULL,
	companyname varchar(500) NOT NULL,
	designation varchar(200) NOT NULL,
	startdate date NOT NULL,
	enddate date NULL,
	currentlyworking bool DEFAULT false NOT NULL,
	description text NULL,
	createdat timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT candidate_experience_pkey PRIMARY KEY (id),
	CONSTRAINT fk_candidate_profile_user FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE
);