-- DROP FUNCTION public.get_all_jobs();

CREATE OR REPLACE FUNCTION public.get_all_jobs()
 RETURNS TABLE(id bigint, title character varying, description text, location character varying,employmenttype character varying, experiencelevel character varying, minsalary numeric, maxsalary numeric, deadline date, status character varying, companyname character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN

    RETURN QUERY
    SELECT
        j.id,
        j.title,
        j.description,
        j.location,
		et.description,
        et.description,
        j.minsalary,
        j.maxsalary,
        j.deadline,
        js.description,
        c.name AS companyname
    FROM jobs j
	INNER JOIN employment_type et ON et.id= j.refemploymenttype 
    INNER JOIN experiencelevel e ON e.id= j.refexperiencelevel
	INNER JOIN jobstatus js ON js.id = j.refstatus
    INNER JOIN company c
        ON c.id = j.companyid
    WHERE j.isdeleted = 0;

END;
$function$
;
