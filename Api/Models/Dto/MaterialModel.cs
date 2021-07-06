namespace Api.Models.Dto
{
    public class MaterialModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }

        public CategoryModel Category { get; set; }
        public UnitOfMeasurementModel UnitOfMeasurement { get; set; }
    }
}
