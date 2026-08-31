CREATE OR REPLACE FUNCTION get_recruiter
(
    p_userid int
)
RETURNS TABLE
(
    id bigint,
    userid int,
    companyid bigint,
    firstname varchar,
    lastname varchar,
    designation varchar,
    department varchar,
    phone varchar,
    linkedinurl varchar
)
LANGUAGE plpgsql
AS
$$
BEGIN

RETURN QUERY

SELECT
r.id,
r.userid,
r.companyid,
r.firstname,
r.lastname,
r.designation,
r.department,
r.phone,
r.linkedinurl
FROM recruiter r
WHERE r.userid=p_userid
AND r.isdeleted=0;

END;
$$;