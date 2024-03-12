namespace KatBulgariaCheck.Models.Kat
{
    public class KatResponse
    {
        public List<ObligationData> ObligationsData { get; set; }
    }

    public class ObligationData
    {
        public int UnitGroup { get; set; }
        public bool ErrorNoDataFound { get; set; }
        public bool ErrorReadingData { get; set; }
        public List<object> Obligations { get; set; }
    }
}