ALTER TABLE candidate_profiles
ADD COLUMN isdeleted SMALLINT NOT NULL DEFAULT 0;

ALTER TABLE candidate_profiles
ADD CONSTRAINT chk_candidate_profile_deleted
CHECK (isdeleted IN (0,1));
ALTER TABLE candidate_profiles
ADD COLUMN updateddate TIMESTAMP;