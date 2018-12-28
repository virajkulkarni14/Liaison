namespace Liaison.BLL.Languages
{
    public interface ILanguage
    {
        string GetBattalionName(Liaison.BLL.Models.Unit.Battalion battalion);
        string ToOrdinal(int? input, bool useOrdinal);
    }
}