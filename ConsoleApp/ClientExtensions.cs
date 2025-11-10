using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ClientExtensions
{
    public static string MaskCardNo(this string cardNo)
    {
        if (string.IsNullOrWhiteSpace(cardNo) || cardNo.Length < 4)
        {
            return cardNo;
        }
        return new string('*', cardNo.Length - 4) + cardNo.Substring(cardNo.Length - 4);
    }
}