-- Table: public."KatLokalPapier"

-- DROP TABLE public."KatLokalPapier";

CREATE TABLE public."KatLokalPapier"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbolfirma character(20), -- Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy
  symbol character(20), -- Symbol archiwum : np. TFG_piwnica
  nazwa character varying(300), -- Nazwa, np. archiwum Top Farms G³ubczyce
  ulica character(50), -- Ulica gdzie jest archiwum
  numerdomu character(10), -- Numer domu  gdzie jest archiwum
  numerlokalu character(10), -- Numer lokalu  gdzie jest archiwum
  miasto character(25), -- Miejscowoœæ  gdzie jest archiwum
  kodpocztowy character(6), -- Kod pocztowy  gdzie jest archiwum
  poczta character(25), -- Poczta  gdzie jest archiwum
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatLokalPapier_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLokalPapier"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatLokalPapier".id IS 'Numer ID';
COMMENT ON COLUMN public."KatLokalPapier".symbolfirma IS 'Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy';
COMMENT ON COLUMN public."KatLokalPapier".symbol IS 'Symbol archiwum : np. TFG_piwnica';
COMMENT ON COLUMN public."KatLokalPapier".nazwa IS 'Nazwa, np. archiwum Top Farms G³ubczyce';
COMMENT ON COLUMN public."KatLokalPapier".ulica IS 'Ulica gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerdomu IS 'Numer domu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerlokalu IS 'Numer lokalu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".miasto IS 'Miejscowoœæ  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".kodpocztowy IS 'Kod pocztowy  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".poczta IS 'Poczta  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatLokalPapier".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatLokalPapier".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatLokalPapier".dataakcept IS 'Data akceptu przez idakcept';

