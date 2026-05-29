-- public.candidate_profiles definition

-- Drop table

-- DROP TABLE public.candidate_profiles;

CREATE TABLE public.candidate_profiles (
	id serial4 NOT NULL,
	userid int4 NOT NULL,
	headline varchar(200) NULL,
	bio text NULL,
	currentsalary numeric(18, 2) NULL,
	expectedsalary numeric(18, 2) NULL,
	resumeurl text NULL,
	createdat timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	addressline1 varchar(100) NULL,
	addressline2 varchar(100) NULL,
	city varchar(64) NULL,
	state varchar(64) NULL,
	country varchar(64) NOT NULL,
	firstname varchar(64) NULL,
	lastname varchar(64) NULL,
	CONSTRAINT candidate_profiles_pkey PRIMARY KEY (id),
	CONSTRAINT fk_candidate_profile_user FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE
);