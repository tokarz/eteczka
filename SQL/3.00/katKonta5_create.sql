-- Table: public."KatKonta5"

-- DROP TABLE public."KatKonta5";

CREATE TABLE public."KatKonta5"
(
  konto5 character(20) NOT NULL, -- Symbol konta ksi�gowego
  nazwa character(150), -- Nazwa, np. Top Farms G�ubczyce
  idoper character(30), -- Idetyfikator osoby dokonuj�cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  firma character(20) NOT NULL, -- Symbol firmy w kt�rej zdefiniowano konto
  kontoskr character(20) NOT NULL, -- Skr�t konta
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  CONSTRAINT "KatKonta5_pkey" PRIMARY KEY (firma, konto5)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatKonta5"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatKonta5".konto5 IS 'Symbol konta ksi�gowego';
COMMENT ON COLUMN public."KatKonta5".nazwa IS 'Nazwa, np. Top Farms G�ubczyce';
COMMENT ON COLUMN public."KatKonta5".idoper IS 'Idetyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatKonta5".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatKonta5".firma IS 'Symbol firmy w kt�rej zdefiniowano konto';
COMMENT ON COLUMN public."KatKonta5".kontoskr IS 'Skr�t konta';

