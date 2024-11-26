using SCME.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCME.dbViewer
{
    public static class CommonResources
    {
        //источник данных для combobox-ов демонстрации и выбора статуса изделия по протоколу испытаний
        public static List<DbRoutines.StatusByAssemblyProtocol> DataSourceOfStatusByAssemblyProtocol = new List<DbRoutines.StatusByAssemblyProtocol>();
    }
}
