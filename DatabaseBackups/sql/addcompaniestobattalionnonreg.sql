begin tran tran1

use Liaison
go

declare @admincorpsid int
declare @parentid int
declare @hsc_id int
declare @a_id int
declare @b_id int
declare @c_id int
--declare @d_id int
--declare @e_id int
--declare @f_id int
--declare @wpn_id int
--declare @spt_id int
declare @hsc_missionid int
declare @inf_missionid int
--declare @wpn_missionid int
--declare @spt_missionid int
declare @baseid int
declare @battalion nvarchar(255)
declare @batord nvarchar (6)
--declare @rgtord nvarchar (2)
declare @batnumb nvarchar(2)
declare @battext nvarchar(255)
declare @batcode nvarchar(15)
--declare @regnumb nvarchar(2)
declare @unitA nvarchar(1)
declare @unitB nvarchar(1)
declare @unitC nvarchar(1)
--declare @unitD nvarchar(1)
--declare @unitE nvarchar(1)
--declare @unitF nvarchar(1)

set @admincorpsid = 3279
--set @battalion='____5th Bn., ___10th RRM'
set @batnumb='_3'
set @battext = 'Cbt. Engr.'
set @batcode = 'CE'
--set @regnumb='_8'
set @batord = 'rd'
--set @rgtord = 'th'
set @unitA='A'
set @unitB='B'
set @unitC='C'
--set @unitD='D'
--set @unitE='E'
--set @unitF='F'
set @parentid=9194
---------------------
set @hsc_missionid=7647
set @inf_missionid=7646
--set @wpn_missionid=7648
--set @spt_missionid=7649
set @baseid=3352

DECLARE @OutputTbl TABLE (ID INT)

INSERT INTO Unit ( UseOrdinal, Letter, MissionName, CommandName, ServiceIdx, ServiceTypeIdx,  RankSymbol, AdminCorpsId, CanHide, Notes)
OUTPUT INSERTED.UnitId INTO @OutputTbl(ID)
VALUES 
(0, null, 'Headquarters & Service','___'+@batnumb+ @batord+' ' +@battext+ ' Bn., RM', 4,1,'|', @admincorpsid, 1, null)
, (0, @unitA, null, '___'+@batnumb+ @batord+ ' ' +@battext+ ' Bn., RM',4,1,'|', @admincorpsid, 1, null)
, (0, @unitB, null, '___'+@batnumb+ @batord+ ' ' +@battext+ ' Bn., RM',4,1,'|', @admincorpsid, 1, null)
, (0, @unitC, null, '___'+@batnumb+ @batord+ ' ' +@battext+ ' Bn., RM',4,1,'|', @admincorpsid, 1, null)
--, (0, @unitD, null, '___'+@batnumb+ @batord+ ' ' +@battext+ ' Bn., RM',4,1,'|', @admincorpsid, 1, null)
--, (0, @unitE, null, '___'+@batnumb+ @batord+ ' ' +@battext+ ' Bn., RM',4,1,'|', @admincorpsid, 1, null)
--, (0, @unitF, null, '___'+@batnumb+ @batord+ ' ' +@battext+ ' Bn., RM',4,1,'|', @admincorpsid, 1, null)
--, (0, null, 'Engineer Support', '___'+@batnumb+ @batord+ ' ' +@battext+ ' Bn., RM',4,1,'|', @admincorpsid, 1, null)
--, (0, null, 'Mobility Assault', '___'+@batnumb+ @batord+ ' ' +@battext+ ' Bn., RM',4,1,'|', @admincorpsid, 1, null)

--select id from @OutputTbl
--ORDER BY ID DESC

--select CONCAT(letter, missionname, commandname) from Unit where unitid in (select id from @OutputTbl )

set @hsc_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 1)
set @a_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 2)
set @b_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 3)
set @c_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 4)
--set @d_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 5)
--set @e_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 6)
--set @f_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 7)
--set @spt_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 5)
--set @wpn_id=(SELECT ID FROM ( SELECT ROW_NUMBER () OVER (ORDER BY id) AS RowNum, * FROM @OutputTbl ) sub WHERE RowNum = 6)


insert into Relationship (RelTypeIdx, RelFromUnitId, RelToUnitId, DoNotUse)
select 1, @parentid, id,0 from @OutputTbl; 

--select RelTypeIdx, RelFromUnitId, RelToUnitId from relationship where reltounitid in (select id from @OutputTbl )

