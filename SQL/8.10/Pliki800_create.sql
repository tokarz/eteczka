-- Table: public."Pliki"

-- DROP TABLE public."Pliki";

CREATE TABLE public."Pliki"
(
  firma character(20) NOT NULL, -- np. TFW
  numeread character(20) NOT NULL, -- numeread
  symbol character(20) NOT NULL, -- symbol dokumentu z KatDokumentyRodzaj
  dataskanu character(10), -- 2016-09-28
  datadokumentu character(10), -- 1985-10-17
  datapocz character(10), -- 1984-10-30
  datakoniec character(10), -- 2017-09-13
  nazwascan character(254), -- Nazwa oryginalu po zeskanowaniu
  nazwaead character(254), -- Nazwa po opisaniu i przesunieciu do folderu pliki
  pelnasciezkaead character(254), -- Pelna sciezka do pliku w folderze pliki
  typpliku character(10), -- PDF, JPG itp.
  opisdodatkowy character(254), -- Wlasny, dowolny opis wprowadzony przez Kozel
  dokwlasny boolean, -- Dokument nasz lub obcy
  systembazowy character(3) DEFAULT 'EAD'::bpchar, -- zawsze EAD
  usuniety boolean DEFAULT false,
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone NOT NULL,
  id integer NOT NULL DEFAULT nextval('pliki_id_seq'::regclass),
  teczkadzial character(1), -- A, B, lub C wedlug KatDokumentyRodzaj
  CONSTRAINT "Pliki_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."Pliki"
  OWNER TO eadadmin;
GRANT ALL ON TABLE public."Pliki" TO eadadmin;
GRANT ALL ON TABLE public."Pliki" TO ead;
COMMENT ON COLUMN public."Pliki".firma IS 'np. TFW';
COMMENT ON COLUMN public."Pliki".numeread IS 'numeread';
COMMENT ON COLUMN public."Pliki".symbol IS 'symbol dokumentu z KatDokumentyRodzaj';
COMMENT ON COLUMN public."Pliki".dataskanu IS '2016-09-28';
COMMENT ON COLUMN public."Pliki".datadokumentu IS '1985-10-17';
COMMENT ON COLUMN public."Pliki".datapocz IS '1984-10-30';
COMMENT ON COLUMN public."Pliki".datakoniec IS '2017-09-13';
COMMENT ON COLUMN public."Pliki".nazwascan IS 'Nazwa oryginalu po zeskanowaniu';
COMMENT ON COLUMN public."Pliki".nazwaead IS 'Nazwa po opisaniu i przesunieciu do folderu pliki';
COMMENT ON COLUMN public."Pliki".pelnasciezkaead IS 'Pelna sciezka do pliku w folderze pliki';
COMMENT ON COLUMN public."Pliki".typpliku IS 'PDF, JPG itp.';
COMMENT ON COLUMN public."Pliki".opisdodatkowy IS 'Wlasny, dowolny opis wprowadzony przez Kozel';
COMMENT ON COLUMN public."Pliki".dokwlasny IS 'Dokument nasz lub obcy';
COMMENT ON COLUMN public."Pliki".systembazowy IS 'zawsze EAD';
COMMENT ON COLUMN public."Pliki".teczkadzial IS 'A, B, lub C wedlug KatDokumentyRodzaj';

