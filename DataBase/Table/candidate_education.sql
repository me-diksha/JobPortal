-- public.candidate_experience definition

-- Drop table

-- DROP TABLE public.candidate_experience;

CREATE TABLE public.candidate_education (
    id serial4 NOT NULL,
    userid int4 NOT NULL,
    institutename varchar(500) NOT NULL,
    degree varchar(200) NOT NULL,
    fieldofstudy varchar(200) NOT NULL,
    startyear int4 NOT NULL,
    endyear int4 NULL,
    percentage numeric(5,2) NULL,
    createdat timestamp DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (id),
    CONSTRAINT fk_candidate_education_user
        FOREIGN KEY (userid)
        REFERENCES public.users(id)
        ON DELETE CASCADE
);