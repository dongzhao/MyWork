using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public interface IUserProfileReportUsp
    {
        IEnumerable<UserProfileReport> Search(string dateFrom, string dateTo);
    }
}
