--begin tran tran1

use Liaison
go

declare @admincorpsid int
declare @parentid int
declare @hsc_id int
declare @a_id int
declare @b_id int
declare @c_id int
declare @d_id int
declare @wpn_id int
declare @hsc_missionid int
declare @inf_missionid int
declare @wpn_missionid int
declare @baseid int
declare @battalion nvarchar(255)
declare @batord nvarchar (2)
declare @rgtord nvarchar (2)
declare @batnumb nvarchar(2)
declare @regnumb nvarchar(2)
declare @unitA nvarchar(1)
declare @unitB nvarchar(1)
declare @unitC nvarchar(1)
declare @unitD nvarchar(1)

set @admincorpsid = 1291
--set @battalion='____5th Bn., ___10th RRM'
set @batnumb='_3'
set @regnumb='_8'
set @batord = 'rd'
set @rgtord = 'th'
set @unitA='M'
set @unitB='I'
set @unitC='K'
set @unitD = 'L'
set @parentid=7981
---------------------
set @hsc_missionid=6508
set @inf_missionid=6509
set @wpn_missionid=6510
set @baseid=2287

DECLARE @OutputTbl TABLE (ID INT)

INSERT INTO Unit ( UseOrdinal, Letter, MissionName, CommandName, ServiceIdx, ServiceTypeIdx,  RankSymbol, AdminCorpsId, CanHide, Notes)
OUTPUT INSERTED.UnitId INTO @OutputTbl(ID)
VALUES 
(0, null, 'Headquarters & Service','___'+@batnumb+'th Bn., ___'+@regnumb+'th RRM', 4,1,'|', @admincorpsid, 1, null),
(0, @unitA, null, '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null),
(0, @unitB, null, '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null),
(0, @unitC, null, '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null),
(0, @unitD, null, '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null),
(0, null, 'Weapons', '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null)

--select id from @OutputTbl
--ORDER BY ID DESC

--select CONCAT(letter, missionname, commandname) from Unit where unitid in (select id from @OutputTbl )

set @hsc_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 1)
set @a_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 2)
set @b_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 3)
set @c_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 4)
set @d_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 5)
set @wpn_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 6)


insert into Relationship (RelTypeIdx, RelFromUnitId, RelToUnitId, DoNotUse)
select 1, @parentid, id,0 from @OutputTbl; 

--select RelTypeIdx, RelFromUnitId, RelToUnitId from relationship where reltounitid in (select id from @OutputTbl )

insert into unitindex( IndexCode, UnitId, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
values 
('RRM___'+@regnumb+'@___'+@batnumb+'|!', @hsc_id, 1, 1, 0, 0, 10),
('HSC-___'+@batnumb+'/___'+@regnumb+' RRM',@hsc_id, 0, 1, 0, 0, 20),
('HSC, ___'+@batnumb+@batord+' Bn., ___'+@regnumb+'th RRM',@hsc_id, 0, 1, 0, 0, 30),
('~USMC HSC-___'+@batnumb+'/___'+@regnumb+' MARINES',@hsc_id, 0, 1, 1, 0, 50),

('RRM___'+@regnumb+'@___'+@batnumb+'|'+@unitA,@a_id, 1, 1, 0, 0, 10),
(@unitA+'-___'+@batnumb+'/___'+@regnumb+' RRM',@a_id, 0, 1, 0, 0, 20),
(@unitA+' Coy., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@a_id, 0, 1, 0, 0, 30),
('~USMC '+@unitA+'-___'+@batnumb+'/___'+@regnumb+' MARINES',@a_id, 0, 1, 1, 0, 50),
 
('RRM___'+@regnumb+'@___'+@batnumb+'|'+@unitB,@b_id, 1, 1, 0, 0, 10),
(@unitB+'-___'+@batnumb+'/___'+@regnumb+' RRM',@b_id, 0, 1, 0, 0, 20),
(@unitB+' Coy., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@b_id, 0, 1, 0, 0, 30),
('~USMC '+@unitB+'-___'+@batnumb+'/___'+@regnumb+' MARINES',@b_id, 0, 1, 1, 0, 50),

('RRM___'+@regnumb+'@___'+@batnumb+'|'+@unitC,@c_id, 1, 1, 0, 0, 10),
(@unitC+'-___'+@batnumb+'/___'+@regnumb+' RRM',@c_id, 0, 1, 0, 0, 20),
(@unitC+' Coy., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@c_id, 0, 1, 0, 0, 30),
('~USMC '+@unitC+'-___'+@batnumb+'/___'+@regnumb+' MARINES',@c_id, 0, 1, 1, 0, 50),

('RRM___'+@regnumb+'@___'+@batnumb+'|'+@unitD,@d_id, 1, 1, 0, 0, 10),
(@unitD+'-___'+@batnumb+'/___'+@regnumb+' RRM',@d_id, 0, 1, 0, 0, 20),
(@unitD+' Coy., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@d_id, 0, 1, 0, 0, 30),
('~USMC '+@unitD+'-___'+@batnumb+'/___'+@regnumb+' MARINES',@d_id, 0, 1, 1, 0, 50),

('RRM___'+@regnumb+'@___'+@batnumb+'|X',@wpn_id, 1, 1, 0, 0, 10),
('WPNS-___'+@batnumb+'/___'+@regnumb+' RRM',@wpn_id, 0, 1, 0, 0, 20),
('Wpns. Coy., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@wpn_id, 0, 1, 0, 0, 30),
('~USMC WPNS-___'+@batnumb+'/___'+@regnumb+' MARINES',@wpn_id, 0, 1, 1, 0, 50)
--@d_id, 

insert into missionunit (missionid, unitid)
values (@hsc_missionid, @hsc_id),
(@inf_missionid, @a_id),
(@inf_missionid, @b_id),
(@inf_missionid, @c_id),
(@inf_missionid, @d_id),
(@wpn_missionid, @wpn_id)

insert into Tennant (Baseid, unitid, ishost)
values (@baseid, @hsc_id, 0),
(@baseid, @a_id, 0),
(@baseid, @b_id, 0),
(@baseid, @c_id, 0),
(@baseid, @d_id, 0),
(@baseid, @wpn_id, 0)


select Unit.UnitId, CONCAT(number, letter, missionname, commandname) , Mission.FullName, base.SortName  
from Unit 
inner join  missionunit on  unit.unitid = missionunit.unitid 
inner join mission on missionunit.missionid = mission.MissionId
inner join Tennant on unit.UnitId = Tennant.UnitId
inner join Base on tennant.BaseId = base.BaseId

where unit.Unitid = @parentid or unit.UnitId in (select id from @OutputTbl) 

select indexcode from unitindex where unitid in (@hsc_id, @a_id, @b_id, @c_id,@d_id, @wpn_id) order by UnitId, DisplayOrder

--rollback tran tran1