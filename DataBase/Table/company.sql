CREATE TABLE company
(
    id                  BIGSERIAL PRIMARY KEY,

    name                VARCHAR(150) NOT NULL,
    description         VARCHAR(1000),

    industry            VARCHAR(100),
    website             VARCHAR(255),

    email               VARCHAR(150),
    phone               VARCHAR(20),

    addressline1        VARCHAR(255),
    addressline2        VARCHAR(255),

    city                VARCHAR(100),
    state               VARCHAR(100),
    country             VARCHAR(100),
    postalcode          VARCHAR(20),

    logosurl            VARCHAR(500),

    company_size         INT,
    foundedyear         INT,

    isactive            SMALLINT NOT NULL DEFAULT 1,
    isdeleted           SMALLINT NOT NULL DEFAULT 0,

    createdby           BIGINT NOT NULL,
    createddate         TIMESTAMP NOT NULL DEFAULT NOW(),

    updatedby           BIGINT,
    updateddate         TIMESTAMP,

    CONSTRAINT chk_company_active
        CHECK (isactive IN (0,1)),

    CONSTRAINT chk_company_deleted
        CHECK (isdeleted IN (0,1)),

    CONSTRAINT fk_company_createdby
        FOREIGN KEY (createdby)
        REFERENCES users(id),

    CONSTRAINT fk_company_updatedby
        FOREIGN KEY (updatedby)
        REFERENCES users(id)
);