using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IAvailabilityRepository
    {
        void AddAvailabilities(List<Availability> availabilities);
        List<Availability> GetAvailabilitiesBetweenDates(DateTime firsteDate, DateTime lastDate);
    }
}
