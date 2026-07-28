CREATE OR REPLACE FUNCTION get_all_companies()
RETURNS TABLE
(
    id BIGINT,
    name VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN

    RETURN QUERY

    SELECT 
        c.id,
        c.name

    FROM company c

    WHERE c.isactive = 1
    AND c.isdeleted = 0

    ORDER BY c.name;

END;
$$;