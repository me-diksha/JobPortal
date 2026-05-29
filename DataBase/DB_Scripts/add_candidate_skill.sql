-- DROP FUNCTION public.add_candidate_skill(int4, int4, int4);

CREATE OR REPLACE FUNCTION public.add_candidate_skill(p_userid integer, p_skillid integer, p_experienceyears integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
DECLARE
    v_id INTEGER;
BEGIN

    INSERT INTO candidate_skills
    (
        userid,
        skillid,
        experienceyears
    )
    VALUES
    (
        p_userid,
        p_skillid,
        p_experienceyears
    )
    RETURNING id INTO v_id;

    RETURN v_id;

END;
$function$
;
