-- DROP FUNCTION public.get_candidateprofile(int4);

CREATE OR REPLACE FUNCTION public.get_candidateprofile(p_userid integer)
 RETURNS SETOF candidate_profiles
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT *
    FROM candidate_profiles
    WHERE userid = p_userid
	AND isdeleted = 0;
END;
$function$
;
