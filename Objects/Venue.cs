using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Venue
  {
    // Properties
    private int _id;
    private string _name;

    // Constructors, getters, setters
    public Venue(string name, int Id = 0)
    {
      _name = name;
      _id = Id;
    }
    // Other methods
    // a method to get all Venue records
    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue newVenue = new Venue(name, id);
        allVenues.Add(newVenue);
      }
      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }
      return allVenues;
    }
    // a method to delete all Venue records
    public static void DeleteAll()
    {
      return;
    }
  }
}
