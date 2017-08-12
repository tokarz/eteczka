-- Table: public."KatRejony"

-- DROP TABLE public."KatRejony";

CREATE TABLE public."KatRejony"
(
  rejon character(20) NOT NULL, -- Identyfikator Rejony : np. DZ - Dzbañce, BG - Bogdanowice itp. w systemie p³acowym mam dwa znaki ale tu jest 20
  nazwa character(150), -- Nazwa, np. Top Farms GÂ³ubczyce
  idoper character(30), -- Idetyfikator osoby dokonujÂ¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂ¹cej
  firma character(20) NOT NULL, -- Symbol firmy do której nale¿y rejon
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  mnemonik character(20), -- Mnemonik, bo rejn nazwywa siê np. R1 a chcemy ¿ebu to by³o bardziej czytelne, np BG - Bogdanowice
  CONSTRAINT "KatRejony_pkey" PRIMARY KEY (firma, rejon)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatRejony"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatRejony".rejon IS 'Identyfikator Rejony : np. DZ - Dzbañce, BG - Bogdanowice itp. w systemie p³acowym mam dwa znaki ale tu jest 20';
COMMENT ON COLUMN public."KatRejony".nazwa IS 'Nazwa, np. Top Farms GÂ³ubczyce';
COMMENT ON COLUMN public."KatRejony".idoper IS 'Idetyfikator osoby dokonujÂ¹cej wpisu';
COMMENT ON COLUMN public."KatRejony".idakcept IS 'Identyfikator osoby akceptujÂ¹cej';
COMMENT ON COLUMN public."KatRejony".firma IS 'Symbol firmy do której nale¿y rejon';
COMMENT ON COLUMN public."KatRejony".mnemonik IS 'Mnemonik, bo rejn nazwywa siê np. R1 a chcemy ¿ebu to by³o bardziej czytelne, np BG - Bogdanowice';

