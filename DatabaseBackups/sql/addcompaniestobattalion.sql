--begin tran tran1

use Liaison
go

declare @admincorpsid int
declare @parentid int
declare @hsc_id int
declare @a_id int
declare @b_id int
declare @c_id int
--declare @d_id int
--declare @wpn_id int
declare @hsc_missionid int
declare @inf_missionid int
--declare @wpn_missionid int
declare @baseid int
declare @battalion nvarchar(255)
declare @batord nvarchar (2)
declare @rgtord nvarchar (2)
declare @batnumb nvarchar(2)
declare @regnumb nvarchar(2)
declare @unitA nvarchar(1)
declare @unitB nvarchar(1)
declare @unitC nvarchar(1)
--declare @unitD nvarchar(1)

set @admincorpsid = 1293
--set @battalion='____5th Bn., ___10th RRM'
set @batnumb='_3'
set @regnumb='11'
set @batord = 'rd'
set @rgtord = 'th'
set @unitA='I'
set @unitB='K'
set @unitC='M'
--set @unitD = 'L'
set @parentid=17753
---------------------
set @hsc_missionid=5503
set @inf_missionid=5504
--set @wpn_missionid=6510
set @baseid=3352

DECLARE @OutputTbl TABLE (ID INT)

INSERT INTO Unit ( UseOrdinal, Letter, MissionName, CommandName, ServiceIdx, ServiceTypeIdx,  RankSymbol, AdminCorpsId, CanHide, Notes)
OUTPUT INSERTED.UnitId INTO @OutputTbl(ID)
VALUES 
(0, null, 'Headquarters & Service','___'+@batnumb+'th Bn., ___'+@regnumb+'th RRM', 4,1,'|', @admincorpsid, 1, null)
, (0, @unitA, null, '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null)
, (0, @unitB, null, '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null) 
, (0, @unitC, null, '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null)
--, (0, @unitD, null, '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null)
--, (0, null, 'Weapons', '___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',4,1,'|', @admincorpsid, 1, null)

--select id from @OutputTbl
--ORDER BY ID DESC

--select CONCAT(letter, missionname, commandname) from Unit where unitid in (select id from @OutputTbl )

set @hsc_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 1)
set @a_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 2)
set @b_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 3)
set @c_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 4)
--set @d_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 5)
--set @wpn_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 5)


insert into Relationship (RelTypeIdx, RelFromUnitId, RelToUnitId, DoNotUse)
select 1, @parentid, id,0 from @OutputTbl; 

--select RelTypeIdx, RelFromUnitId, RelToUnitId from relationship where reltounitid in (select id from @OutputTbl )

insert into unitindex( IndexCode, UnitId, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
values 
('RRM___'+@regnumb+'@___'+@batnumb+'|!', @hsc_id, 1, 1, 0, 0, 10),
('HSB-___'+@batnumb+'/___'+@regnumb+' RRM',@hsc_id, 0, 1, 0, 0, 20),
('HSB, ___'+@batnumb+@batord+' Bn., ___'+@regnumb+'th RRM',@hsc_id, 0, 1, 0, 0, 30),
('~USMC MARINES ___'+@regnumb+'/___'+@batnumb+ '-HSC',@hsc_id, 0, 1, 1, 0, 50),

('RRM___'+@regnumb+'|'+@unitA,@a_id, 1, 1, 0, 0, 10),
(@unitA+'-___'+@batnumb+'/___'+@regnumb+' RRM',@a_id, 0, 1, 0, 0, 20),
(@unitA+' Bty., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@a_id, 0, 1, 0, 0, 30),
('~USMC  MARINES ___'+@regnumb+'/___'+@batnumb+ '-A',@a_id, 0, 1, 1, 0, 50),
 
('RRM___'+@regnumb+'|'+@unitB,@b_id, 1, 1, 0, 0, 10),
(@unitB+'-___'+@batnumb+'/___'+@regnumb+' RRM',@b_id, 0, 1, 0, 0, 20),
(@unitB+' Bty., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@b_id, 0, 1, 0, 0, 30),
('~USMC MARINES ___'+@regnumb+'/___'+@batnumb+ '-B',@b_id, 0, 1, 1, 0, 50),

('RRM___'+@regnumb+'|'+@unitC,@c_id, 1, 1, 0, 0, 10),
(@unitC+'-___'+@batnumb+'/___'+@regnumb+' RRM',@c_id, 0, 1, 0, 0, 20),
(@unitC+' Bty., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@c_id, 0, 1, 0, 0, 30),
('~USMC MARINES ___'+@regnumb+'/___'+@batnumb+ '-C',@c_id, 0, 1, 1, 0, 50)

--, ('RRM___'+@regnumb+'@___'+@batnumb+'|'+@unitD,@d_id, 1, 1, 0, 0, 10),
--(@unitD+'-___'+@batnumb+'/___'+@regnumb+' RRM',@d_id, 0, 1, 0, 0, 20),
--(@unitD+' Coy., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@d_id, 0, 1, 0, 0, 30),
--('~USMC '+@unitD+'-___'+@batnumb+'/___'+@regnumb+' MARINES',@d_id, 0, 1, 1, 0, 50),

--('RRM___'+@regnumb+'@___'+@batnumb+'|X',@wpn_id, 1, 1, 0, 0, 10),
--('WPNS-___'+@batnumb+'/___'+@regnumb+' RRM',@wpn_id, 0, 1, 0, 0, 20),
--('Wpns. Coy., ___'+@batnumb+@batord+' Bn., ___'+@regnumb+@rgtord+' RRM',@wpn_id, 0, 1, 0, 0, 30),
--('~USMC MARINES ___'+@regnumb+'/___'+@batnumb+ '-WPNS',@wpn_id, 0, 1, 1, 0, 50)
--@d_id, 

insert into missionunit (missionid, unitid)
values (@hsc_missionid, @hsc_id)
, (@inf_missionid, @a_id)
, (@inf_missionid, @b_id)
, (@inf_missionid, @c_id)
--, (@inf_missionid, @d_id)
--, (@wpn_missionid, @wpn_id)

insert into Tennant (Baseid, unitid, ishost)
values (@baseid, @hsc_id, 0)
, (@baseid, @a_id, 0)
, (@baseid, @b_id, 0)
, (@baseid, @c_id, 0)
--, (@baseid, @d_id, 0)
--, (@baseid, @wpn_id, 0)


select Unit.UnitId, CONCAT(number, letter, missionname, commandname) , Mission.FullName, base.SortName  
from Unit 
inner join  missionunit on  unit.unitid = missionunit.unitid 
inner join mission on missionunit.missionid = mission.MissionId
inner join Tennant on unit.UnitId = Tennant.UnitId
inner join Base on tennant.BaseId = base.BaseId

where unit.Unitid = @parentid or unit.UnitId in (select id from @OutputTbl) 
--,@d_id, @wpn_id
select indexcode from unitindex where unitid in (@hsc_id, @a_id, @b_id, @c_id) order by UnitId, DisplayOrder

--rollback tran tran1