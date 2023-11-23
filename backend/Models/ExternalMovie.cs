namespace backend.Models
{
    public class ExternalMovie
    {
        public required int id { get; set; }
        public required string title { get; set; }
        public required string director {  get; set; }
        public required int year { get; set; }
        public required int rate { get; set; }
    }
}
