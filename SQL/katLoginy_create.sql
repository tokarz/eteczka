-- Table: public."KatLoginy"

-- DROP TABLE public."KatLoginy";

CREATE TABLE public."KatLoginy"
(
  id numeric(20,0) NOT NULL, -- Kolumna ID
  identyfikator character(30), -- Identyfikator - login
  nazwisko character(40), -- Nazwisko u�ytkownika
  imie character(20), -- Imi� u�ytkownika
  hasloshort character(50), -- Has�o kr�tkie minimum 6 znak�w do potwierdzania szybkiego
  haslolong character(50), -- Has�o d�ugie minimum 12 znak�w do logowania i operacji specjalnych
  rolareadonly boolean, -- Rola - uprawnienia tylko do odczytu
  rolaaddpracownik boolean, -- Rola - uprawnienia do dopisywania i modyfikacji danych pracownika
  rolamodifypracownik boolean, -- Rola - modyfikacja danych pracownika
  rolaaddfile boolean, -- Rola - dodanie pliku do systemu
  rolamodifyfile boolean, -- Rola - modyfikacja opisu pliku ju� istniej�cego w systemie
  rolaslowniki boolean, -- Rola - modyfikacja tabel s�ownikowych
  rolasendmail boolean, -- Rola - uprawnienie do wys�ania pliku mailem
  rolaraport boolean, -- Rola - uprawnienia do raport�w na drukar�
  rolaraportexport boolean, -- Rola uprawnienia do eksportu raport�w np. do xls
  roladoubleakcept boolean, -- Rola - uprawnienia do podw�jnego akceptu
  datamodify time with time zone, -- Data modyfikacji tabeli uprawnie�
  CONSTRAINT "KatLoginy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginy"
  OWNER TO postgres;
COMMENT ON TABLE public."KatLoginy"
  IS 'Katalog u�ytkownik�w systemu';
COMMENT ON COLUMN public."KatLoginy".id IS 'Kolumna ID';
COMMENT ON COLUMN public."KatLoginy".identyfikator IS 'Identyfikator - login';
COMMENT ON COLUMN public."KatLoginy".nazwisko IS 'Nazwisko u�ytkownika';
COMMENT ON COLUMN public."KatLoginy".imie IS 'Imi� u�ytkownika';
COMMENT ON COLUMN public."KatLoginy".hasloshort IS 'Has�o kr�tkie minimum 6 znak�w do potwierdzania szybkiego';
COMMENT ON COLUMN public."KatLoginy".haslolong IS 'Has�o d�ugie minimum 12 znak�w do logowania i operacji specjalnych';
COMMENT ON COLUMN public."KatLoginy".rolareadonly IS 'Rola - uprawnienia tylko do odczytu';
COMMENT ON COLUMN public."KatLoginy".rolaaddpracownik IS 'Rola - uprawnienia do dopisywania i modyfikacji danych pracownika';
COMMENT ON COLUMN public."KatLoginy".rolamodifypracownik IS 'Rola - modyfikacja danych pracownika';
COMMENT ON COLUMN public."KatLoginy".rolaaddfile IS 'Rola - dodanie pliku do systemu';
COMMENT ON COLUMN public."KatLoginy".rolamodifyfile IS 'Rola - modyfikacja opisu pliku ju� istniej�cego w systemie';
COMMENT ON COLUMN public."KatLoginy".rolaslowniki IS 'Rola - modyfikacja tabel s�ownikowych';
COMMENT ON COLUMN public."KatLoginy".rolasendmail IS 'Rola - uprawnienie do wys�ania pliku mailem';
COMMENT ON COLUMN public."KatLoginy".rolaraport IS 'Rola - uprawnienia do raport�w na drukar�';
COMMENT ON COLUMN public."KatLoginy".rolaraportexport IS 'Rola uprawnienia do eksportu raport�w np. do xls';
COMMENT ON COLUMN public."KatLoginy".roladoubleakcept IS 'Rola - uprawnienia do podw�jnego akceptu';
COMMENT ON COLUMN public."KatLoginy".datamodify IS 'Data modyfikacji tabeli uprawnie�';

