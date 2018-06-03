using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public class BLLMission
    {
        public BLLMission(MissionUnit mu)
        {
            this.DisplayName = mu.Mission.DisplayName;
            this.Variant = mu.MissionVariant;
            this.FullName = mu.Mission.FullName;
            this.MissionNotes = mu.Mission.Notes;
            this.MissionVariantNotes = mu.Notes;
        }

        public string MissionVariantNotes { get; set; }

        public string MissionNotes { get; set; }

        public string FullName { get; set; }

        public string Variant { get; set; }

        public string DisplayName { get; set; }
    }
}