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
