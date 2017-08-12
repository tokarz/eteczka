-- Table: public."Pliki"

-- DROP TABLE public."Pliki";

CREATE TABLE public."Pliki"
(
  symbol character(30),
  dataskanu character(10),
  datadokumentu character(10), -- Data wytworzenia papieru
  datapocz character(10),
  datakoniec character(10),
  nazwapliku character(254),
  pelnasciezka character(254),
  typpliku character(10),
  opisdodatkowy character(254),
  numeread character(20) NOT NULL,
  dokwlasny boolean,
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  id integer NOT NULL DEFAULT nextval('"Pliki_id_seq"'::regclass),
  CONSTRAINT "Pliki_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."Pliki"
  OWNER TO postgres;
COMMENT ON COLUMN public."Pliki".datadokumentu IS 'Data wytworzenia papieru';

