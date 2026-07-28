CREATE OR REPLACE FUNCTION get_compani
(
 p_id bigint
)
RETURNS SETOF company
LANGUAGE plpgsql
AS $$
BEGIN

RETURN QUERY

SELECT *
FROM company
WHERE id=p_id
AND isdeleted=0 ;

END;
$$;