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

    string strConStr = System.Configuration.ConfigurationManager.ConnectionStrings["cnMetrics"].ToString();


    public DateTime CalenderStuff(string sDate, int duration, List<DateTime> theDates)
    {
        string[] dateParts = sDate.Split('/');
        string[] yearParts = dateParts[2].Split(' ');
        DateTime startDate = new DateTime(int.Parse(yearParts[0]), int.Parse(dateParts[0]), int.Parse(dateParts[1]));

        DateTime curDate;
        DateTime lastDate;
        int numDays;

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



    public DateTime GetLastDate(string sDate, int duration)
    {
        string[] dateParts = sDate.Split('/');
        string[] yearParts = dateParts[2].Split(' ');
        DateTime startDate = new DateTime(int.Parse(yearParts[0]), int.Parse(dateParts[0]), int.Parse(dateParts[1]));

        DateTime curDate;
        DateTime lastDate = DateTime.Now;
        int numDays;

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


    public DataTable GetProjects()
    {
        DataSet ds = new DataSet();

        try
        {
            SqlConnection conn = new SqlConnection(strConStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT DISTINCT ProjectName FROM Projects", conn);
            SqlDataReader dr = cmd.ExecuteReader();            

            ds.Tables.Add(new DataTable());
            ds.Load(dr, LoadOption.PreserveChanges, ds.Tables[0]);

            conn.Close();
        }
        catch (Exception)
        {
        }

        return ds.Tables[0];
    }


    public DataTable GetProjectDates(string projectName)
    {
        DataSet ds = new DataSet();

        try
        {
            SqlConnection conn = new SqlConnection(strConStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT StartDate FROM Projects WHERE ProjectName='" + projectName.Replace("'", "''") + "'", conn);
            SqlDataReader dr = cmd.ExecuteReader();

            ds.Tables.Add(new DataTable());
            ds.Load(dr, LoadOption.PreserveChanges, ds.Tables[0]);

            conn.Close();
        }
        catch (Exception)
        {
        }

        return ds.Tables[0];
    }

    
    
}