using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorManagement.Models;

namespace VisitorManagement.Services
{
    public interface IVisitService
    {
        Task<bool> CheckInAsync(int visitorId);
        Task<bool> CheckOutAsync(int visitorId);
        Task<List<VisitLog>> GetTodayVisitsAsync(int hostEmployeeId);
        Task<bool> ManualUpdateAsync(VisitLog visit);
        Task<List<DateTime>> GetNoShowDatesAsync();
        Task<List<VisitLog>> GetNoShowsByDateAsync(DateTime date);
        Task<MontlyReport> GetMonthlyReportAsync(int year, int month);
       

    }
}
