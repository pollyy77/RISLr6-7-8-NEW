using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

public class RentalBase
{
    public int RentalId { get; set; }

    public RentalBase(int id)
    {
        RentalId = id;
    }

    public bool IsOverdue(DateTime actualReturnDate)
    {
        return actualReturnDate > EndDate;
    }

    public virtual decimal CalculateTotalAmount(decimal fineRatePerDay)
    {
        return AmountPaid;
    }

    public virtual string GetSummary()
    {
        return $"Прокат #{RentalId}. Клиент: {CardNo}, Период: {StartDate:yyyy-MM-dd} - {EndDate:yyyy-MM-dd}";
    }

    public string CardNo { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal AmountPaid { get; set; }
}

