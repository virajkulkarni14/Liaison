--declare @bnnumb nvarchar(3)
--declare @unitid int

--set @bnnumb = '230'
--set @unitid = 3374

use liaison 
go

create table #temp3
(
    batnumb nvarchar(255),
	unitid int

)
insert into #temp3 (batnumb, unitid) VALUES 

('230', 3374),
('231', 3375),
('232', 3376),
('233', 3377)

insert into UnitIndex (Indexcode, unitid, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
select
    'EGYPT@__'+batnumb, unitid, 1, 1, 0, 0, 10
from
    #temp3

