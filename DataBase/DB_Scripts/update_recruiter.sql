CREATE OR REPLACE FUNCTION update_recruiter
(
    p_userid bigint,
    p_companyid bigint,
    p_firstname varchar,
    p_lastname varchar,
    p_designation varchar,
    p_department varchar,
    p_phone varchar,
    p_linkedinurl varchar
)
RETURNS bigint
LANGUAGE plpgsql
AS
$$
BEGIN

UPDATE recruiter
SET

companyid=p_companyid,
firstname=p_firstname,
lastname=p_lastname,
designation=p_designation,
department=p_department,
phone=p_phone,
linkedinurl=p_linkedinurl,
updatedat=NOW()

WHERE userid=p_userid;

RETURN recruiter.id;

END;
$$;