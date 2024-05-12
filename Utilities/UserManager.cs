using smtAzal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smtAzal.Utilities
{
    public class CurrentUser
    {
        private static int _userId;

        public static int UserId
        {
            get { return _userId; }
        }

        public static void SetUserId(int userId)
        {
            _userId = userId;
        }
    }
}
