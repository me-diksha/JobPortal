-- public.users definition

-- Drop table

-- DROP TABLE public.users;

CREATE TABLE public.users (
	id serial4 NOT NULL,
	name varchar(100) NOT NULL,
	email varchar(255) NOT NULL,
	passwordhash text NOT NULL,
	roleid int4 NOT NULL,
	createdat timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT users_email_key UNIQUE (email),
	CONSTRAINT users_pkey PRIMARY KEY (id),
	CONSTRAINT fk_role FOREIGN KEY (roleid) REFERENCES public.roles(id)
);