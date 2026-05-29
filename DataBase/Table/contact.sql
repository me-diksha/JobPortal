-- public.contact definition

-- Drop table

-- DROP TABLE public.contact;

CREATE TABLE public.contact (
	id serial4 NOT NULL,
	userid int4 NOT NULL,
	mobileno varchar(20) NULL,
	email text NULL,
	linkdelnurl text NULL,
	createdat timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT contact_pkey PRIMARY KEY (id),
	CONSTRAINT fk_contact_user FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE
);