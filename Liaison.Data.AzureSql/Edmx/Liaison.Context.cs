﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Liaison.Data.AzureSql.Edmx
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LiaisonAzureEntities : DbContext
    {
        public LiaisonAzureEntities()
            : base("name=LiaisonAzureEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminCorp> AdminCorps { get; set; }
        public virtual DbSet<Aircraft> Aircraft { get; set; }
        public virtual DbSet<AltCode> AltCodes { get; set; }
        public virtual DbSet<Base> Bases { get; set; }
        public virtual DbSet<ConfigSetting> ConfigSettings { get; set; }
        public virtual DbSet<Dictionary> Dictionaries { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentLink> DocumentLinks { get; set; }
        public virtual DbSet<DocumentReference> DocumentReferences { get; set; }
        public virtual DbSet<EquipmentOwner> EquipmentOwners { get; set; }
        public virtual DbSet<GroundEquipment> GroundEquipments { get; set; }
        public virtual DbSet<GroundEquipmentType> GroundEquipmentTypes { get; set; }
        public virtual DbSet<Mission> Missions { get; set; }
        public virtual DbSet<MissionUnit> MissionUnits { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<RelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<ShipClass> ShipClasses { get; set; }
        public virtual DbSet<ShipClassMember> ShipClassMembers { get; set; }
        public virtual DbSet<ShipPrefix> ShipPrefixes { get; set; }
        public virtual DbSet<SortOrder> SortOrders { get; set; }
        public virtual DbSet<TaskForce> TaskForces { get; set; }
        public virtual DbSet<Tennant> Tennants { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<UnitIndex> UnitIndexes { get; set; }
    }
}
