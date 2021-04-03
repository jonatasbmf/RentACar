using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.Data
{
    public interface ISQLite
    {
        SQLiteConnection PegarConexao();
    }
}
