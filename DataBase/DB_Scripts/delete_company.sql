CREATE OR REPLACE FUNCTION delete_company
(
 p_id bigint,
 p_updatedby bigint
)
RETURNS boolean
LANGUAGE plpgsql
AS $$
BEGIN

UPDATE company
SET
isdeleted=1,
updatedby=p_updatedby,
updateddate=NOW()

WHERE id=p_id;


RETURN TRUE;

END;
$$;