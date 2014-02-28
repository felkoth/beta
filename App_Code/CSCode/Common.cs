using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// C# code used by vb project
/// </summary>
public class Common
{
	public Common()
	{
		// TODO: Add constructor logic here
	}

    /// <summary>
    ///     connection string to local DB
    /// </summary>
    string strConStr = System.Configuration.ConfigurationManager.ConnectionStrings["cnMetrics"].ToString();

    /// <summary>
    ///     populate list of project dates excluding weekends
    /// </summary>
    public DateTime CalenderStuff(string sDate, int duration, List<DateTime> theDates)
    {
        string dateFormat = "";


        // format startdate to MM/dd/yy, else use current date
        try
        {
            dateFormat = DateTime.Parse(sDate).ToString("MM/dd/yyyy");
        }
        catch (Exception)
        {

            dateFormat = DateTime.Now.ToString("MM/dd/yyyy");
        }



        // check for date delimiters
        string[] dateParts;

        if (dateFormat.Contains('-'))
        {
            dateParts = dateFormat.Split('-');
        }
        else
        {
            dateParts = dateFormat.Split('/');
        }  



        // create new DateTime object in format mm/dd/yyy
        string[] yearParts = dateParts[2].Split(' ');
        DateTime startDate = new DateTime(int.Parse(yearParts[0]), int.Parse(dateParts[0]), int.Parse(dateParts[1]));

        DateTime curDate;
        DateTime lastDate;
        int numDays;



        // iterate through number of days and skip weekends
        for (int ii = 0; ii < duration; ii++)
        {
            curDate = startDate.AddDays(ii);
            numDays = ii;

            if (curDate.DayOfWeek == DayOfWeek.Saturday)
            {
                numDays = ii + 2;
                duration += 2;
            }

            if (curDate.DayOfWeek == DayOfWeek.Sunday)
                numDays = ii + 1;

            theDates.Add(startDate.AddDays(numDays));
        }


        // check if list has dates
        if (theDates.Count != 0)
        {
            lastDate = theDates.Last();
        }
        else
        {
            lastDate = DateTime.Now;
        }

        return lastDate;
    }


    /// <summary>
    ///     calculate end date of project, excluding weekends
    /// </summary>
    public DateTime GetLastDate(string sDate, int duration)
    {
        string dateFormat = "";

        // format startdate to MM/dd/yy, else use current date
        try
        {
            dateFormat = DateTime.Parse(sDate).ToString("MM/dd/yyyy");
        }
        catch (Exception)
        {

            dateFormat = DateTime.Now.ToString("MM/dd/yyyy");
        }



        // check for date delimiters
        string[] dateParts;

        if (dateFormat.Contains('-'))
        {
            dateParts = dateFormat.Split('-');
        }
        else
        {
            dateParts = dateFormat.Split('/');
        }


        // create new DateTime object in format mm/dd/yyy
        string[] yearParts = dateParts[2].Split(' ');
        DateTime startDate = new DateTime(int.Parse(yearParts[0]), int.Parse(dateParts[0]), int.Parse(dateParts[1]));

        DateTime curDate;
        DateTime lastDate = DateTime.Now;
        int numDays;


        // iterate through number of days and skip weekends
        for (int ii = 0; ii < duration; ii++)
        {
            curDate = startDate.AddDays(ii);
            numDays = ii;

            if (curDate.DayOfWeek == DayOfWeek.Saturday)
            {
                numDays = ii + 2;
                duration += 2;
            }

            if (curDate.DayOfWeek == DayOfWeek.Sunday)
                numDays = ii + 1;

            lastDate = startDate.AddDays(numDays);
        }

        return lastDate;
    }


    //public DataTable GetProjects()
    //{
    //    DataSet ds = new DataSet();

    //    try
    //    {
    //        SqlConnection conn = new SqlConnection(strConStr);
    //        conn.Open();

    //        SqlCommand cmd = new SqlCommand("SELECT DISTINCT ProjectName FROM Projects", conn);
    //        SqlDataReader dr = cmd.ExecuteReader();            

    //        ds.Tables.Add(new DataTable());
    //        ds.Load(dr, LoadOption.PreserveChanges, ds.Tables[0]);

    //        conn.Close();
    //    }
    //    catch (Exception)
    //    {
    //    }

    //    return ds.Tables[0];
    //}


    //public DataTable GetProjectDates(string projectName)
    //{
    //    DataSet ds = new DataSet();

    //    try
    //    {
    //        SqlConnection conn = new SqlConnection(strConStr);
    //        conn.Open();

    //        SqlCommand cmd = new SqlCommand("SELECT StartDate FROM Projects WHERE ProjectName='" + projectName.Replace("'", "''") + "'", conn);
    //        SqlDataReader dr = cmd.ExecuteReader();

    //        ds.Tables.Add(new DataTable());
    //        ds.Load(dr, LoadOption.PreserveChanges, ds.Tables[0]);

    //        conn.Close();
    //    }
    //    catch (Exception)
    //    {
    //    }

    //    return ds.Tables[0];
    //}

    
    
}