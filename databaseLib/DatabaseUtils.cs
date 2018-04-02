using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Centralite.Database;

namespace CodingUtil
{
    public class DatabaseUtils
    {
        public static void InsertEUI(long eui)
        {
            try
            {
                using (ManufacturingStoreEntities cx = new ManufacturingStoreEntities())
                {
                    string mac = StationSetupUtility.GetMacAddress();
                    var production_stie_id = cx.StationSites.Where(s => s.StationMac == mac).Single().ProductionSiteId;

                    string euistr = eui.ToString("X16");

                    cx.EuiLists.Where(e => e.EUI == euistr)


                    EuiList euiList = new EuiList();
                    euiList.EUI = eui.ToString("X16");
                    euiList.ProductionSiteId = production_stie_id;

                    cx.EuiLists.Add(euiList);
                    cx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().GetType() == typeof(SqlException))
                {
                    SqlException sx = (SqlException)ex.GetBaseException();
                    if (sx.Number == 2627)
                    {
                        //Violation of primary key/Unique constraint
                        // 
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }

        }
    }
}
