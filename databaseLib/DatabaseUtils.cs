using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

using Centralite.Database;

namespace CodingUtil
{
    public class DatabaseUtils
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static void InsertEUI(long eui)
        {
            try
            {
                using (ManufacturingStoreEntities cx = new ManufacturingStoreEntities())
                {
                    // Get production site
                    string mac = StationSetupUtility.GetMacAddress();
                    var production_stie_id = cx.StationSites.Where(s => s.StationMac == mac).Single().ProductionSiteId;

                    // db eui is a string
                    string euistr = eui.ToString("X16");

                    // Form query for the eui
                    var q = cx.EuiLists.Where(e => e.EUI == euistr);
                    
                    if(q.Any())
                    {
                        // Check is the same site id
                        int db_site_id = q.Single().ProductionSiteId;
                        if (db_site_id != production_stie_id)
                        {
                            string msg = $"EUI {euistr} already in db with site id = {db_site_id}, this machine is assigned site id {production_stie_id}";
                            _logger.Error(msg);
                            throw new Exception(msg);
                        }
                    }


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
