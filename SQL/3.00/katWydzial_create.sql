-- Table: public."KatWydzial"

-- DROP TABLE public."KatWydzial";

CREATE TABLE public."KatWydzial"
(
  wydzial character(20) NOT NULL, -- Identyfikator Dzialu : np. KSG - ksi�gowo��, TRAK - traktorzy�ci itp.
  nazwa character(150), -- Nazwa, np. Administracja, Traktorzy�ci itp.
  datamodify time without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Symbol firmydo kt�rej przypisano dzia�.
  CONSTRAINT firmawydzial PRIMARY KEY (firma, wydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatWydzial".wydzial IS 'Identyfikator Dzialu : np. KSG - ksi�gowo��, TRAK - traktorzy�ci itp.';
COMMENT ON COLUMN public."KatWydzial".nazwa IS 'Nazwa, np. Administracja, Traktorzy�ci itp.';
COMMENT ON COLUMN public."KatWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatWydzial".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatWydzial".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatWydzial".firma IS 'Symbol firmydo kt�rej przypisano dzia�.';

