CREATE TABLE jobs (
    id BIGSERIAL PRIMARY KEY,
    companyid BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    description VARCHAR(655) NOT NULL,
    location VARCHAR(200),
    refemploymenttype BIGINT,
    experiencelevel VARCHAR(100),
    minsalary NUMERIC,
    maxsalary NUMERIC,
    status VARCHAR(50) DEFAULT 'Open',
    deadline DATE,
    isdeleted smallint default 0,
    createdby BIGINT NOT NULL,
    createdat TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedat TIMESTAMP,
    CONSTRAINT fk_employmenttype FOREIGN KEY (refemploymenttype) REFERENCES public.employment_type(id) ON DELETE CASCADE
);


--after update 
CREATE TABLE jobs (
	id bigserial NOT NULL,
	companyid int8 NOT NULL,
	title varchar(200) NOT NULL,
	description varchar(655) NOT NULL,
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
	CONSTRAINT jobs_pkey PRIMARY KEY (id),
	CONSTRAINT fk_employmenttype FOREIGN KEY (refemploymenttype) REFERENCES public.employment_type(id) ON DELETE CASCADE,
	CONSTRAINT fk_jobs_experiencelevel FOREIGN KEY (refexperiencelevel) REFERENCES public.experiencelevel(id),
	CONSTRAINT fk_jobs_status FOREIGN KEY (refstatus) REFERENCES public.jobstatus(id)
);