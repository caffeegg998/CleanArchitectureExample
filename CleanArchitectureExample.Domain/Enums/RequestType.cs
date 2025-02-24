using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Enums
{
    public enum RequestType
    {
        InQueue,    // Đang trong hàng đợi
        FinishedGood,   // Đã nhập kho
        PDC2Received  // PDC2 đã nhận hàng
    }
}
