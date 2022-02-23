namespace RecipeLibrary.Models
{
    public class StandardIngredient
    {
        public string Name { get; set; }
        public string Measurement
        {
            get { return $"{MeasurementInfo?.Quantity} {MeasurementInfo?.Measure}"; }
        }
        public Measurement MeasurementInfo { get; set; }
        public int Calories { get; set; }
    }
}
