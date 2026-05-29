-- DROP FUNCTION public.getuserbyemail(text);

CREATE OR REPLACE FUNCTION public.getuserbyemail(p_email text)
 RETURNS TABLE(id integer, name character varying, email character varying, passwordhash text, roleid integer)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT u.id, u.name, u.email, u.passwordhash, u.roleid 
    FROM users u 
    WHERE LOWER(u.email) = LOWER(p_email) 
    LIMIT 1;
END;
$function$
;
