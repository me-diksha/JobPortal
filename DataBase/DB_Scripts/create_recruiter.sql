CREATE OR REPLACE FUNCTION create_recruiter
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
DECLARE
    recruiterid bigint;
BEGIN

    INSERT INTO recruiter
    (
        userid,
        companyid,
        firstname,
        lastname,
        designation,
        department,
        phone,
        linkedinurl
    )
    VALUES
    (
        p_userid,
        p_companyid,
        p_firstname,
        p_lastname,
        p_designation,
        p_department,
        p_phone,
        p_linkedinurl
    )
    RETURNING id
    INTO recruiterid;

    RETURN recruiterid;

END;
$$;