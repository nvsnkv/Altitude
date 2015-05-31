namespace Altitude.Domain.Storage
{
    public interface IExporter
    {
        void Prepare();
        void Export(Position item);
        void Finish();
    }
}