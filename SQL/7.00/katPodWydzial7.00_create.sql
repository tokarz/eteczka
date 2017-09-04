-- Table: public."KatPodWydzial"

-- DROP TABLE public."KatPodWydzial";

CREATE TABLE public."KatPodWydzial"
(
  podwydzial character(20) NOT NULL, -- Podwydzia³
  nazwa character(150), -- Nazwa, np. Rachuba
  wydzial character(20) NOT NULL, -- Wydzia³ do którego nale¿y podwydzia³
  datamodify timestamp without time zone, -- Data modyfikacji
  idoper character(30), -- Id uzytkownika
  idakcept character(30), -- Identyfikator osoby akcept
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Firma klucz to : firma+wydzial+podwydzial
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  CONSTRAINT "KatPodWydzial_pkey" PRIMARY KEY (firma, wydzial, podwydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPodWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPodWydzial".podwydzial IS 'Podwydzia³';
COMMENT ON COLUMN public."KatPodWydzial".nazwa IS 'Nazwa, np. Rachuba';
COMMENT ON COLUMN public."KatPodWydzial".wydzial IS 'Wydzia³ do którego nale¿y podwydzia³';
COMMENT ON COLUMN public."KatPodWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatPodWydzial".idoper IS 'Id uzytkownika';
COMMENT ON COLUMN public."KatPodWydzial".idakcept IS 'Identyfikator osoby akcept';
COMMENT ON COLUMN public."KatPodWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatPodWydzial".firma IS 'Firma klucz to : firma+wydzial+podwydzial';

