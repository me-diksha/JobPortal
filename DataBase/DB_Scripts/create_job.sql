CREATE OR REPLACE FUNCTION create_job
(
    p_companyid BIGINT,
    p_title VARCHAR,
    p_description TEXT,
    p_location VARCHAR,
    p_refemploymenttype VARCHAR,
    p_experiencelevel VARCHAR,
    p_minsalary NUMERIC,
    p_maxsalary NUMERIC,
    p_deadline DATE,
    p_createdby BIGINT
)
RETURNS BIGINT
LANGUAGE plpgsql
AS $$
DECLARE
    v_id BIGINT;
BEGIN

    INSERT INTO jobs
    (
        companyid,
        title,
        description,
        location,
        refemploymenttype,
        experiencelevel,
        minsalary,
        maxsalary,
        deadline,
        createdby
    )
    VALUES
    (
        p_companyid,
        p_title,
        p_description,
        p_location,
        p_employmenttype,
        p_experiencelevel,
        p_minsalary,
        p_maxsalary,
        p_deadline,
        p_createdby
    )
    RETURNING id INTO v_id;

    RETURN v_id;
END;
$$;