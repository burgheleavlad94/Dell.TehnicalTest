using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Dell.Data.Entities
{
    public interface IIdentifiable
    {
        int ID { get; set; }
        byte[] RecordVersion { get; set; }

        void ReadFromReader(DbDataReader reader);
    }
}
