using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Rental
{
    public const decimal FineRate = 50.0M;

    public static bool IsValid(Rental rental)
    {
        return rental.EndDate >= rental.StartDate && rental.AmountPaid >= 0;
    }

    public void DisplayInfo()
    {
        Console.WriteLine(GetSummary());
    }
}
