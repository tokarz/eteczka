-- Table: public."KatDokumentyRodzaj"

-- DROP TABLE public."KatDokumentyRodzaj";

CREATE TABLE public."KatDokumentyRodzaj"
(
  id numeric(10,0) NOT NULL, -- Numer id
  symbol character(20), -- Symbol dokumetu, np. SWPR - świadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  CONSTRAINT "KatDokumentyRodzaj_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDokumentyRodzaj"
  OWNER TO postgres;
COMMENT ON TABLE public."KatDokumentyRodzaj"
  IS 'Słownik rodzajów dokumentów, np. BDL - badania lekarskie, SWPR - świadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".id IS 'Numer id';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - świadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';

