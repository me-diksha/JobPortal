CREATE OR REPLACE FUNCTION delete_candidate_profiles
(
    p_userid BIGINT
)
RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
BEGIN

    UPDATE candidate_profiles
    SET 
        isdeleted = 1,
        updateddate = NOW()
    WHERE userid = p_userid;

    RETURN TRUE;

END;
$$;