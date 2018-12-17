namespace Liaison.BLL.Models.Objects
{
	public class HCS
	{
		public HCS(string hCS, int? hCSNumber)
		{
			this.Code = hCS;
			if (hCSNumber.HasValue)
			{
				this.Number = hCSNumber.Value;
			}
		}

		public string Code
		{
			get; set;
		}

		public int Number { get; set; }
	}
}