CREATE OR REPLACE FUNCTION public.update_candidate_experience
(
    p_id INTEGER,
    p_userid INTEGER,
    p_companyname TEXT,
    p_designation TEXT,
    p_startdate DATE,
    p_enddate DATE,
    p_currentlyworking BOOLEAN,
    p_description TEXT
)
RETURNS TABLE
(
    id INTEGER,
    userid INTEGER,
    companyname TEXT,
    designation TEXT,
    startdate DATE,
    enddate DATE,
    currentlyworking BOOLEAN,
    description TEXT
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
    WHERE id = p_id
      AND userid = p_userid
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