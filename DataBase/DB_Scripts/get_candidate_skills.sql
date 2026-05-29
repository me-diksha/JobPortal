-- DROP FUNCTION public.get_candidate_skills(int4);

CREATE OR REPLACE FUNCTION public.get_candidate_skills(p_userid integer)
 RETURNS TABLE(id integer, userid integer, skillid integer, skillname text, experienceyears integer)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT
         cs.id, 
         cs.userid,
         cs.skillid,
         s.name::text ,
         cs.experienceyears
         FROM candidate_skills cs
         INNER JOIN skills s
         ON s.id = cs.skillid
         WHERE cs.userid = p_userid;
END;
$function$
;
