using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    private const string InputFilePath = "bikes.xml";
    private const string LogFilePath = "batch_log.txt";

    public static void Main(string[] args)
    {
        Console.WriteLine("--- ConsoleApp: Отчеты по велопрокату ---");
        try
        {
            RentalProcessor processor = new RentalProcessor();
            List<Rental> rentals = processor.LoadRentals(InputFilePath);

            DateTime startDate, endDate;
            if (!TryReadDate("Введите начальную дату периода (гггг-мм-дд): ", out startDate) ||
                !TryReadDate("Введите конечную дату периода (гггг-мм-дд): ", out endDate))
            {
                Console.WriteLine("Операция прервана. Некорректный ввод даты.");
                return;
            }

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

            using (StreamWriter sw = new StreamWriter("debtors.csv", false, Encoding.UTF8))
            {
                sw.WriteLine("Карта Клиента,Просрочка (дни),Сумма Долга (Штраф)");

                var today = DateTime.Today.Date;

                foreach (var rental in rentals)
                {
                    rental.CalculateTotalAmount(Rental.FineRate);
                }

                var debtors = rentals
                    .Where(r => r.IsOverdue(today) && r.Fine > 0)
                    .GroupBy(r => r.CardNo)
                    .Select(g => new
                    {
                        CardNo = g.Key,
                        MaxDelayDays = (int)g.Max(r => (today - r.EndDate).TotalDays),
                        TotalDebt = g.Sum(r => r.Fine)
                    });

                foreach (var debtor in debtors)
                {
                    sw.WriteLine($"\"{debtor.CardNo.MaskCardNo()}\",{debtor.MaxDelayDays},{debtor.TotalDebt:F2}");
                }
            }
            Console.WriteLine("Отчёт debtors.csv успешно создан.");
        }
        catch (Exception ex)
        {
            File.AppendAllText(LogFilePath,
                               $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ОШИБКА: {ex.Message}\r\n" +
                               $"Стек: {ex.StackTrace}\r\n\r\n");

            Console.WriteLine($"Произошла критическая ошибка. Подробности записаны в файл {LogFilePath}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
    private static bool TryReadDate(string prompt, out DateTime date)
    {
        Console.Write(prompt);
        return DateTime.TryParse(Console.ReadLine(), out date);
    }
}