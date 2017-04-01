-- Table: public."KatLoginy"

-- DROP TABLE public."KatLoginy";

CREATE TABLE public."KatLoginy"
(
  id numeric(20,0) NOT NULL, -- Kolumna ID
  identyfikator character(30), -- Identyfikator - login
  nazwisko character(40), -- Nazwisko u¿ytkownika
  imie character(20), -- Imiê u¿ytkownika
  hasloshort character(50), -- Has³o krótkie minimum 6 znaków do potwierdzania szybkiego
  haslolong character(50), -- Has³o d³ugie minimum 12 znaków do logowania i operacji specjalnych
  rolareadonly boolean, -- Rola - uprawnienia tylko do odczytu
  rolaaddpracownik boolean, -- Rola - uprawnienia do dopisywania i modyfikacji danych pracownika
  rolamodifypracownik boolean, -- Rola - modyfikacja danych pracownika
  rolaaddfile boolean, -- Rola - dodanie pliku do systemu
  rolamodifyfile boolean, -- Rola - modyfikacja opisu pliku ju¿ istniej¹cego w systemie
  rolaslowniki boolean, -- Rola - modyfikacja tabel s³ownikowych
  rolasendmail boolean, -- Rola - uprawnienie do wys³ania pliku mailem
  rolaraport boolean, -- Rola - uprawnienia do raportów na drukarê
  rolaraportexport boolean, -- Rola uprawnienia do eksportu raportów np. do xls
  roladoubleakcept boolean, -- Rola - uprawnienia do podwójnego akceptu
  datamodify time with time zone, -- Data modyfikacji tabeli uprawnieñ
  CONSTRAINT "KatLoginy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginy"
  OWNER TO postgres;
COMMENT ON TABLE public."KatLoginy"
  IS 'Katalog u¿ytkowników systemu';
COMMENT ON COLUMN public."KatLoginy".id IS 'Kolumna ID';
COMMENT ON COLUMN public."KatLoginy".identyfikator IS 'Identyfikator - login';
COMMENT ON COLUMN public."KatLoginy".nazwisko IS 'Nazwisko u¿ytkownika';
COMMENT ON COLUMN public."KatLoginy".imie IS 'Imiê u¿ytkownika';
COMMENT ON COLUMN public."KatLoginy".hasloshort IS 'Has³o krótkie minimum 6 znaków do potwierdzania szybkiego';
COMMENT ON COLUMN public."KatLoginy".haslolong IS 'Has³o d³ugie minimum 12 znaków do logowania i operacji specjalnych';
COMMENT ON COLUMN public."KatLoginy".rolareadonly IS 'Rola - uprawnienia tylko do odczytu';
COMMENT ON COLUMN public."KatLoginy".rolaaddpracownik IS 'Rola - uprawnienia do dopisywania i modyfikacji danych pracownika';
COMMENT ON COLUMN public."KatLoginy".rolamodifypracownik IS 'Rola - modyfikacja danych pracownika';
COMMENT ON COLUMN public."KatLoginy".rolaaddfile IS 'Rola - dodanie pliku do systemu';
COMMENT ON COLUMN public."KatLoginy".rolamodifyfile IS 'Rola - modyfikacja opisu pliku ju¿ istniej¹cego w systemie';
COMMENT ON COLUMN public."KatLoginy".rolaslowniki IS 'Rola - modyfikacja tabel s³ownikowych';
COMMENT ON COLUMN public."KatLoginy".rolasendmail IS 'Rola - uprawnienie do wys³ania pliku mailem';
COMMENT ON COLUMN public."KatLoginy".rolaraport IS 'Rola - uprawnienia do raportów na drukarê';
COMMENT ON COLUMN public."KatLoginy".rolaraportexport IS 'Rola uprawnienia do eksportu raportów np. do xls';
COMMENT ON COLUMN public."KatLoginy".roladoubleakcept IS 'Rola - uprawnienia do podwójnego akceptu';
COMMENT ON COLUMN public."KatLoginy".datamodify IS 'Data modyfikacji tabeli uprawnieñ';

