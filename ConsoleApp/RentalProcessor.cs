using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;

public class RentalProcessor
{
    public List<Rental> LoadRentals(string filePath)
    {
        if (!System.IO.File.Exists(filePath))
        {
            throw new System.IO.FileNotFoundException($"Входной файл не найден: {filePath}");
        }

        XDocument doc = XDocument.Load(filePath);

        var rentals = doc.Descendants("Rental")
            .Select(x => new Rental(int.Parse(x.Element("RentalID")?.Value ?? "0"))
            {
                CardNo = x.Element("CardNo")?.Value,
                ClientName = x.Element("ClientName")?.Value,
                StartDate = DateTime.Parse(x.Element("StartDate")?.Value),
                EndDate = DateTime.Parse(x.Element("EndDate")?.Value),
                AmountPaid = decimal.Parse(x.Element("Amount")?.Value, CultureInfo.InvariantCulture),
                Fine = decimal.Parse(x.Element("Fine")?.Value ?? "0.0", CultureInfo.InvariantCulture)
            })
            .Where(Rental.IsValid)
            .ToList();

        return rentals;
    }
}
