-- DROP FUNCTION public.get_candidate_experience(int4);

CREATE OR REPLACE FUNCTION public.get_candidate_experience(p_userid integer)
 RETURNS TABLE(id integer, userid integer, companyname character varying, designation character varying, startdate date, enddate date, currentlyworking boolean, description text)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT
        ce.id,
        ce.userid,
        ce.companyname,
        ce.designation,
        ce.startdate,
        ce.enddate,
        ce.currentlyworking,
        ce.description
    FROM candidate_experience ce
    WHERE ce.userid = p_userid;
END;
$function$
;
