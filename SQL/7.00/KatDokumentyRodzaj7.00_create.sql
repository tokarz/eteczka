-- Table: public."KatDokumentyRodzaj"

-- DROP TABLE public."KatDokumentyRodzaj";

CREATE TABLE public."KatDokumentyRodzaj"
(
  symbol character(20) NOT NULL, -- Symbol dokumetu, np. SWPR - Ă„Ä…Ă˘â‚¬Ĺ›wiadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  dokwlasny boolean, -- OkreÄąâ€şla czy dokument zostaÄąâ€š wytworzony przez nas czy jest to dokument obcy True=wÄąâ€šasny
  jrwa character(10), -- PeÄąâ€šna klasyfikacja JRWA
  teczkadzial character(10), -- CzĂ„â„˘Äąâ€şĂ„â€ˇ akt - dozwolone wartoÄąâ€şci : A,B,C
  typedycji character(2), -- OkreÄąâ€şla pola ktÄ‚Ĺ‚re majĂ„â€¦ byĂ„â€ˇ wymagane w edycji, np data dokumentu, data waÄąÄ˝noÄąâ€şci itp.
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
  OWNER TO postgres;
COMMENT ON TABLE public."KatDokumentyRodzaj"
  IS 'Slownik rodzajĂ„â€šÄąâ€šw dokumentĂ„â€šÄąâ€šw, np. BDL - badania lekarskie, SWPR - Ă„Ä…Ă˘â‚¬Ĺ›wiadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - Ă„Ä…Ă˘â‚¬Ĺ›wiadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';
COMMENT ON COLUMN public."KatDokumentyRodzaj".dokwlasny IS 'OkreÄąâ€şla czy dokument zostaÄąâ€š wytworzony przez nas czy jest to dokument obcy True=wÄąâ€šasny';
COMMENT ON COLUMN public."KatDokumentyRodzaj".jrwa IS 'PeÄąâ€šna klasyfikacja JRWA';
COMMENT ON COLUMN public."KatDokumentyRodzaj".teczkadzial IS 'CzĂ„â„˘Äąâ€şĂ„â€ˇ akt - dozwolone wartoÄąâ€şci : A,B,C';
COMMENT ON COLUMN public."KatDokumentyRodzaj".typedycji IS 'OkreÄąâ€şla pola ktÄ‚Ĺ‚re majĂ„â€¦ byĂ„â€ˇ wymagane w edycji, np data dokumentu, data waÄąÄ˝noÄąâ€şci itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".confidential IS 'Poufnosc';

