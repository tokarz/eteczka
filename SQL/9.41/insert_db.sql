-- Database: "E-Agropin-EAD"
-- Insert do tabeli smtp; 


  \connect E-Agropin-EAD



-- INSERT STATEMENTS



--INSERT INTO "SerwerSmtp"(
--            smtpserwer, mailusername, mailpassword,
--            mailsender, mailsubject, mailbody, smtpport, datamodify)
--    VALUES ('smtp-topfarms.ogicom.pl', 'kadry.cuw@topfarms.pl', '29WRYIPeszedc29',
--	    'kadry.cuw@topfarms.pl','CUW email','Skany dokumentów',25,
--            '2017-05-02 13:44:00');

INSERT INTO "SerwerSmtp"(
            smtpserwer, mailusername, mailpassword,
            mailsender, mailsubject, mailbody, smtpport, datamodify)
    VALUES ('smtp.gmail.com', 'mmtpaszcz@gmail.com', '#ksglmp#',
	    'mmtpaszcz@gmail.com','CUW email','Skany dokumentów',465,
            '2017-05-02 13:44:00');


--smtp-topfarms.ogicom.pl 
