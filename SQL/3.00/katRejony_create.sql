-- Table: public."KatRejony"

-- DROP TABLE public."KatRejony";

CREATE TABLE public."KatRejony"
(
  rejon character(20) NOT NULL, -- Identyfikator Rejony : np. DZ - Dzba�ce, BG - Bogdanowice itp. w systemie p�acowym mam dwa znaki ale tu jest 20
  nazwa character(150), -- Nazwa, np. Top Farms G³ubczyce
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  firma character(20) NOT NULL, -- Symbol firmy do kt�rej nale�y rejon
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  mnemonik character(20), -- Mnemonik, bo rejn nazwywa si� np. R1 a chcemy �ebu to by�o bardziej czytelne, np BG - Bogdanowice
  CONSTRAINT "KatRejony_pkey" PRIMARY KEY (firma, rejon)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatRejony"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatRejony".rejon IS 'Identyfikator Rejony : np. DZ - Dzba�ce, BG - Bogdanowice itp. w systemie p�acowym mam dwa znaki ale tu jest 20';
COMMENT ON COLUMN public."KatRejony".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN public."KatRejony".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatRejony".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatRejony".firma IS 'Symbol firmy do kt�rej nale�y rejon';
COMMENT ON COLUMN public."KatRejony".mnemonik IS 'Mnemonik, bo rejn nazwywa si� np. R1 a chcemy �ebu to by�o bardziej czytelne, np BG - Bogdanowice';

