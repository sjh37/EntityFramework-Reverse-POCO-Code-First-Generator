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
