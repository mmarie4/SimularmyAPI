do
$$
begin
	CREATE TABLE users (
	    id uuid not null ,
	    pseudo varchar not null ,
	    email varchar not null ,
	    password_hash varchar not null ,
	    password_salt varchar not null ,
	    created_at timestamp not null ,
	    created_by uuid not null ,
	    updated_at timestamp not null ,
	    updated_by uuid not null ,
		is_admin bool default false
	);
	ALTER TABLE users ADD CONSTRAINT pk_users PRIMARY KEY ( id );
	CREATE INDEX idx_users_email ON users ( email );
end;
$$;