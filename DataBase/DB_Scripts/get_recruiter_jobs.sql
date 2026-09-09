-- DROP FUNCTION public.get_recruiter_jobs(int8);

CREATE OR REPLACE FUNCTION public.get_recruiter_jobs(p_companyid bigint)
 RETURNS TABLE(id bigint, title character varying, description character varying, location character varying, employmenttype character varying, experiencelevel character varying, minsalary numeric, maxsalary numeric, status character varying, deadline date, companyname character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT
        j.id,
        j.title,
        j.description,
        j.location,
        e.description,
        el.description,
        j.minsalary,
        j.maxsalary,
        js.description,
        j.deadline,
        c.name
    FROM jobs j
    LEFT JOIN company c
        ON c.id = j.companyid
    LEFT JOIN experiencelevel el
        ON el.id = j.refexperiencelevel
    LEFT JOIN employment_type e
        ON e.id = j.refemploymenttype
    LEFT JOIN jobstatus js
        ON js.id = j.refstatus
    WHERE j.companyid = p_companyid and j.isdeleted = 0;
END;
$function$
;