insert into unitindex( IndexCode, UnitId, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
values 
('RM@' +@batcode+ '___'+@batnumb+'|!', @hsc_id, 1, 1, 0, 0, 10)
, ('HSC-___'+@batnumb+' ' +@batcode+ 'B/RM',@hsc_id, 0, 1, 0, 0, 20)
, ('HSC, ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@hsc_id, 0, 1, 0, 0, 30)
, ('~USMC ' +@batcode+ 'B ___'+@batnumb+'-HSC',@hsc_id, 0, 1, 1, 0, 50)

, ('RM@' +@batcode+ '___'+@batnumb+'|_'+@unitA,@a_id, 1, 1, 0, 0, 10)
, (@unitA+'-___'+@batnumb+' ' +@batcode+ 'B/RM',@a_id, 0, 1, 0, 0, 20)
, (@unitA+' Coy., ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@a_id, 0, 1, 0, 0, 30)
, ('~USMC ' +@batcode+ 'B ___'+@batnumb+'-'+@unitA,@a_id, 0, 1, 1, 0, 50)

, ('RM@' +@batcode+ '___'+@batnumb+'|_'+@unitB,@b_id, 1, 1, 0, 0, 10)
, (@unitB+'-___'+@batnumb+' ' +@batcode+ 'B/RM',@b_id, 0, 1, 0, 0, 20)
, (@unitB+' Coy., ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@b_id, 0, 1, 0, 0, 30)
, ('~USMC ' +@batcode+ 'B ___'+@batnumb+'-'+@unitB,@b_id, 0, 1, 1, 0, 50)

, ('RM@' +@batcode+ '___'+@batnumb+'|_'+@unitC,@c_id, 1, 1, 0, 0, 10)
, (@unitC+'-___'+@batnumb+' ' +@batcode+ 'B/RM',@c_id, 0, 1, 0, 0, 20)
, (@unitC+' Coy., ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@c_id, 0, 1, 0, 0, 30)
, ('~USMC ' +@batcode+ ' ___'+@batnumb+'-'+@unitC,@c_id, 0, 1, 1, 0, 50)

--, ('RM@' +@batcode+ '___'+@batnumb+'|_'+@unitD,@d_id, 1, 1, 0, 0, 10) 
--, (@unitD+'-___'+@batnumb+' ' +@batcode+ 'B/RM',@d_id, 0, 1, 0, 0, 20)
--, (@unitD+' Coy., ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@d_id, 0, 1, 0, 0, 30)
--, ('~USMC ' +@batcode+ ' ___'+@batnumb+'-'+@unitD,@d_id, 0, 1, 1, 0, 50)

--, ('RM@' +@batcode+ '___'+@batnumb+'|_'+@unitE,@e_id, 1, 1, 0, 0, 10)
--, (@unitE+'-___'+@batnumb+' ' +@batcode+ 'B/RM',@e_id, 0, 1, 0, 0, 20)
--, (@unitE+' Coy., ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@e_id, 0, 1, 0, 0, 30)
--, ('~USMC ' +@batcode+ ' ___'+@batnumb+'-'+@unitE,@e_id, 0, 1, 1, 0, 50)

--, ('RM@' +@batcode+ '___'+@batnumb+'|_'+@unitF,@f_id, 1, 1, 0, 0, 10)
--, (@unitF+'-___'+@batnumb+' ' +@batcode+ 'B/RM',@f_id, 0, 1, 0, 0, 20)
--, (@unitF+' Coy., ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@f_id, 0, 1, 0, 0, 30)
--, ('~USMC ' +@batcode+ ' ___'+@batnumb+'-'+@unitF,@f_id, 0, 1, 1, 0, 50)

--, ('RM@' +@batcode+ '___'+@batnumb+'|MA', @wpn_id, 1, 1, 0, 0, 10)
--, ('MAC-___'+@batnumb+' ' +@batcode+ 'B/RM',@wpn_id, 0, 1, 0, 0, 20)
--, ('Mob. Aslt. Coy., ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@wpn_id, 0, 1, 0, 0, 30)
--, ('~USMC MARINES ' +@batcode+ ' ___'+@batnumb+'-MAC',@wpn_id, 0, 1, 1, 0, 50)

--, ('RM@' +@batcode+ '___'+@batnumb+'|ES', @spt_id, 1, 1, 0, 0, 10)
--, ('ESC-___'+@batnumb+' ' +@batcode+ 'B/RM',@spt_id, 0, 1, 0, 0, 20)
--, ('Eng. Supt. Coy., ___'+@batnumb+@batord+' ' +@battext+ ' Bn., RM',@spt_id, 0, 1, 0, 0, 30)
--, ('~USMC MARINES ' +@batcode+ ' ___'+@batnumb+'-ESC',@spt_id, 0, 1, 1, 0, 50)

--@d_id, 

insert into missionunit (missionid, unitid)
values (@hsc_missionid, @hsc_id)
, (@inf_missionid, @a_id)
, (@inf_missionid, @b_id)
, (@inf_missionid, @c_id)
--, (@inf_missionid, @d_id)
--, (@inf_missionid, @e_id)
--, (@inf_missionid, @f_id)
--, (@wpn_missionid, @wpn_id)
--, (@spt_missionid, @spt_id)

insert into Tennant (Baseid, unitid, ishost)
values (@baseid, @hsc_id, 0)
, (@baseid, @a_id, 0)
, (@baseid, @b_id, 0)
, (@baseid, @c_id, 0)
--, (@baseid, @d_id, 0)
--, (@baseid, @e_id, 0)
--, (@baseid, @f_id, 0)
--, (@baseid, @wpn_id, 0)
--, (@baseid, @spt_id, 0)


select Unit.UnitId, CONCAT(number, letter, missionname, commandname) , Mission.FullName, base.SortName  
from Unit 
inner join  missionunit on  unit.unitid = missionunit.unitid 
inner join mission on missionunit.missionid = mission.MissionId
inner join Tennant on unit.UnitId = Tennant.UnitId
inner join Base on tennant.BaseId = base.BaseId

where unit.Unitid = @parentid or unit.UnitId in (select id from @OutputTbl) 
--,@d_id ,@e_id,@f_id, @wpn_id, @spt_id
select indexcode from unitindex where unitid in (@hsc_id, @a_id, @b_id, @c_id) order by UnitId, DisplayOrder

rollback tran tran1