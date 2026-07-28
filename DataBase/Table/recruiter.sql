CREATE TABLE recruiter
(
    id              BIGSERIAL PRIMARY KEY,
    userid          BIGINT NOT NULL UNIQUE,
    companyid       BIGINT NULL,

    firstname       VARCHAR(100) NOT NULL,
    lastname        VARCHAR(100) NOT NULL,
    designation     VARCHAR(100) NULL,
    department      VARCHAR(100) NULL,
    phone           VARCHAR(20) NULL,
    linkedinurl     VARCHAR(255) NULL,

    isactive        SMALLINT NOT NULL DEFAULT 1,
    isdeleted       SMALLINT NOT NULL DEFAULT 0,

    createdat       TIMESTAMP NOT NULL DEFAULT NOW(),
    updatedat       TIMESTAMP NOT NULL DEFAULT NOW(),

    CONSTRAINT fk_recruiter_user
        FOREIGN KEY(userid)
        REFERENCES users(id),

    CONSTRAINT fk_recruiter_company
        FOREIGN KEY(companyid)
        REFERENCES company(id),

    CONSTRAINT chk_recruiter_active
        CHECK(isactive IN (0,1)),

    CONSTRAINT chk_recruiter_deleted
        CHECK(isdeleted IN (0,1))
);