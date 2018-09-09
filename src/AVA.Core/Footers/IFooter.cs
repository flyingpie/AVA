namespace AVA.Core.Footers
{
    public interface IFooter
    {
        int Priority { get; }

        void Draw();
    }
}