-- Table: public."KatLoginy"

-- DROP TABLE public."KatLoginy";

CREATE TABLE public."KatLoginy"
(
  identyfikator character(30) NOT NULL, -- Identyfikator - login
  nazwisko character(40), -- Nazwisko u�ytkownika
  imie character(20), -- Imi� u�ytkownika
  hasloshort character(50), -- Has�o kr�tkie
  haslolong character(50), -- Has�o do logowania
  datamodify time without time zone,
  isadmin boolean,
  usuniety boolean,
  pocztaemail character(30), -- E-mail u�ytkownika na wszelki wypadek
  CONSTRAINT "KatLoginy_pkey" PRIMARY KEY (identyfikator)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginy"
  OWNER TO postgres;
COMMENT ON TABLE public."KatLoginy"
  IS 'Katalog uÂżytkownikĂłw systemu';
COMMENT ON COLUMN public."KatLoginy".identyfikator IS 'Identyfikator - login';
COMMENT ON COLUMN public."KatLoginy".nazwisko IS 'Nazwisko u�ytkownika';
COMMENT ON COLUMN public."KatLoginy".imie IS 'Imi� u�ytkownika';
COMMENT ON COLUMN public."KatLoginy".hasloshort IS 'Has�o kr�tkie';
COMMENT ON COLUMN public."KatLoginy".haslolong IS 'Has�o do logowania';
COMMENT ON COLUMN public."KatLoginy".pocztaemail IS 'E-mail u�ytkownika na wszelki wypadek';

