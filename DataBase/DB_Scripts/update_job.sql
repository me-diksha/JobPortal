CREATE OR REPLACE FUNCTION update_job(
    p_jobid BIGINT,
    p_companyid BIGINT,
    p_title TEXT,
    p_description TEXT,
    p_location TEXT,
    p_refemploymenttype BIGINT,
    p_refexperiencelevel BIGINT,
    p_minsalary NUMERIC,
    p_maxsalary NUMERIC,
    p_deadline date,
    p_updatedby INTEGER
)
RETURNS TABLE
(
    id BIGINT,
    title CHARACTER VARYING,
    description TEXT,
    location CHARACTER VARYING,
    employmenttype CHARACTER VARYING,
    experiencelevel CHARACTER VARYING,
    minsalary NUMERIC,
    maxsalary NUMERIC,
    status CHARACTER VARYING,
    deadline DATE,
    companyname CHARACTER VARYING
)
LANGUAGE plpgsql
AS $$
BEGIN

    RETURN QUERY
    UPDATE jobs j
    SET
        title = p_title,
        description = p_description,
        location = p_location,
        refemploymenttype = p_refemploymenttype,
        refexperiencelevel = p_refexperiencelevel,
        minsalary = p_minsalary,
        maxsalary = p_maxsalary,
        deadline = p_deadline,
        updatedby = p_updatedby
    WHERE j.id = p_jobid
      AND j.companyid = p_companyid
      AND j.isdeleted = 0
    RETURNING
        j.id,
        j.title,
        j.description,
        j.location,
        (
            SELECT et.description
            FROM employment_type et
            WHERE et.id = j.refemploymenttype
        ) as employmenttype ,
        (
            SELECT el.description
            FROM experiencelevel el
            WHERE el.id = j.refexperiencelevel
        ) as experiencelevel,
        j.minsalary,
        j.maxsalary,
        (
            SELECT js.description
            FROM jobstatus js
            WHERE js.id = j.refstatus
        ) as status,
        j.deadline,
        (
            SELECT c.name
            FROM company c
            WHERE c.id = j.companyid
        ) as companyname;

END;
$$;