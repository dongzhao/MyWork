using MyWork.Core;
using MyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public class UserProfileQuery : IUserProfileQuery
    {
        protected readonly MyWorkDbContext ctx;

        public UserProfileQuery(MyWorkDbContext context)
        {
            this.ctx = context;
        }

        public Func<UserProfile, bool> Filter()
        {
            return PredicateBuilder.True<UserProfile>();
        }

        //public Func<UserProfile, bool> WithFirstName(Func<UserProfile, bool> func, string strName)
        //{
        //    Func<UserProfile, bool> condition = c => (c.FirstName == strName);
        //    func = condition.And(func);
        //    return func;
        //}

        //public Func<UserProfile, bool> WithLastName(Func<UserProfile, bool> func, string strName)
        //{
        //    Func<UserProfile, bool> condition = c => (c.LastName == strName);
        //    func = condition.And(func);
        //    return func;
        //}

        //public Func<UserProfile, bool> WithGender(Func<UserProfile, bool> func, bool bGender)
        //{
        //    Func<UserProfile, bool> condition = c => (c.Gender == bGender);
        //    func = condition.And(func);
        //    return func;
        //}

        //public Func<UserProfile, bool> WithBirthDate(Func<UserProfile, bool> func, DateTime dBirthDate)
        //{
        //    Func<UserProfile, bool> condition = c => (c.BirthDate.HasValue && c.BirthDate.Value.Date.CompareTo(dBirthDate.Date)==0);
        //    func = condition.And(func);
        //    return func;
        //}

        private IEnumerable<UserProfile> Execute(Func<UserProfile, bool> func)
        {
            return ctx.UserProfileSet.Where(func).ToList();
        }

        public IEnumerable<UserProfile> Execute(List<QueryItem> items)
        {
            var func = Filter();
            foreach (var item in items)
            {
                //var type = Type.GetType(item.Operator);
                if (item.ItemName == UserProfileQueryField.FirstName)
                {
                    //func = WithFirstName(func, DynamicConvertor.To<string>(item.ItemValue));
                    func = func.And(c => c.FirstName == DynamicConvertor.To<string>(item.ItemValue) );
                }
                if (item.ItemName == UserProfileQueryField.LastName)
                {
                    //func = WithLastName(func, DynamicConvertor.To<string>(item.ItemValue));
                    func = func.And(c => c.LastName == DynamicConvertor.To<string>(item.ItemValue) );
                }
                if (item.ItemName == UserProfileQueryField.Gender)
                {
                    //func = WithGender(func, DynamicConvertor.To<bool>(item.ItemValue));
                    func = func.And(c => c.Gender == DynamicConvertor.To<bool>(item.ItemValue) );
                }
                if (item.ItemName == UserProfileQueryField.BirthDate)
                {
                    //func = WithBirthDate(func, DynamicConvertor.To<DateTime>(item.ItemValue));
                    func = func.And(c => c.BirthDate.HasValue && c.BirthDate.Value.Date.CompareTo(DynamicConvertor.To<DateTime>(item.ItemValue).Date) == 0 );
                }
            }
            return Execute(func);
        }

    }

    public class UserProfileQueryField
    {
        public static string FirstName
        {
            get { return "FirstName"; }
        }

        public static string LastName
        {
            get { return "LastName"; }
        }

        public static string Gender
        {
            get { return "Gender"; }
        }

        public static string BirthDate
        {
            get { return "BirthDate"; }
        }

        public static List<string> FieldNames
        {
            get
            {
                return new List<string>()
                {
                    FirstName,
                    LastName,
                    Gender,
                    BirthDate,
                };
            }
        }
    }

}
