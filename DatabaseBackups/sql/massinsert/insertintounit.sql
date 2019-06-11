declare @admincorpsid int

set @admincorpsid = 1285

DECLARE @OutputTbl TABLE (ID INT)

INSERT INTO Unit ( UseOrdinal, Letter, MissionName, ServiceIdx, ServiceTypeIdx,  RankSymbol, AdminCorpsId, CanHide, Notes)
OUTPUT INSERTED.UnitId INTO @OutputTbl(ID)
VALUES 
(0, null, 'Headquarters & Service', 4,1,'|', @admincorpsid, 1, null),
(0, 'A', null, 4,1,'|', @admincorpsid, 1, null),
(0, 'B', null, 4,1,'|', @admincorpsid, 1, null),
(0, 'C', null, 4,1,'|', @admincorpsid, 1, null),
(0, null, 'Weapons', 4,1,'|', @admincorpsid, 1, null)

select id from @OutputTbl

insert into Relationship (RelTypeIdx, RelFromUnitId, RelToUnitId, DoNotUse)
select 4, 6996, id,0 from @OutputTbl; 