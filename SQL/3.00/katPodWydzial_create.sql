-- Table: public."KatPodWydzial"

-- DROP TABLE public."KatPodWydzial";

CREATE TABLE public."KatPodWydzial"
(
  podwydzial character(20) NOT NULL, -- Identyfikator podwydzia³u : np. KDR (Kadry - p³ace).
  nazwa character(150), -- Nazwa, np. Rachuba
  wydzial character(20) NOT NULL, -- Identyfikator dzia³u nadrzêdnego : np. FA (tabela KatDzialy). Podwydzial KDR nale¿y do dzia³u FA
  datamodify time without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Symbol firmy, pe�ny klucz to : firma+wydzial+podwydzial
  CONSTRAINT frimawydzialpodwydzial PRIMARY KEY (firma, wydzial, podwydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPodWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPodWydzial".podwydzial IS 'Identyfikator podwydzia³u : np. KDR (Kadry - p³ace).';
COMMENT ON COLUMN public."KatPodWydzial".nazwa IS 'Nazwa, np. Rachuba';
COMMENT ON COLUMN public."KatPodWydzial".wydzial IS 'Identyfikator dzia³u nadrzêdnego : np. FA (tabela KatDzialy). Podwydzial KDR nale¿y do dzia³u FA';
COMMENT ON COLUMN public."KatPodWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatPodWydzial".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatPodWydzial".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatPodWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatPodWydzial".firma IS 'Symbol firmy, pe�ny klucz to : firma+wydzial+podwydzial';

