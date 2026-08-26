CREATE OR REPLACE FUNCTION public.update_candidate_experience(
    p_id integer,
    p_userid integer,
    p_companyname text,
    p_designation text,
    p_startdate date,
    p_enddate date,
    p_currentlyworking boolean,
    p_description text
)
RETURNS TABLE(
    id integer,
    userid integer,
    companyname varchar,
    designation varchar,
    startdate date,
    enddate date,
    currentlyworking boolean,
    description varchar
)
LANGUAGE plpgsql
AS $function$
BEGIN

    RETURN QUERY
    UPDATE candidate_experience
    SET
        companyname = p_companyname,
        designation = p_designation,
        startdate = p_startdate,
        enddate = p_enddate,
        currentlyworking = p_currentlyworking,
        description = p_description
    WHERE candidate_experience.id = p_id
      AND candidate_experience.userid = p_userid
    RETURNING
        candidate_experience.id,
        candidate_experience.userid,
        candidate_experience.companyname,
        candidate_experience.designation,
        candidate_experience.startdate,
        candidate_experience.enddate,
        candidate_experience.currentlyworking,
        candidate_experience.description;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Experience not found.';
    END IF;

END;
$function$;