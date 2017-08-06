-- Table: public."KatLokalPapier"

-- DROP TABLE public."KatLokalPapier";

CREATE TABLE public."KatLokalPapier"
(
  firma character(20) NOT NULL, -- Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy
  lokalpapier character(20) NOT NULL, -- Symbol archiwum : np. TFG_piwnica
  nazwa character varying(300), -- Nazwa, np. archiwum Top Farms GÂłubczyce
  ulica character(50), -- Ulica gdzie jest archiwum
  numerdomu character(10), -- Numer domu  gdzie jest archiwum
  numerlokalu character(10), -- Numer lokalu  gdzie jest archiwum
  miasto character(25), -- MiejscowoĹ“Ă¦  gdzie jest archiwum
  kodpocztowy character(6), -- Kod pocztowy  gdzie jest archiwum
  poczta character(25), -- Poczta  gdzie jest archiwum
  idoper character(30), -- Idetyfikator osoby dokonujÂącej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂącej
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  CONSTRAINT "KatLokalPapier_pkey" PRIMARY KEY (firma, lokalpapier)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLokalPapier"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatLokalPapier".firma IS 'Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy';
COMMENT ON COLUMN public."KatLokalPapier".lokalpapier IS 'Symbol archiwum : np. TFG_piwnica';
COMMENT ON COLUMN public."KatLokalPapier".nazwa IS 'Nazwa, np. archiwum Top Farms GÂłubczyce';
COMMENT ON COLUMN public."KatLokalPapier".ulica IS 'Ulica gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerdomu IS 'Numer domu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerlokalu IS 'Numer lokalu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".miasto IS 'MiejscowoĹ“Ă¦  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".kodpocztowy IS 'Kod pocztowy  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".poczta IS 'Poczta  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".idoper IS 'Idetyfikator osoby dokonujÂącej wpisu';
COMMENT ON COLUMN public."KatLokalPapier".idakcept IS 'Identyfikator osoby akceptujÂącej';

