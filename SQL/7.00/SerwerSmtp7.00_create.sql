-- Table: public."SerwerSmtp"

-- DROP TABLE public."SerwerSmtp";

CREATE TABLE public."SerwerSmtp"
(
  smtpserwer character(100) NOT NULL, -- Nazwa serwera
  mailusername character(100), -- Np. kadry@poczta.pl
  mailpassword character(100), -- Has這 do poczty
  mailsender character(100), -- Np. kadry@poczta.pl
  mailsubject character varying(300), -- Naglowek
  mailbody character varying(300), -- Tresc maila
  pdfmasterpassword character(100), -- Has這 administratora do pdf
  datamodify timestamp without time zone,
  smtpport numeric(5,0), -- Numer portu
  CONSTRAINT "SerwerSmtp_pkey" PRIMARY KEY (smtpserwer)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."SerwerSmtp"
  OWNER TO postgres;
COMMENT ON COLUMN public."SerwerSmtp".smtpserwer IS 'Nazwa serwera';
COMMENT ON COLUMN public."SerwerSmtp".mailusername IS 'Np. kadry@poczta.pl';
COMMENT ON COLUMN public."SerwerSmtp".mailpassword IS 'Has這 do poczty';
COMMENT ON COLUMN public."SerwerSmtp".mailsender IS 'Np. kadry@poczta.pl';
COMMENT ON COLUMN public."SerwerSmtp".mailsubject IS 'Naglowek';
COMMENT ON COLUMN public."SerwerSmtp".mailbody IS 'Tresc maila';
COMMENT ON COLUMN public."SerwerSmtp".pdfmasterpassword IS 'Has這 administratora do pdf';
COMMENT ON COLUMN public."SerwerSmtp".smtpport IS 'Numer portu';

