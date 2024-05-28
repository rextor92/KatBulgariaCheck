namespace KatBulgariaCheck.Models.Kat
{
    public class KatResponse
    {
        public List<ObligationData> ObligationsData { get; set; } = new List<ObligationData>();
    }

    public class ObligationData
    {
        public int UnitGroup { get; set; }
        public bool ErrorNoDataFound { get; set; }
        public bool ErrorReadingData { get; set; }
        public List<object> Obligations { get; set; } = new List<object>();
    }
}