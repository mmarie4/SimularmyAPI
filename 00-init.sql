do
$$
begin
	CREATE TABLE users (
	    id uuid not null ,
	    pseudo varchar not null ,
	    email varchar not null ,
	    password_hash varchar not null ,
	    password_salt varchar not null ,
		is_admin bool default false ,
		units varchar not null ,
		elo integer not null ,
	    created_at timestamp not null ,
	    created_by uuid not null ,
	    updated_at timestamp not null ,
	    updated_by uuid not null
	);
	ALTER TABLE users ADD CONSTRAINT pk_users PRIMARY KEY ( id );
	CREATE INDEX idx_users_email ON users ( email );

	CREATE TABLE units (
	    id integer not null ,
	    name varchar not null ,
	    stats varchar not null ,
		type integer not null
	);
	ALTER TABLE units ADD CONSTRAINT pk_units PRIMARY KEY ( id );

end;
$$;
