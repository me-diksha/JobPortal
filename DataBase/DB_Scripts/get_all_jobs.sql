CREATE OR REPLACE FUNCTION get_all_jobs()
RETURNS TABLE
(
    id BIGINT,
    title VARCHAR,
    description character varying,
    location VARCHAR,
    refemploymenttype bigint,
    employmenttype VARCHAR,
    experiencelevel VARCHAR,
    minsalary NUMERIC,
    maxsalary NUMERIC,
    deadline DATE,
    status VARCHAR,
    companyname VARCHAR
)
AS $$
BEGIN

    RETURN QUERY
    SELECT
        j.id,
        j.title,
        j.description,
        j.location,
        j.refemploymenttype,
		et.description,
        j.experiencelevel,
        j.minsalary,
        j.maxsalary,
        j.deadline,
        j.status,
        c.name AS companyname
    FROM jobs j
	INNER JOIN employment_type ON et.id= j.refemploymenttype 
    INNER JOIN company c
        ON c.id = j.companyid
    WHERE j.isdeleted = 0;

END;
$$ LANGUAGE plpgsql;