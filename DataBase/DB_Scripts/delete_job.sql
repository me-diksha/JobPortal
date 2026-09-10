-- DROP FUNCTION public.delete_job(int8, int8, int4);

CREATE OR REPLACE FUNCTION public.delete_job(p_jobid bigint, p_companyid bigint, p_updatedby integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN

    UPDATE jobs
    SET 
        isdeleted = 1,
        updatedat = NOW(),
		updatedby = p_updatedby
    WHERE id = p_jobid and companyid = p_companyid;

    RETURN TRUE;

END;
$function$
;
