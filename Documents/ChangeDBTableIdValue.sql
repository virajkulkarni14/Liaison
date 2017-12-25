set identity_insert MyTable ON
DBCC CHECKIDENT ('MyTable', RESEED, 17);
set identity_insert MyTable OFF