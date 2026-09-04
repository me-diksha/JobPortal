-- DROP FUNCTION public.create_job(int8, varchar, text, varchar, varchar, varchar, numeric, numeric, date, int8);

CREATE OR REPLACE FUNCTION public.create_job(p_companyid bigint, p_title character varying, p_description text, p_location character varying, p_refemploymenttype bigint, p_refexperiencelevel bigint, p_minsalary numeric, p_maxsalary numeric, p_deadline date, p_createdby bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
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
        refexperiencelevel,
		refstatus,
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
		1,
        p_minsalary,
        p_maxsalary,
        p_deadline,
        p_createdby
    )
    RETURNING id INTO v_id;

    RETURN v_id;
END;
$function$
;
