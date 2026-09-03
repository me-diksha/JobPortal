CREATE OR REPLACE FUNCTION public.update_company(
    p_name           character varying,
    p_description    character varying,
    p_industry      character varying,
    p_website       character varying,
    p_email         character varying,
    p_phone         character varying,
    p_addressline1  character varying,
    p_addressline2  character varying,
    p_city          character varying,
    p_state         character varying,
    p_country       character varying,
    p_postalcode    character varying,
    p_logosurl      character varying,
    p_company_size  integer,
    p_foundedyear   integer,
    p_updatedby     bigint,
    p_companyid     bigint
)
RETURNS TABLE (
    id              bigint,
    name            character varying,
    description     character varying,
    industry        character varying,
    website         character varying,
    email           character varying,
    phone           character varying,
    addressline1    character varying,
    addressline2    character varying,
    city            character varying,
    state           character varying,
    country         character varying,
    postalcode      character varying,
    logosurl        character varying,
    company_size    integer,
    foundedyear     integer,
    updatedby       bigint
)
LANGUAGE plpgsql
AS $function$
BEGIN

    RETURN QUERY
    UPDATE company
    SET
        name          = p_name,
        description   = p_description,
        industry      = p_industry,
        website       = p_website,
        email         = p_email,
        phone         = p_phone,
        addressline1  = p_addressline1,
        addressline2  = p_addressline2,
        city          = p_city,
        state         = p_state,
        country       = p_country,
        postalcode    = p_postalcode,
        logosurl      = p_logosurl,
        company_size  = p_company_size,
        foundedyear   = p_foundedyear,
        updatedby     = p_updatedby
    WHERE company.id = p_companyid
    RETURNING
        company.id,
        company.name,
        company.description,
        company.industry,
        company.website,
        company.email,
        company.phone,
        company.addressline1,
        company.addressline2,
        company.city,
        company.state,
        company.country,
        company.postalcode,
        company.logosurl,
        company.company_size,
        company.foundedyear,
        company.updatedby;

END;
$function$;