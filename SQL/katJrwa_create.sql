-- Table: public."KatJrwa"

-- DROP TABLE public."KatJrwa";

CREATE TABLE public."KatJrwa"
(
  id numeric(20,0) NOT NULL, -- Identyfikator ID
  slklas1 character(1), -- Symbol klasyfikacyjny I
  slklas2 character(2), -- Symbol klasyfikacyjny II
  slklas3 character(3), -- Symbol klasyfikacyjny III
  slklas4 character(4), -- Symbol klasyfikacyjny IV
  kategoria character(5), -- kategoria wed³ug s³ownika KategorieAkt
  nazwa character(100),
  opis character varying(300), -- Opis szczegó³owy
  idoper character(30), -- Identyfikator osoby dokonuj¹cej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatJrwa_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatJrwa"
  OWNER TO postgres;
COMMENT ON TABLE public."KatJrwa"
  IS 'Jednolity Rzeczowy WykazAkt';
COMMENT ON COLUMN public."KatJrwa".id IS 'Identyfikator ID';
COMMENT ON COLUMN public."KatJrwa".slklas1 IS 'Symbol klasyfikacyjny I';
COMMENT ON COLUMN public."KatJrwa".slklas2 IS 'Symbol klasyfikacyjny II';
COMMENT ON COLUMN public."KatJrwa".slklas3 IS 'Symbol klasyfikacyjny III';
COMMENT ON COLUMN public."KatJrwa".slklas4 IS 'Symbol klasyfikacyjny IV';
COMMENT ON COLUMN public."KatJrwa".kategoria IS 'kategoria wed³ug s³ownika KategorieAkt';
COMMENT ON COLUMN public."KatJrwa".nazwa IS 'Opis klasyfikacyjny';
COMMENT ON COLUMN public."KatJrwa".opis IS 'Opis szczegó³owy';
COMMENT ON COLUMN public."KatJrwa".idoper IS 'Identyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatJrwa".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN public."KatJrwa".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatJrwa".dataakcept IS 'Data akceptu przez idakcept';


