-- public.candidate_skills definition

-- Drop table

-- DROP TABLE public.candidate_skills;

CREATE TABLE public.candidate_skills (
	id serial4 NOT NULL,
	userid int4 NOT NULL,
	skillid int4 NOT NULL,
	experienceyears int4 DEFAULT 0 NOT NULL,
	createdat timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT candidate_skills_pkey PRIMARY KEY (id),
	CONSTRAINT fk_candidate_skill_skill FOREIGN KEY (skillid) REFERENCES public.skills(id) ON DELETE CASCADE,
	CONSTRAINT fk_candidate_skill_user FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE
);