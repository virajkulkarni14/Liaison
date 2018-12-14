--declare @bnnumb nvarchar(3)
--declare @unitid int

--set @bnnumb = '230'
--set @unitid = 3374

create table #temp3
(
    batnumb nvarchar(255),
	unitid int

)
insert into #temp3 (batnumb, unitid) VALUES 
('231', 3375),
('233', 3377),
('234', 3378),
('235', 3379),
('236', 3371),
('237', 3372),
('238', 3373),
('777', 3367),
('888', 3368),
('999', 3369),
('239', 3386),
('240', 3387),
('241', 3388),
('242', 3389),
('243', 3390),
('244', 3391),
('515', 3364),
('616', 3365),
('818', 3366),
('245', 3383),
('246', 3384),
('247', 3385)

insert into UnitIndex (Indexcode, unitid, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
select
    'SF@__'+batnumb, unitid, 1, 1, 0, 0, 10
from
    #temp3

insert into UnitIndex (Indexcode, unitid, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
select
    '__'+batnumb+' SF BN', unitid, 0, 1, 0, 0, 20
from
    #temp3

insert into UnitIndex (Indexcode, unitid, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
select
    '__'+batnumb+' SF Bn.', unitid, 0, 1, 0, 0, 30
from
    #temp3

--insert into UnitIndex (Indexcode, unitid, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
--Values 
--('SF@__'+@bnnumb, @unitid, 1, 1, 0, 0, 10),
--('__'+@bnnumb+' SF BN', @unitid, 0, 1, 0, 0, 20),
--('__'+@bnnumb+' SF Bn.', @unitid, 0, 1, 0, 0, 30)
