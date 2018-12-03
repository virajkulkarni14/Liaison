use Liaison 
go

declare @oldid int
declare  @newid int
declare @newparent int
declare @newnumber int
declare @newnumbstr nvarchar(255)
declare @newnumbord nvarchar(255)
declare @missionid int

set @oldid=3515
set @missionid = 2280

--------- update -----------
set @newnumber = 308
set @newparent = 3508
----------------------------

set @newnumbstr  = Concat('__', @newnumber)
set @newnumbord = 
    CONVERT(VARCHAR(10),@newnumber) + CASE WHEN @newnumber % 100 IN (11, 12, 13) THEN 'th'
    ELSE 
        CASE @newnumber % 10
            WHEN 1 THEN 'st'
            WHEN 2 THEN 'nd'
            WHEN 3 THEN 'rd'
        ELSE 'th'
        END
    END 


insert into unit (Number, UseOrdinal, Letter, NickName, LegacyMissionName, MissionName, UniqueName, CommandName, UnitTypeVariant, ServiceIdx, ServiceTypeIdx, TerritorialDesignation, UnitGuid, RankSymbol, AdminCorpsId, CanHide, 
                  Decommissioned)
				 
SELECT  Number, UseOrdinal, Letter, NickName, LegacyMissionName, MissionName, UniqueName, Concat (@newnumbord, ' (R) CA Bde'), UnitTypeVariant, ServiceIdx, 3, TerritorialDesignation, UnitGuid, RankSymbol, AdminCorpsId, CanHide, 
                  Decommissioned from unit where unitid =@oldid

				set  @newid =  SCOPE_IDENTITY()
--set @newid = 3516
				insert into UnitIndex (IndexCode, UnitId, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
				values 					
						(	Concat('CIVIL*', @newnumbstr, '-!'), @newid, 1, 1, 0, 0, 10		),
						(	Concat('HHC-',@newnumbstr, ' CA BDE'), @newid, 0, 1, 0, 0, 20	),
						(	Concat('HHC, ',@newnumbord, ' CA Bde.'), @newid, 1, 1, 0, 0, 30	)

insert into MissionUnit (missionid, unitid) values (@missionid, @newid)

insert into Relationship (RelTypeIdx, RelFromUnitId, RelToUnitId, DoNotUse) values (1, @newparent, @newid, 0)

select @newid