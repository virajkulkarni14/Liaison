using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public class BllMission
    {
        public BllMission(MissionUnit mu)
        {
            this.MissionId = mu.MissionId;
            this.DisplayName = mu.Mission.DisplayName;
            this.Variant = mu.MissionVariant;
            this.IsAssociate = mu.IsAssociate;
            this.FullName = mu.Mission.FullName;
            this.MissionNotes = mu.Mission.Notes;
            this.MissionVariantNotes = mu.Notes;
        }
        public int MissionId { get; set; }
        public string MissionVariantNotes { get; set; }
        public bool IsAssociate { get; set; }
        public string MissionNotes { get; set; }

        public string FullName { get; set; }

        public string Variant { get; set; }

        public string DisplayName { get; set; }
    }
}