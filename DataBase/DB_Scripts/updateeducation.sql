CREATE OR REPLACE FUNCTION public.updateeducation(
    p_userid integer,
    p_instituename text,
    p_degree text,
    p_fieldofstudy text,
    p_startyear integer,
    p_endyear integer,
    p_percentage numeric,
    p_educationid integer
)
RETURNS TABLE(
    id integer,
    userid integer,
    institutename character varying,
    degree text,
    fieldofstudy character varying,
    startyear integer,
    endyear integer,
    percentage numeric
)
LANGUAGE plpgsql
AS $function$
BEGIN

    RETURN QUERY
    UPDATE candidate_education AS ce
    SET
        institutename = p_instituename,
        degree = p_degree,
        fieldofstudy = p_fieldofstudy,
        startyear = p_startyear,
        endyear = p_endyear,
        percentage = p_percentage
    WHERE ce.id = p_educationid
      AND ce.userid = p_userid
    RETURNING
        ce.id,
        ce.userid,
        ce.institutename,
        ce.degree,
        ce.fieldofstudy,
        ce.startyear,
        ce.endyear,
        ce.percentage;

END;
$function$;