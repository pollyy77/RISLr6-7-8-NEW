using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    private const string InputFilePath = "bikes.xml";

    public static void Main(string[] args)
    {
        Console.WriteLine("--- ConsoleApp: Отчеты по велопрокату ---");

        RentalProcessor processor = new RentalProcessor();
        List<Rental> rentals = processor.LoadRentals(InputFilePath);

        Console.Write("Введите начальную дату периода (гггг-мм-дд): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Введите конечную дату периода (гггг-мм-дд): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine());

        if (startDate > endDate)
        {
            Console.WriteLine("Ошибка: Начальная дата не может быть позже конечной.");
            return;
        }

        var allDates = Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1)
                                 .Select(offset => startDate.AddDays(offset).Date);

        var dailyData = rentals
            .Where(r => r.EndDate.Date >= startDate.Date && r.EndDate.Date <= endDate.Date)
            .GroupBy(r => r.EndDate.Date)
            .Select(g => new
            {
                Date = g.Key,
                TotalRevenue = g.Sum(r => r.AmountPaid),
                TotalFines = g.Sum(r => r.Fine)
            });

        using (StreamWriter sw = new StreamWriter("revenue_period.txt"))
        {
            sw.WriteLine("Дата\t\tВыручка\t\tШтрафы");

            var finalReport = from date in allDates
                              join data in dailyData on date equals data.Date into joined
                              from subData in joined.DefaultIfEmpty(new { Date = date, TotalRevenue = 0M, TotalFines = 0M })
                              select new
                              {
                                  Date = date,
                                  Revenue = subData.TotalRevenue,
                                  Fines = subData.TotalFines
                              };

            foreach (var item in finalReport.OrderBy(x => x.Date))
            {
                sw.WriteLine($"{item.Date:yyyy-MM-dd}\t{item.Revenue:F2}\t\t{item.Fines:F2}");
            }
        }
        Console.WriteLine("Отчёт revenue_period.txt успешно создан.");

        Console.ReadKey();
    }
}