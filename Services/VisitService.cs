using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagement.Models;

namespace VisitorManagement.Services
{
    public class VisitService : IVisitService
    {
        private readonly AppDbContext _context;

        public VisitService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckInAsync(int visitorId)
        {
            var visitor = await _context.Visitors.FindAsync(visitorId);

            if (visitor == null)
            {
                visitor = new Visitor
                {
                    Name = $"Visitor {visitorId}",
                    ContactInfo = "0000000000"
                };
                _context.Visitors.Add(visitor);
                await _context.SaveChangesAsync();
                visitorId = visitor.Id;
            }

            var today = DateTime.Today;

            var visit = await _context.VisitLogs
                .FirstOrDefaultAsync(v => v.VisitorId == visitorId
                                          && v.VisitDate == today
                                          && v.Status == "Scheduled");

           
            if (visit == null)
            {
                visit = new VisitLog
                {
                    VisitorId = visitorId,
                    VisitDate = today,
                    HostEmployeeId = 1, 
                    Purpose = "Auto-generated visit",
                    Status = "Scheduled",
                    Notes = ""
                };
                _context.VisitLogs.Add(visit);
                await _context.SaveChangesAsync();
            }

            if (visit.CheckInTime != null)
                return false;

            var now = DateTime.Now;
            var currentTime = now.TimeOfDay;

            if (currentTime < TimeSpan.FromHours(8) || currentTime > TimeSpan.FromHours(19))
                return false;

            visit.CheckInTime = now;
            visit.Status = "Checked In";

            if (visit.ScheduledTime != null && (now - visit.ScheduledTime.Value).TotalMinutes > 30)
                visit.Notes = "Late Visit";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckOutAsync(int visitorId)
        {
            var visit = await _context.VisitLogs
                .FirstOrDefaultAsync(v => v.VisitorId == visitorId && v.Status == "Checked In");

            if (visit == null)
                return false;

            visit.CheckOutTime = DateTime.Now;
            visit.Status = "Checked Out";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<VisitLog>> GetTodayVisitsAsync(int hostEmployeeId)
        {
            var today = DateTime.Today;
            var visits = await _context.VisitLogs
                .Include(v => v.Visitor)
                .Include(v => v.HostEmployee)
                .Where(v => v.HostEmployeeId == hostEmployeeId && v.VisitDate == today)
                .ToListAsync();

            var nowTime = DateTime.Now.TimeOfDay;
            bool isUpdated = false;

            foreach (var visit in visits)
            {
                if (visit.Status == "Checked In" && visit.CheckOutTime == null && nowTime > TimeSpan.FromHours(19))
                {
                    visit.Status = "No Show";
                    isUpdated = true;
                }
            }

            if (isUpdated)
                await _context.SaveChangesAsync();

            return visits;
        }

        public async Task<bool> ManualUpdateAsync(VisitLog visit)
        {
            _context.VisitLogs.Update(visit);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DateTime>> GetNoShowDatesAsync()
        {
            return await _context.VisitLogs
                .Where(v => v.Status == "No Show")
                .Select(v => v.VisitDate.Date)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<VisitLog>> GetNoShowsByDateAsync(DateTime date)
        {
            return await _context.VisitLogs
                .Include(v => v.Visitor)
                .Include(v => v.HostEmployee)
                .Where(v => v.Status == "No Show" && v.VisitDate.Date == date.Date)
                .ToListAsync();
        }



        public async Task<MontlyReport> GetMonthlyReportAsync(int year, int month)
        {
            var visits = await _context.VisitLogs
                .Where(v => v.VisitDate.Year == year && v.VisitDate.Month == month)
                .ToListAsync();

            var totalVisits = visits.Count;
            var uniqueVisitors = visits.Select(v => v.VisitorId).Distinct().Count();
            var noShowCount = visits.Count(v => v.Status == "No Show");
            var lateVisitCount = visits.Count(v => v.Notes == "Late Visit");

            return new MontlyReport
            {
                TotalVisits = totalVisits,
                UniqueVisitors = uniqueVisitors,
                NoShowCount = noShowCount,
                LateVisitCount = lateVisitCount
            };
        }
    }
}
