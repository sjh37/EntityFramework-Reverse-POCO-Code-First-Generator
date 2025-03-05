CREATE TABLE public.AllColumnTypes
(
	"bigint" bigint,
	--"bit_varying" bit varying,
	"bit_1" bit(1),
	"bit_8" bit(8),
	"boolean" boolean,
	"box" box,
	"bytea" bytea,
	"char" char,
	"character" character,
	"character_varying" character varying,
	"cid" cid,
	"cidr" cidr,
	"circle" circle,
	"date" date,
	"double_precision" double precision,
	"inet" inet,
	"integer" integer,
	"interval" interval,
	"json" json,
	"jsonb" jsonb,
	"line" line,
	"lseg" lseg,
	--"macaddr" macaddr,
	"money" money,
	"name" name,
	"numeric" numeric,
	"oid" oid,
	"oidvector" oidvector,
	"path" path,
	"point" point,
	"polygon" polygon,
	"real" real,
	"smallint" smallint,
	"text" text,
	"time_with_time_zone" time with time zone,
	"time_without_time_zone" time without time zone,
	"timestamp_with_time_zone" timestamp with time zone,
	"timestamp_without_time_zone" timestamp without time zone,
	--"tsquery" tsquery,
	--"tsvector" tsvector,
	"uuid" uuid,
	"xid" xid,
	"xml" xml
);

ALTER TABLE public.AllColumnTypes OWNER TO postgres;

ALTER TABLE ONLY public.AllColumnTypes
    ADD CONSTRAINT pk_AllColumnTypes PRIMARY KEY ("bigint");

insert into public.AllColumnTypes ("bigint") VALUES (1234);
GO

-- 855 Code generation does not create the navigation properties unless it's in the same schema
CREATE SCHEMA another
    AUTHORIZATION postgres;
GO

CREATE TABLE public.categories (
    category_id smallint NOT NULL,
    category_name character varying(15) NOT NULL
);

ALTER TABLE ONLY categories
    ADD CONSTRAINT pk_categories PRIMARY KEY (category_id);

CREATE TABLE another.category_description (
    category_id smallint NOT NULL,
    description character varying(80) NOT NULL
);

ALTER TABLE ONLY another.category_description
    ADD CONSTRAINT pk_category_description PRIMARY KEY (category_id);

ALTER TABLE ONLY another.category_description
    ADD CONSTRAINT fk_category_description FOREIGN KEY (category_id) REFERENCES public.categories;
