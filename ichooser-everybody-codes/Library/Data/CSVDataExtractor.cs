using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class CSVDataExtractor : IDataExtractor
    {
        private readonly string _csvFilename;
        public const string SemicolonSeparator = ";";

        public CSVDataExtractor(string csvFilename, string commaSeparator = SemicolonSeparator)
        {
            if (string.IsNullOrWhiteSpace(csvFilename))
            {
                throw new ArgumentException($"'{nameof(csvFilename)}' cannot be null or whitespace.", nameof(csvFilename));
            }

            // file exists
            if (!File.Exists(csvFilename))
            {
                throw new ArgumentException($"The CSV file '{csvFilename}' does not exist.", nameof(csvFilename));
            }

            _csvFilename = csvFilename;

            if (string.IsNullOrEmpty(commaSeparator))
            {
                commaSeparator = SemicolonSeparator;
            }
        }

        public IList<T> GetData<T>()
        {
            var data = new List<T>();

            string[] linesInFile;

            try
            {
                linesInFile = File.ReadAllLines(_csvFilename);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading all lines of the file.", ex);
            }

            // check there is data
            if (linesInFile.Length == 0)
            {
                return data;
            }

            // assume the first line contains the header
            var header = linesInFile[0];
            // get the header value and index
            var headerValues =
                header.Split(SemicolonSeparator)
                .Select((item, index) => new
                {
                    item,
                    index
                })
                .ToList();

            // get the properties in T to check that the header values match
            var typeOfT = typeof(T);
            var propertiesInT =
                typeOfT
                .GetProperties()
                .ToDictionary(e => e.Name);

            var headerValuesNotInT = headerValues.Where(e => !propertiesInT.ContainsKey(e.item)).ToList();

            if (headerValuesNotInT.Any())
            {
                throw new Exception($"The following header values are not located as properties within '{typeOfT}': [{string.Join("; ", headerValuesNotInT)}]");
            }

            // go throw the data and get an instance of T
            foreach (var line in linesInFile.Skip(1))
            {
                // split the line into values
                var lineValues = line.Split(SemicolonSeparator);

                // enough values in line
                if (lineValues.Length < headerValues.Count())
                    continue;

                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var headerValue in headerValues)
                {
                    // get the property
                    var property = propertiesInT[headerValue.item];

                    // assume the property can be converted 
                    var lineValue = lineValues[headerValue.index];

                    var valueConveted = Convert.ChangeType(lineValue, property.PropertyType);
                    property.SetValue(instanceOfT, valueConveted);
                }

                // add the data
                data.Add(instanceOfT);
            }

            return data;
        }
    }
}
