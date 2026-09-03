create or replace function get_all_employmenttype()
returns table(
 id bigint,
 description character varying
)
as $$
begin
	return query
	select e.id,
		   e.description
	from employment_type e;
end;
$$ LANGUAGE plpgsql;
