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

        public static int InsertEUI(long eui)
        {
            try
            {
                using (ManufacturingStoreEntities cx = new ManufacturingStoreEntities())
                {
                    // Get production site
                    string mac = StationSetupUtility.GetMacAddress();
                    var production_stie_id = cx.StationSites.Where(s => s.StationMac == mac).Single().ProductionSiteId;
                    if (production_stie_id == 0)
                        _logger.Warn($"Invalid production site id: {production_stie_id} for MAC: {mac}");

                    // db eui is a string
                    string euistr = eui.ToString("X16");

                    // Form query for the eui
                    var q = cx.EuiLists.Where(e => e.EUI == euistr);
                    
                    if(q.Any())
                    {
                        return q.Single().Id;

                        // I'm not sure we care what site previously coded the device
                        // So I'm removing this for now...
                        // Checking for a valid site id maybe better...

                        // Check is the same site id
                        //int db_site_id = q.Single().ProductionSiteId;
                        //if (db_site_id != production_stie_id)
                        //{
                        //    string msg = $"EUI {euistr} already in db with site id = {db_site_id}, this machine is assigned site id {production_stie_id}";
                        //    _logger.Error(msg);
                        //    throw new Exception(msg);
                        //}
                        //else
                        //{
                        //    return q.Single().Id;
                        //}
                    }


                    EuiList euiList = new EuiList();
                    euiList.EUI = eui.ToString("X16");
                    euiList.ProductionSiteId = production_stie_id;

                    cx.EuiLists.Add(euiList);
                    cx.SaveChanges();

                    return q.Single().Id;

                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception in InsertEUI: {ex.Message}\r\n{ex.StackTrace}");

                if (ex.GetBaseException().GetType() == typeof(SqlException))
                {
                    // I think I originally added this to skip errors when trying to insert already existing EUI
                    // This should no longer happen as we now check for if already exists...So re-throw it
                    SqlException sx = (SqlException)ex.GetBaseException();
                    if (sx.Number == 2627)
                    {
                        //Violation of primary key/Unique constraint
                        throw;
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
