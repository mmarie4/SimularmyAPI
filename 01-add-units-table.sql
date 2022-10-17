do
$$
begin
	CREATE TABLE units (
	    id integer not null ,
	    name varchar not null ,
	    stats jsonb not null
	);
	ALTER TABLE units ADD CONSTRAINT pk_units PRIMARY KEY ( id );
end;
$$;