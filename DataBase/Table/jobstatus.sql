CREATE TABLE jobstatus
(
    id BIGSERIAL PRIMARY KEY,
    description VARCHAR(100) NOT NULL,
    isdeleted BOOLEAN NOT NULL DEFAULT FALSE
);
INSERT INTO jobstatus (description)
VALUES
    ('Draft'),
    ('Open'),
    ('Closed'),
	('Actively Recruiting');