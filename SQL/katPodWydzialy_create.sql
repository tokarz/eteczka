-- Table: public."KatPodWydzialy"

-- DROP TABLE public."KatPodWydzialy";

CREATE TABLE public."KatPodWydzialy"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator podwydzia³u : np. KDR (Kadry - p³ace).
  nazwa character(150), -- Nazwa, np. Top Farms G³ubczyce
  symboldzialy character(20), -- Identyfikator dzia³u nadrzêdnego : np. FA (tabela KatDzialy). Podwydzial KDR nale¿y do dzia³u FA
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatPodDzialy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPodWydzialy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPodWydzialy".id IS 'Numer ID';
COMMENT ON COLUMN public."KatPodWydzialy".symbol IS 'Identyfikator podwydzia³u : np. KDR (Kadry - p³ace).';
COMMENT ON COLUMN public."KatPodWydzialy".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN public."KatPodWydzialy".symboldzialy IS 'Identyfikator dzia³u nadrzêdnego : np. FA (tabela KatDzialy). Podwydzial KDR nale¿y do dzia³u FA';
COMMENT ON COLUMN public."KatPodWydzialy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatPodWydzialy".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatPodWydzialy".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatPodWydzialy".dataakcept IS 'Data akceptu przez idakcept';

