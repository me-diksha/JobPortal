-- DROP FUNCTION public.get_candidate_education(int4);

CREATE OR REPLACE FUNCTION public.get_candidate_education(p_userid integer)
 RETURNS TABLE(id integer, userid integer, institutename character varying, degree text, fieldofstudy character varying, startyear integer, endyear integer, percentage numeric)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT
        ce.id,
        ce.userid,
        ce.institutename,
        ce.degree,
        ce.fieldofstudy,
        ce.startyear,
        ce.endyear,
        ce.percentage
       
    FROM candidate_education ce
    WHERE ce.userid = p_userid;
END;
$function$
;
