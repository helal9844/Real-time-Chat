using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_DAL;

public static class DateTimeForAgeExtentions
{
    public static int CalculateAge(this DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age =  today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }
}
