insert into "MiejscePracy" 
(numeread, datapocz, datakoniec, firma,rejon,wydzial,podwydzial, konto5, 
			idoper, datamodify, idakcept, dataakcept,
			systembazowy, usuniety, id )
values
('Paszczak','2015-04-01', '9999-12-31', 'TFW','02','02', '17', '12R1170',
			'Z.Tokarz', '2017-08-26 08:12', 'To³di', '2017-09-26 08:12',
			'EAD', false, nextval('miejscepracy_id_seq'))
