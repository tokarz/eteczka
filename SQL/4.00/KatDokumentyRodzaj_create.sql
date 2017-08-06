-- Table: public."KatDokumentyRodzaj"

-- DROP TABLE public."KatDokumentyRodzaj";

CREATE TABLE public."KatDokumentyRodzaj"
(
  symbol character(20) NOT NULL, -- Symbol dokumetu, np. SWPR - Å“wiadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  dokwlasny boolean, -- Okreœla czy dokument zosta³ wytworzony przez nas czy jest to dokument obcy True=w³asny
  jrwa character(10), -- Pe³na klasyfikacja JRWA
  teczkadzial character(10), -- Czêœæ akt - dozwolone wartoœci : A,B,C
  typedycji character(2), -- Okreœla pola które maj¹ byæ wymagane w edycji, np data dokumentu, data wa¿noœci itp.
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
  IS 'Slownik rodzajÃ³w dokumentÃ³w, np. BDL - badania lekarskie, SWPR - Å“wiadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - Å“wiadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';
COMMENT ON COLUMN public."KatDokumentyRodzaj".dokwlasny IS 'Okreœla czy dokument zosta³ wytworzony przez nas czy jest to dokument obcy True=w³asny';
COMMENT ON COLUMN public."KatDokumentyRodzaj".jrwa IS 'Pe³na klasyfikacja JRWA';
COMMENT ON COLUMN public."KatDokumentyRodzaj".teczkadzial IS 'Czêœæ akt - dozwolone wartoœci : A,B,C';
COMMENT ON COLUMN public."KatDokumentyRodzaj".typedycji IS 'Okreœla pola które maj¹ byæ wymagane w edycji, np data dokumentu, data wa¿noœci itp.';

