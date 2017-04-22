-- Table: public."KatDzialy"

-- DROP TABLE public."KatDzialy";

CREATE TABLE public."KatDzialy"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator Dzialy : np. TFG, TFNI itp.
  nazwa character(150), -- Nazwa, np. Top Farms G³ubczyce
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatDzialy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDzialy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatDzialy".id IS 'Numer ID';
COMMENT ON COLUMN public."KatDzialy".symbol IS 'Identyfikator Dzialy : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatDzialy".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN public."KatDzialy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatDzialy".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatDzialy".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatDzialy".dataakcept IS 'Data akceptu przez idakcept';

