CREATE TABLE public.roles (
	id serial4 NOT NULL,
	description varchar(50) NOT NULL,
	CONSTRAINT roles_description_key UNIQUE (description),
	CONSTRAINT roles_pkey PRIMARY KEY (id)
);