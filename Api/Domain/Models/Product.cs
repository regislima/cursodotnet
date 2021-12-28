namespace api.Domain.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public short QuantityInPackage { get; set; }
        public UnityOfMeasurement UnitOfMeasurement { get; set; }
        
        #region Foreign Key
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        #endregion
    }
}