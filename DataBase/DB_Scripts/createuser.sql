-- DROP FUNCTION public.createuser(text, text, text, int4);

CREATE OR REPLACE FUNCTION public.createuser(p_name text, p_email text, p_passwordhash text, p_roleid integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
DECLARE v_id INTEGER;
BEGIN
    INSERT INTO users (name, email, passwordhash, roleid) 
    VALUES (p_name, p_email, p_passwordhash, p_roleid) 
    RETURNING id INTO v_id;
    
    RETURN v_id;
END;
$function$
;
