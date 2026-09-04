create or replace function get_all_jobStatus()
returns table(
 id bigint,
 description character varying
)
as $$
begin
	return query
	select j.id,
		   j.description
	from jobstatus j
	where j.isdeleted =false;
end;
$$ LANGUAGE plpgsql;
