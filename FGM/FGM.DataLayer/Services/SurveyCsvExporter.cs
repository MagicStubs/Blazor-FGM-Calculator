using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using FGM.DataLayer.Interfaces;

namespace FGM.DataLayer.Services;

public class SurveyCsvExporter : ICsvExporter<CalculatedSurvey>
{
    public string Export(List<CalculatedSurvey> dataToExport)
    {
        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture));
        csvWriter.WriteRecords(dataToExport);
        streamWriter.Flush();
        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}