-- DROP FUNCTION public.createprofile(int4, text, text, numeric, numeric, text, text, text, text, text, text, text, text);

CREATE OR REPLACE FUNCTION public.createprofile(p_userid integer, p_headline text, p_bio text, p_currentsalary numeric, p_expectedsalary numeric, p_resumeurl text, p_addressline1 text, p_addressline2 text, p_city text, p_state text, p_country text, p_firstname text, p_lastname text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
DECLARE 
    v_id INTEGER;
BEGIN
    INSERT INTO candidate_profiles (
        userid,
        headline,
        bio,
        currentsalary,
        expectedsalary,
        resumeurl,
        addressline1,
        addressline2,
        city,
        state,
        country,
        firstname,
        lastname
    )
    VALUES (
        p_userid,
        p_Headline,
        p_Bio,
        p_CurrentSalary,
        p_ExpectedSalary,
        p_ResumeUrl,
        p_AddressLine1,
        p_AddressLine2,
        p_City,
        p_State,
        p_Country,
        p_Firstname,
        p_LastName
    )
    RETURNING id INTO v_id;

    RETURN v_id;
END;
$function$
;
