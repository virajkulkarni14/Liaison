﻿using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public class BLLBase
    {
   
        internal string Prefix { get; set; }
        internal string Name { get; }
        internal string Suffix { get; set; }
        internal string CommissionedName { get; set; }
        public string AltName { get; set; }
        public BLLBase ParentBase { get; private set; }
        public BLLBases SubFacilities { get; private set; }
        public bool IsHost { get; set; }
        public BLLBase(Tennant tennant)
        {
            if (tennant == null)
            {
                return;
            }

            this.CommissionedName = tennant.Base.CommissionedName;
            this.IsHost = tennant.IsHost;
            this.Prefix = tennant.Base.Prefix;
            this.Name = tennant.Base.Name;
            this.Suffix = tennant.Base.Suffix;
            this.AltName = tennant.Base.AltName;
            this.ParentBase = new BLLBase(tennant.Base.ParentBase);
            this.SubFacilities = new BLLBases(tennant.Base.SubFacilities);
        }

        public BLLBase(Data.Sql.Edmx.Base facility)
        {
            if (facility == null)
            {
                return;
            }

            this.CommissionedName = facility.CommissionedName;
            this.IsHost = false;
            this.Prefix = facility.Prefix;
            this.Name = facility.Name;
            this.Suffix = facility.Suffix;
            this.AltName = facility.AltName;
            if (facility.ParentBaseId != null)
            {
                this.ParentBase = new BLLBase(facility.ParentBase);
            }
            //this.SubFacilities = new BLLBases(facility.SubFacilities);
        }
    }
}