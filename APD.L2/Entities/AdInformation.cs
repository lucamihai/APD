namespace APD.L2.Entities
{
    public class AdInformation
    {
        public string ImageFilePath { get; set; }
        public string RedirectTo { get; set; }
        public int Priority { get; set; }

        // Override for easier debugging
        public override string ToString()
        {
            return $"Priority: {Priority}, ImageFilePath: {ImageFilePath}";
        }
    }
}