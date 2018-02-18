-- INSERT STATEMENTS

INSERT INTO "DbInfo"(wersja)
    VALUES ('9.30');


INSERT INTO "KatLoginy"(
            identyfikator, hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('Administrator', '21232f297a57a5a743894a0e4a801fc3', 'f6fdffe48c908deb0f4c3bd36c032e72',
            '2017-05-02 13:44:00', true, false);

INSERT INTO "KatLoginy"(
            identyfikator,  hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('Z.Tokarz', 'b87beb7419c847f18cf2e4dd101fe37e', '1c76fe13b0ac4d8caffb0b39578f2e78',
            '2017-05-02 13:44:00', false, false);

INSERT INTO "KatLoginy"(
            identyfikator,  hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('M.Tokarz', '207023ccb44feb4d7dadca005ce29a64', '3a660f8cfb3a1a674ce6715792f7de05',
            '2017-05-02 13:44:00', false, false);

INSERT INTO "KatLoginy"(
            identyfikator, hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('A.Tokarz', '207023ccb44feb4d7dadca005ce29a64', '3a660f8cfb3a1a674ce6715792f7de05',
            '2017-05-02 13:44:00', false, false);

INSERT INTO "KatLoginy"(
            identyfikator, hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('M.Skalacki', '207023ccb44feb4d7dadca005ce29a64', '3a660f8cfb3a1a674ce6715792f7de05',
            '2017-05-02 13:44:00', false, false);

---KatLoginyDetale
		INSERT INTO public."KatLoginyDetale"(
            identyfikator, nazwisko, imie, pocztaemail)
    VALUES ('Administrator', 'Admin', 'TF', 'poczta@poczta.pl');
	---KatLoginyDetale
		INSERT INTO public."KatLoginyDetale"(
            identyfikator, nazwisko, imie, pocztaemail)
    VALUES ('Z.Tokarz', 'Tokarz', 'Zbigniew', 'paszcz@poczta.pl');
	---KatLoginyDetale
		INSERT INTO public."KatLoginyDetale"(
            identyfikator, nazwisko, imie, pocztaemail)
    VALUES ('A.Tokarz', 'Tokarz', 'Aleksandra', 'burqin@poczta.pl');
	---KatLoginyDetale
		INSERT INTO public."KatLoginyDetale"(
            identyfikator, nazwisko, imie, pocztaemail)
    VALUES ('M.Skalacki', 'Skałacki', 'Michał', 'skala@poczta.pl');
	---KatLoginyDetale
		INSERT INTO public."KatLoginyDetale"(
            identyfikator, nazwisko, imie, pocztaemail)
    VALUES ('M.Tokarz', 'Tokarz', 'Maciej', 'toki@poczta.pl');
				
			
---KatLoginyFirmy


INSERT INTO "KatLoginyFirmy"(
            identyfikator,firma, rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('Z.Tokarz', 'AFM',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');


INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma ,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('M.Tokarz', 'AFM',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');

INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('A.Tokarz', 'AFM', 
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');

INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('M.Skalacki','AFM',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');



            --DRUGA FIRMA

            INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('Z.Tokarz', 'JAGROL', 
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');


INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('M.Tokarz', 'JAGROL',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');

INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('A.Tokarz', 'JAGROL',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');

INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('M.Skalacki', 'JAGROL', 
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');