ALTER TABLE jobs
ADD COLUMN refexperiencelevel BIGINT,
ADD COLUMN refstatus BIGINT;

ALTER TABLE jobs
ADD CONSTRAINT fk_jobs_experiencelevel
FOREIGN KEY (refexperiencelevel)
REFERENCES experiencelevel(id);

ALTER TABLE jobs
ADD CONSTRAINT fk_jobs_status
FOREIGN KEY (refstatus)
REFERENCES jobstatus(id);


ALTER TABLE jobs
DROP COLUMN experiencelevel,
DROP COLUMN status;