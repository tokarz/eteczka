-- Table: public."MiejscePracy"

-- DROP TABLE public."MiejscePracy";

CREATE TABLE public."MiejscePracy"
(
  pesel character(11) NOT NULL, -- Pesel pracownika
  firma character(20), -- Symbol firmy
  rejon character(20), -- Symbol rejonu w ramach firmy
  wydzial character(20), -- Symbol dzia³u
  podwydzial character(20), -- Symbol podwydzia³u
  konto5 character(20), -- Symbol konta ksiêgowego
  datapocz character(10) NOT NULL, -- Data pocz¹tkowa w miejscu pracy
  datakoniec character(10), -- Data koñcowa w miejscu pracy
  idoper character(30), -- ID operatora
  datamodify time without time zone, -- Data operacji
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time without time zone, -- Data akceptu
  CONSTRAINT "MiejscePracy_pkey" PRIMARY KEY (pesel, datapocz)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."MiejscePracy"
  OWNER TO postgres;
COMMENT ON TABLE public."MiejscePracy"
  IS 'Tabela ';
COMMENT ON COLUMN public."MiejscePracy".pesel IS 'Pesel pracownika';
COMMENT ON COLUMN public."MiejscePracy".firma IS 'Symbol firmy';
COMMENT ON COLUMN public."MiejscePracy".rejon IS 'Symbol rejonu w ramach firmy';
COMMENT ON COLUMN public."MiejscePracy".wydzial IS 'Symbol dzia³u';
COMMENT ON COLUMN public."MiejscePracy".podwydzial IS 'Symbol podwydzia³u';
COMMENT ON COLUMN public."MiejscePracy".konto5 IS 'Symbol konta ksiêgowego';
COMMENT ON COLUMN public."MiejscePracy".datapocz IS 'Data pocz¹tkowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".datakoniec IS 'Data koñcowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".idoper IS 'ID operatora';
COMMENT ON COLUMN public."MiejscePracy".datamodify IS 'Data operacji';
COMMENT ON COLUMN public."MiejscePracy".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."MiejscePracy".dataakcept IS 'Data akceptu';

