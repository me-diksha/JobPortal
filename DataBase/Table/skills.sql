-- public.skills definition

-- Drop table

-- DROP TABLE public.skills;

CREATE TABLE public.skills (
	id serial4 NOT NULL,
	"name" varchar(100) NOT NULL,
	CONSTRAINT skills_name_key UNIQUE (name),
	CONSTRAINT skills_pkey PRIMARY KEY (id)
);