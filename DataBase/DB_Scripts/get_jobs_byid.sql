-- DROP FUNCTION public.get_jobs_byid(int8);

CREATE OR REPLACE FUNCTION public.get_jobs_byid(p_jobid bigint)
 RETURNS TABLE(id bigint, title character varying, description text, location character varying, employmenttype character varying, experiencelevel character varying, minsalary numeric, maxsalary numeric, status character varying, deadline date, companyname character varying)
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
    WHERE j.id= p_jobid;
END;
$function$
;
