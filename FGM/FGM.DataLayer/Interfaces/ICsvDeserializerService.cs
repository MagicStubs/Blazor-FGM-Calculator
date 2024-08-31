using Microsoft.AspNetCore.Components.Forms;

namespace FGM.DataLayer.Interfaces;

public interface ICsvDeserializerService<T>
{
    public List<T> DeserializeCsvFile(IBrowserFile fileToDeserialize);

    public Task<List<T>> DeserializeCsvFileAsync(IBrowserFile fileToDeserialize);
}