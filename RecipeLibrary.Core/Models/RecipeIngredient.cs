namespace RecipeLibrary.Models
{
    public class RecipeIngredient
    {
        public StandardIngredient Ingredient { get; set; }
        public decimal Quantity { get; set; }
        public decimal Calories
        {
            get { return (Ingredient?.Calories * Quantity).GetValueOrDefault(); }
        }

        public string Measurement
        {
            get { return Ingredient == null ? null : $"{(Quantity * Ingredient?.MeasurementInfo?.Quantity).Value:G29} {Ingredient?.MeasurementInfo?.Measure}"; }
        }
    }
}
