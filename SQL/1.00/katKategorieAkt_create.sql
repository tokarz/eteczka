-- Table: public."KatKategorieAkt"

-- DROP TABLE public."KatKategorieAkt";

CREATE TABLE public."KatKategorieAkt"
(
  id numeric(15,0) NOT NULL, -- Numer Id
  symbol character(20), -- kategorie typu A,B,BC,BE + ewentualne cyfry
  nazwa character(100), -- Nazwa kategorii
  idoper character(30), -- Identyfikator osoby dokonuj�cej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatKategorieAkt_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatKategorieAkt"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatKategorieAkt".id IS 'Numer Id';
COMMENT ON COLUMN public."KatKategorieAkt".symbol IS 'kategorie typu A,B,BC,BE + ewentualne cyfry';
COMMENT ON COLUMN public."KatKategorieAkt".nazwa IS 'Nazwa kategorii';
COMMENT ON COLUMN public."KatKategorieAkt".idoper IS 'Identyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatKategorieAkt".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN public."KatKategorieAkt".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatKategorieAkt".dataakcept IS 'Data akceptu przez idakcept';


