using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckPlanTools.Interfaces.Services
{
    internal interface IGeolocationService
    {
        string GetCountryFromCordinate(string cordinate);
    }
}
