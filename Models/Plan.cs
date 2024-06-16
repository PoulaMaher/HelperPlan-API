namespace HelperPlan.Models
{
    public class Plan
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public decimal Price { get; set; }
        public string? Type { get; set; }
        
        public int Duration {  get; set; }

        //Navigation Properties
        public Subscribtion? Subscribtion { get; set; }
    }
}
