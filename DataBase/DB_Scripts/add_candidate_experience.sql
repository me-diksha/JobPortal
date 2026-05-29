-- DROP FUNCTION public.add_candidate_experience(int4, text, text, date, date, bool, text);

CREATE OR REPLACE FUNCTION public.add_candidate_experience(p_userid integer, p_companyname text, p_designation text, p_startdate date, p_enddate date, p_currentlyworking boolean, p_description text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
DECLARE
    v_id INTEGER;
BEGIN

    INSERT INTO candidate_experience
    (
        userid,
        companyname,
        designation,
        startdate,
        enddate,
        currentlyworking,
        description
    )
    VALUES
    (
        p_userid,
        p_companyname,
        p_designation,
        p_startdate,
        p_enddate,
        p_currentlyworking,
        p_description
    )
    RETURNING id INTO v_id;

    RETURN v_id;

END;
$function$
;
