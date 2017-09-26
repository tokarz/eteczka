-- Table: public."KatDokumentyRodzaj"

-- DROP TABLE public."KatDokumentyRodzaj";

CREATE TABLE public."KatDokumentyRodzaj"
(
  symbol character(20) NOT NULL, -- Symbol dokumetu, np. SWPR - swiadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  dokwlasny boolean, -- Okresla czy dokument zostal wytworzony przez nas True, dokument obcy False
  jrwa character(10), -- Pelna klasyfikacja JRWA
  teczkadzial character(1), -- Czesc akt - dozwolone wartoscici : A,B,C
  typedycji character(2), -- Okresla pola ktore maja byc wymagane w edycji, np data dokumentu, data waznoci itp.
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  confidential numeric(2,0), -- Poufnosc
  CONSTRAINT "KatDokumentyRodzaj_pkey" PRIMARY KEY (symbol)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDokumentyRodzaj"
  OWNER TO eadadmin;
GRANT ALL ON TABLE public."KatDokumentyRodzaj" TO eadadmin;
GRANT ALL ON TABLE public."KatDokumentyRodzaj" TO ead;
COMMENT ON TABLE public."KatDokumentyRodzaj"
  IS 'Slownik rodzajĂłw, np. BDL - badania lekarskie, SWPR - swiadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - swiadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';
COMMENT ON COLUMN public."KatDokumentyRodzaj".dokwlasny IS 'Okresla czy dokument zostal wytworzony przez nas True, dokument obcy False';
COMMENT ON COLUMN public."KatDokumentyRodzaj".jrwa IS 'Pelna klasyfikacja JRWA';
COMMENT ON COLUMN public."KatDokumentyRodzaj".teczkadzial IS 'Czesc akt - dozwolone wartoscici : A,B,C';
COMMENT ON COLUMN public."KatDokumentyRodzaj".typedycji IS 'Okresla pola ktore maja byc wymagane w edycji, np data dokumentu, data waznoci itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".confidential IS 'Poufnosc';

