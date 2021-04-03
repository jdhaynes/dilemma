INSERT INTO topic (id, name)
VALUES ('6f572c7d-a45d-46f0-ac4b-6c1281d63325', 'Male fashion'),
	   ('031e2c12-e05f-41d7-b43a-b787e9452186', 'Female fashion'),
	   ('f2102dd1-9a1a-4b45-9c1f-750b50b1b021', 'Food and drink');

INSERT INTO poster (id, dob)
VALUES ('10e02481-52ed-4de8-a806-719e07d0662b', '1993-06-15'),
	   ('ac1156cd-9d34-49ce-adcc-8bac324e0400', '1989-02-06');

INSERT INTO dilemma (id, topic_id, question, posted_date, is_withdrawn, withdrawn_date, poster_id)
VALUES ('7cc8b887-5461-4e26-901d-d87a7774a499', '6f572c7d-a45d-46f0-ac4b-6c1281d63325', 'Test question 1',
		'2021-04-03 22:35', FALSE, NULL, 'ac1156cd-9d34-49ce-adcc-8bac324e0400'),
	   ('732f5822-488c-44c0-bbe3-7213fcacc526', 'f2102dd1-9a1a-4b45-9c1f-750b50b1b021', 'Test question 2',
		'2021-03-02 14:21', FALSE, NULL, '10e02481-52ed-4de8-a806-719e07d0662b');

INSERT INTO option (id, description, dilemma_id, image_object_id)
VALUES ('7a9d4c9e-da67-4c38-8f29-dbaf3522bbb5', 'Dilemma 1 Option 1', '7cc8b887-5461-4e26-901d-d87a7774a499',
		'de6f11e7-d9a2-4f8c-8f38-55a0429570fc'),
	   ('12de71fb-3d65-4494-ba2f-da8075049c15', 'Dilemma 1 Option 2', '7cc8b887-5461-4e26-901d-d87a7774a499',
		'458faadb-b2fd-48af-bb18-7de8ebad1955'),
	   ('17bcd760-6526-460d-9606-b20cd7c113f7', 'Dilemma 1 Option 3', '7cc8b887-5461-4e26-901d-d87a7774a499',
		'fc1be087-3104-45cb-9c22-325b072603fb'),
	   ('239842d9-187b-4154-812f-43dda40534fb', 'Dilemma 2 Option 1', '732f5822-488c-44c0-bbe3-7213fcacc526',
		'77c7abd1-cf9d-4688-8f06-11772ba86648'),
	   ('3d13668f-859f-482c-9cc0-a135c590f7e8', 'Dilemma 2 Option 2', '732f5822-488c-44c0-bbe3-7213fcacc526',
		'77c7abd1-cf9d-4688-8f06-11772ba86648')