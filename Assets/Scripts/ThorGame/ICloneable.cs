namespace ThorGame
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}