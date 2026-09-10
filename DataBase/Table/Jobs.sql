-- public.jobs definition

-- Drop table

-- DROP TABLE public.jobs;

CREATE TABLE public.jobs (
	id bigserial NOT NULL,
	companyid int8 NOT NULL,
	title varchar(200) NOT NULL,
	description text NOT NULL,
	"location" varchar(200) NULL,
	refemploymenttype int8 NULL,
	minsalary numeric NULL,
	maxsalary numeric NULL,
	deadline date NULL,
	isdeleted int2 DEFAULT 0 NULL,
	createdby int8 NOT NULL,
	createdat timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	updatedat timestamp NULL,
	refexperiencelevel int8 NULL,
	refstatus int8 NULL,
	updatedby int4 NULL,
	CONSTRAINT jobs_pkey PRIMARY KEY (id),
	CONSTRAINT fk_employmenttype FOREIGN KEY (refemploymenttype) REFERENCES public.employment_type(id) ON DELETE CASCADE,
	CONSTRAINT fk_jobs_experiencelevel FOREIGN KEY (refexperiencelevel) REFERENCES public.experiencelevel(id),
	CONSTRAINT fk_jobs_status FOREIGN KEY (refstatus) REFERENCES public.jobstatus(id),
	CONSTRAINT fk_jobs_updatedbyuser FOREIGN KEY (updatedby) REFERENCES public.users(id)
);