use Liaison 
go

declare @oldid int
declare  @newid int
declare @newparent int
declare @newnumber int
declare @newnumbstr nvarchar(255)
declare @newnumbord nvarchar(255)
declare @commandname nvarchar(255)
declare @unitIndex nvarchar(255)
declare @index10pre nvarchar(255)
declare @index10post nvarchar(255)
declare @index20pre nvarchar(255)
declare @index20post nvarchar(255)
declare @index30pre nvarchar(255)
declare @index30post nvarchar(255)
declare @missionid int
declare @baseid int
declare @ishost bit

--------- update -----------
set @newnumber = 17
set @baseid = 2238
set @ishost = 0
	
set @newparent = 3562

set @oldid=3566
set @missionid = 2286
set @commandname = NULL-- ' (R) CA Bde'

set @index10pre = 'PSYOPS@'
set @index10post = ''--'-!'
set @index20pre = '' --'HHC-'
set @index20post = ' PSYOPS BN'
set @index30pre = ''--'HHC, '
set @index30post = ' (R) PsyOps Bn.'

--set @index10pre = 'CIVIL@'
--set @index10post = ''--'-!'
--set @index20pre = '' --'HHC-'
--set @index20post = ' CA BN'
--set @index30pre = ''--'HHC, '
--set @index30post = ' CA Bn.'
----------------------------

set @newnumbstr  = Concat('___', @newnumber)
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
				 
SELECT  @newnumber, UseOrdinal, Letter, NickName, LegacyMissionName, MissionName, UniqueName, Concat (@newnumbord, @commandname), UnitTypeVariant, ServiceIdx, 3, TerritorialDesignation, UnitGuid, RankSymbol, AdminCorpsId, CanHide, 
                  Decommissioned from unit where unitid =@oldid

				set  @newid =  SCOPE_IDENTITY()
--set @newid = 3516
				insert into UnitIndex (IndexCode, UnitId, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
				values 					
						(	Concat(@index10pre, @newnumbstr, @index10post), @newid, 1, 1, 0, 0, 10		),
						(	Concat(@index20pre,@newnumbstr, @index20post), @newid, 0, 1, 0, 0, 20	),
						(	Concat(@index30pre,@newnumbord, @index30post), @newid, 0, 1, 0, 0, 30	)

insert into MissionUnit (missionid, unitid) values (@missionid, @newid)

insert into Relationship (RelTypeIdx, RelFromUnitId, RelToUnitId, DoNotUse) values (1, @newparent, @newid, 0)

insert into Tennant (BaseId, UnitId, IsHost, Notes) values (@baseid, @newid, @ishost, null) 

select @newid

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


use Liaison 
go

declare @oldid int
declare  @newid int
declare @newparent int
declare @newnumber int
declare @newnumbstr nvarchar(255)
declare @newnumbord nvarchar(255)
declare @commandname nvarchar(255)
declare @unitIndex nvarchar(255)
declare @index10pre nvarchar(255)
declare @index10post nvarchar(255)
declare @index20pre nvarchar(255)
declare @index20post nvarchar(255)
declare @index30pre nvarchar(255)
declare @index30post nvarchar(255)
declare @missionid int
declare @baseid int
declare @ishost bit

--------- update -----------
set @newnumber = 486
set @baseid = 2170

set @newparent = 3498

set @ishost = 0
set @oldid=3514
set @missionid = 2277
set @commandname = NULL-- ' (R) CA Bde'
set @index10pre = 'CIVIL@'
set @index10post = ''--'-!'
set @index20pre = '' --'HHC-'
set @index20post = ' CA BN'
set @index30pre = ''--'HHC, '
set @index30post = ' CA Bn.'
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
				 
SELECT  @newnumber, UseOrdinal, Letter, NickName, LegacyMissionName, MissionName, UniqueName, Concat (@newnumbord, @commandname), UnitTypeVariant, ServiceIdx, 3, TerritorialDesignation, UnitGuid, RankSymbol, AdminCorpsId, CanHide, 
                  Decommissioned from unit where unitid =@oldid

				set  @newid =  SCOPE_IDENTITY()
--set @newid = 3516
				insert into UnitIndex (IndexCode, UnitId, IsSortIndex, IsDisplayIndex, IsAlt, IsPlaceholder, DisplayOrder)
				values 					
						(	Concat(@index10pre, @newnumbstr, @index10post), @newid, 1, 1, 0, 0, 10		),
						(	Concat(@index20pre,@newnumbstr, @index20post), @newid, 0, 1, 0, 0, 20	),
						(	Concat(@index30pre,@newnumbord, @index30post), @newid, 1, 1, 0, 0, 30	)

insert into MissionUnit (missionid, unitid) values (@missionid, @newid)

insert into Relationship (RelTypeIdx, RelFromUnitId, RelToUnitId, DoNotUse) values (1, @newparent, @newid, 0)

insert into Tennant (BaseId, UnitId, IsHost, Notes) values (@baseid, @newid, @ishost, null) 

select @newid