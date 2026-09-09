CREATE OR REPLACE FUNCTION update_job_status(
    p_jobid BIGINT,
    p_companyid BIGINT,
    p_statusid INTEGER,
    p_updatedby INTEGER
)
RETURNS BOOLEAN
AS $$
BEGIN

    UPDATE jobs
    SET
        refstatus = p_statusid,
        updatedat = CURRENT_TIMESTAMP
    WHERE id = p_jobid
      AND companyid = p_companyid
      AND isdeleted = 0;

    IF FOUND THEN
        RETURN TRUE;
    END IF;

    RETURN FALSE;

END;
$$ LANGUAGE plpgsql;