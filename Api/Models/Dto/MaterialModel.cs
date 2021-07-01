namespace Api.Models.Dto
{
    public class MaterialModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int UnitOfMeasurementId { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }
    }
}
