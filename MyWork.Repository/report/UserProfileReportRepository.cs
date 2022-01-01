using MyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public class UserProfileReportRepository : IUserProfileReportRepository
    {
		private readonly MyWorkDbContext ctx;
		public UserProfileReportRepository(MyWorkDbContext context)
        {
			this.ctx = context;
        }

		public IEnumerable<UserProfileReport> Search(string dateFrom, string dateTo)
        {
			var pDateFrom = new SqlParameter("@dateFrom", dateFrom);
			var pDateTo = new SqlParameter("@dateTo", dateTo);
			return ctx.Database.SqlQuery<UserProfileReport>("usp_SearchUserProfile @dateFrom, @dateTo", pDateFrom, pDateTo).ToList();
		}

	}

    public class UserProfileReport
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Boolean? Gender { get; set; } = null;
		public DateTime? BirthDate { get; set; } = null;
		public string Mobile { get; set; }
		public string Address { get; set; }
		public int Year { get; set; }
		public int Quarter { get; set; }
		public int Month { get; set; }
		public int Week { get; set; }
		public int Day { get; set; }
	}
}
