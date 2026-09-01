
CREATE OR REPLACE FUNCTION public.get_recruiter(p_userid integer)
 RETURNS TABLE(id bigint, userid integer, companyid bigint, firstname character varying, lastname character varying, designation character varying, department character varying, phone character varying, linkedinurl character varying, companyname character varying)
 LANGUAGE plpgsql
AS $function$
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
r.linkedinurl,
c.name
FROM recruiter r
left join company c on c.id = r.companyid
WHERE r.userid=p_userid
AND r.isdeleted=0;

END;
$function$
;
