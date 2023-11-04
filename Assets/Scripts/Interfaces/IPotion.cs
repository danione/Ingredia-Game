public interface IPotion 
{
    public bool Destroyed { get;}
    public void Use();
    public void Destroy();
    public void Tick(); // Update function, used to determine when to destroy
    public void Reset();
}
