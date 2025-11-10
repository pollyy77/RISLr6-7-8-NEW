using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Rental : RentalBase
{
    public delegate void FineCalculatedHandler(decimal fineAmount);
    public event FineCalculatedHandler OnFineCalculated;

    public decimal Fine { get; set; }
    public string ClientName { get; set; }

    public Rental(int id) : base(id) { }

    public override decimal CalculateTotalAmount(decimal fineRatePerDay)
    {
        if (IsOverdue(DateTime.Today))
        {
            TimeSpan delay = DateTime.Today.Date - EndDate.Date;
            Fine = Math.Max(0, (decimal)delay.TotalDays * fineRatePerDay);

            OnFineCalculated?.Invoke(Fine);
        }

        return base.CalculateTotalAmount(fineRatePerDay) + Fine;
    }
}