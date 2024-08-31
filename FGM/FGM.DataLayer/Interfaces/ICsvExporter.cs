namespace FGM.DataLayer.Interfaces;

public interface ICsvExporter<T>
{
    public string Export(List<T> dataToExport);
}