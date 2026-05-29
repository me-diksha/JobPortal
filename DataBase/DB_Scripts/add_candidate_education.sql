-- DROP FUNCTION public.add_candidate_education(int4, text, text, text, int4, int4, numeric);

CREATE OR REPLACE FUNCTION public.add_candidate_education(p_userid integer, p_instituename text, p_degree text, p_fieldofstudy text, p_startyear integer, p_endyear integer, p_percentage numeric)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
DECLARE
    v_id INTEGER;
BEGIN

    INSERT INTO candidate_education
    (
        userid,
        institutename,
        degree,
        fieldofstudy,
        startyear,
        endyear,
        percentage
      
    )
    VALUES
    (
        p_userid,
        p_instituename , 
        p_degree ,
        p_fieldofstudy ,
        p_startyear ,
        p_endyear ,
        p_percentage 
    )
    RETURNING id INTO v_id;

    RETURN v_id;

END;
$function$
;
