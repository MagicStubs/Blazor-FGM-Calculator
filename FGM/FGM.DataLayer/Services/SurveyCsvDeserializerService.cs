using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FGM.Data;
using FGM.Data.Exceptions;
using FGM.Data.Maps;
using FGM.DataLayer.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace FGM.DataLayer.Services;

public class SurveyCsvDeserializerService : ICsvDeserializerService<Survey>
{
    public List<Survey> DeserializeCsvFile(IBrowserFile fileToDeserialize)
    {
        var fileExtension = Path.GetExtension(fileToDeserialize.Name);
        if (!string.Equals(fileExtension, ".csv", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidFileTypeException(
                $"The file type '{fileExtension}' is not supported. Only CSV files are allowed.");
        }

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using var reader = new StreamReader(fileToDeserialize.OpenReadStream());
        using var csv = new CsvReader(reader, config);

        // Register the custom mapping
        csv.Context.RegisterClassMap<SurveyMap>();

        return csv.GetRecords<Survey>().ToList();
    }

    public async Task<List<Survey>> DeserializeCsvFileAsync(IBrowserFile fileToDeserialize)
    {
        var fileExtension = Path.GetExtension(fileToDeserialize.Name);
        if (!string.Equals(fileExtension, ".csv", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidFileTypeException(
                $"The file type '{fileExtension}' is not supported. Only CSV files are allowed.");
        }

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using var memoryStream = new MemoryStream();
        await fileToDeserialize.OpenReadStream().CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        using var reader = new StreamReader(memoryStream);
        using var csv = new CsvReader(reader, config);
        
        csv.Context.RegisterClassMap<SurveyMap>();

        var records = new List<Survey>();
        await foreach (var record in csv.GetRecordsAsync<Survey>())
        {
            records.Add(record);
        }

        return records;
    }
}