namespace ThorGame.Trees
{
    public interface IConnection
    {
        INode From { get; }
        INode To { get; }
        
#if UNITY_EDITOR
        string TreeTitle { get; }
        string TreeGuid { get; set; }
#endif
    }
}