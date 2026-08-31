CREATE OR REPLACE FUNCTION create_company
(
    p_name varchar,
    p_description varchar,
    p_industry varchar,
    p_website varchar,
    p_email varchar,
    p_phone varchar,
    p_addressline1 varchar,
    p_addressline2 varchar,
    p_city varchar,
    p_state varchar,
    p_country varchar,
    p_postalcode varchar,
    p_logosurl varchar,
    p_company_size int,
    p_foundedyear int,
    p_createdby bigint
)
RETURNS bigint
LANGUAGE plpgsql
AS $$
DECLARE
    companyid bigint;
BEGIN

    INSERT INTO company
    (
        name,
        description,
        industry,
        website,
        email,
        phone,
        addressline1,
        addressline2,
        city,
        state,
        country,
        postalcode,
        logosurl,
        company_size,
        foundedyear,
        createdby,
		createddate
    )
    VALUES
    (
        p_name,
        p_description,
        p_industry,
        p_website,
        p_email,
        p_phone,
        p_addressline1,
        p_addressline2,
        p_city,
        p_state,
        p_country,
        p_postalcode,
        p_logosurl,
        p_company_size,
        p_foundedyear,
        p_createdby,
		NOW()
    )
    RETURNING id INTO companyid;


    RETURN companyid;

END;
$$;