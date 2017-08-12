-- Table: public."KatDokumentyRodzaj"

-- DROP TABLE public."KatDokumentyRodzaj";

CREATE TABLE public."KatDokumentyRodzaj"
(
  symbol character(20) NOT NULL, -- Symbol dokumetu, np. SWPR - œwiadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  dokwlasny boolean, -- Okre�la czy dokument zosta� wytworzony przez nas czy jest to dokument obcy True=w�asny
  jrwa character(10), -- Pe�na klasyfikacja JRWA
  teczkadzial character(10), -- Cz�� akt - dozwolone warto�ci : A,B,C
  typedycji character(2), -- Okre�la pola kt�re maj� by� wymagane w edycji, np data dokumentu, data wa�no�ci itp.
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  CONSTRAINT "KatDokumentyRodzaj_pkey" PRIMARY KEY (symbol)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDokumentyRodzaj"
  OWNER TO postgres;
COMMENT ON TABLE public."KatDokumentyRodzaj"
  IS 'Slownik rodzajów dokumentów, np. BDL - badania lekarskie, SWPR - œwiadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - œwiadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';
COMMENT ON COLUMN public."KatDokumentyRodzaj".dokwlasny IS 'Okre�la czy dokument zosta� wytworzony przez nas czy jest to dokument obcy True=w�asny';
COMMENT ON COLUMN public."KatDokumentyRodzaj".jrwa IS 'Pe�na klasyfikacja JRWA';
COMMENT ON COLUMN public."KatDokumentyRodzaj".teczkadzial IS 'Cz�� akt - dozwolone warto�ci : A,B,C';
COMMENT ON COLUMN public."KatDokumentyRodzaj".typedycji IS 'Okre�la pola kt�re maj� by� wymagane w edycji, np data dokumentu, data wa�no�ci itp.';

