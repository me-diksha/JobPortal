CREATE TABLE experiencelevel
(
    id BIGSERIAL PRIMARY KEY,
    description VARCHAR(100) NOT NULL,
    isdeleted BOOLEAN NOT NULL DEFAULT FALSE
);

INSERT INTO experiencelevel (description)
VALUES
    ('Fresher'),
    ('0-2 Years'),
    ('2-5 Years'),
    ('5+ Years');