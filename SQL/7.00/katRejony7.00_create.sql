-- Table: public."KatRejony"

-- DROP TABLE public."KatRejony";

CREATE TABLE public."KatRejony"
(
  rejon character(20) NOT NULL, -- Identyfikator Rejony : np. DZ - DzbaĹ„ce, BG - Bogdanowice itp. w systemie pĹ‚acowym mam dwa znaki ale tu jest 20
  nazwa character(150), -- Nazwa, np. Top Farms GĂ‚Ĺ‚ubczyce
  idoper character(30), -- Idetyfikator osoby dokonujĂ‚Ä…cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujĂ‚Ä…cej
  firma character(20) NOT NULL, -- Symbol firmy do ktĂłrej naleĹĽy rejon
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  mnemonik character(20), -- Mnemonik, bo rejn nazwywa siÄ™ np. R1 a chcemy ĹĽebu to byĹ‚o bardziej czytelne, np BG - Bogdanowice
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  CONSTRAINT "KatRejony_pkey" PRIMARY KEY (firma, rejon)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatRejony"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatRejony".rejon IS 'Identyfikator Rejony : np. DZ - DzbaĹ„ce, BG - Bogdanowice itp. w systemie pĹ‚acowym mam dwa znaki ale tu jest 20';
COMMENT ON COLUMN public."KatRejony".nazwa IS 'Nazwa, np. Top Farms GĂ‚Ĺ‚ubczyce';
COMMENT ON COLUMN public."KatRejony".idoper IS 'Idetyfikator osoby dokonujĂ‚Ä…cej wpisu';
COMMENT ON COLUMN public."KatRejony".idakcept IS 'Identyfikator osoby akceptujĂ‚Ä…cej';
COMMENT ON COLUMN public."KatRejony".firma IS 'Symbol firmy do ktĂłrej naleĹĽy rejon';
COMMENT ON COLUMN public."KatRejony".mnemonik IS 'Mnemonik, bo rejn nazwywa siÄ™ np. R1 a chcemy ĹĽebu to byĹ‚o bardziej czytelne, np BG - Bogdanowice';

