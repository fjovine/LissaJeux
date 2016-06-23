using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lissajous
{
    public interface IFallible
    {
        bool IsError
        {
            get;
        }

        bool Enable
        {
            get;
            set;
        }
    }
}
