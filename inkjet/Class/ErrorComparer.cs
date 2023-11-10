using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace inkjet.Class
{
    public class ErrorComparer
    {
        public class PersonComparer : IEqualityComparer<Error>
        {
            public bool Equals(Error x, Error y)
            {
                if (x.ErrorCode == y.ErrorCode && x.Status == y.Status)
                    return true;

                return false;
            }

            public int GetHashCode(Error obj)
            {
                return obj.ErrorCode.GetHashCode();
            }
        }
    }
}
