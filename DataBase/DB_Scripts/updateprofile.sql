--DROP FUNCTION public.updateprofile(int4, text, text, numeric, numeric, text, text, text, text, text, text, text, text, int4);

CREATE OR REPLACE FUNCTION public.updateprofile(p_userid integer, p_headline text, p_bio text, p_currentsalary numeric, p_expectedsalary numeric, p_resumeurl text, p_addressline1 text, p_addressline2 text, p_city text, p_state text, p_country text, p_firstname text, p_lastname text, p_profileid integer)
 RETURNS TABLE(id integer, userid integer, headline character varying, bio text, currentsalary numeric, expectedsalary numeric, resumeurl character varying, addressline1 character varying, addressline2 character varying, city character varying, state character varying, country character varying, firstname character varying, lastname character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN

    RETURN QUERY
    UPDATE candidate_profiles cp
    SET
        headline       = p_headline,
        bio            = p_bio,
        currentsalary  = p_currentsalary,
        expectedsalary = p_expectedsalary,
        resumeurl      = p_resumeurl,
        addressline1   = p_addressline1,
        addressline2   = p_addressline2,
        city           = p_city,
        state          = p_state,
        country        = p_country,
        firstname      = p_firstname,
        lastname       = p_lastname,
		updateddate     = NOW()
    WHERE cp.userid = p_userid
      AND cp.id = p_profileid
    RETURNING
        cp.id,
        cp.userid,
        cp.headline,
        cp.bio,
        cp.currentsalary,
        cp.expectedsalary,
        cp.resumeurl,
        cp.addressline1,
        cp.addressline2,
        cp.city,
        cp.state,
        cp.country,
        cp.firstname,
        cp.lastname;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Profile not found.';
    END IF;

END;
$function$
;
