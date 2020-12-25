namespace NGame.NCore
{
    public interface ISystem:System.IDisposable
    {
        void Execute();
        void Initialize();
        void Filter(IEntity entity);
    }
}
