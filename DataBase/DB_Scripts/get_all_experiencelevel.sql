create or replace function get_all_experiencelevel()
returns table(
 id bigint,
 description character varying
)
as $$
begin
	return query
	select e.id,
		   e.description
	from experiencelevel e
	where e.isdeleted=false;
end;
$$ LANGUAGE plpgsql;
