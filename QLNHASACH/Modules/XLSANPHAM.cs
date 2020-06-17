using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNHASACH.Modules
{
    class XLSANPHAM: XLBANG
    {
        public XLSANPHAM() : base("NSX") { }
        public XLSANPHAM( string pQuery) : base("NSX",pQuery) { }
    }
}
