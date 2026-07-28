CREATE OR REPLACE FUNCTION delete_recruiter
(
    p_userid bigint
)
RETURNS boolean
LANGUAGE plpgsql
AS
$$
BEGIN

UPDATE recruiter
SET

isdeleted=1,
updatedat=NOW()

WHERE userid=p_userid;

RETURN FOUND;

END;
$$;