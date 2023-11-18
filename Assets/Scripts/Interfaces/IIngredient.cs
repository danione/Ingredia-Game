public interface IIngredient
{
    public IngredientData Data { get; set; }

    public void Initialise(IngredientData data);
}
