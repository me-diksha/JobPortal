--DROP FUNCTION public.get_all_skills();

CREATE OR REPLACE FUNCTION public.get_all_skills()
 RETURNS TABLE(id int, name character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN

    RETURN QUERY

    SELECT 
        s.id,
        s.name

    FROM skills s
    ORDER BY s.name;

END;
$function$
;
