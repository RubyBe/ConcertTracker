using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class Band
    {
      // Properties
      private int _id;
      private string _name;

      // Constructors, getters, setters
      public Band(string name, int Id = 0)
      {
        _name = name;
        _id = Id;
      }
      public int GetId()
      {
        return _id;
      }
      public string GetName()
      {
        return _name;
      }
      public void SetName(string name)
      {
        _name = name;
      }
      // Other methods
      // a method to save an instance of the band object to the database
      public void Save()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandName);", conn);
        SqlParameter nameParameter = new SqlParameter();
        nameParameter.ParameterName = "@BandName";
        nameParameter.Value = this.GetName();
        cmd.Parameters.Add(nameParameter);
        SqlDataReader rdr = cmd.ExecuteReader();
        while(rdr.Read())
        {
          this._id = rdr.GetInt32(0);
        }
        if (rdr != null)
        {
          rdr.Close();
        }
        if(conn != null)
        {
          conn.Close();
        }
      }
      // a method to override Equality and test the equivalence of name and id instead
      public override bool Equals(System.Object otherBand)
      {
        if (!(otherBand is Band))
        {
          return false;
        }
        else
        {
          Band newBand = (Band) otherBand;
          bool idEquality = this.GetId() == newBand.GetId();
          bool nameEquality = this.GetName() == newBand.GetName();
          return (idEquality && nameEquality);
        }
      }
      // a method to find a single record using the instance id
      public static Band Find(int id)
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);
        SqlParameter bandIdParameter = new SqlParameter();
        bandIdParameter.ParameterName = "@BandId";
        bandIdParameter.Value = id.ToString();
        cmd.Parameters.Add(bandIdParameter);
        SqlDataReader rdr = cmd.ExecuteReader();

        int foundBandId = 0;
        string foundBandName = null;

        while(rdr.Read())
        {
          foundBandId = rdr.GetInt32(0);
          foundBandName = rdr.GetString(1);
        }
        Band foundBand = new Band(foundBandName, foundBandId);

        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return foundBand;
      }
      // a method to return all Band object instances in a list
      public static List<Band> GetAll()
      {
        List<Band> allBands = new List<Band>{};
        SqlConnection conn = DB.Connection();
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          int bandId = rdr.GetInt32(0);
          string bandName = rdr.GetString(1);
          Band newBand = new Band(bandName, bandId);
          allBands.Add(newBand);
        }
        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return allBands;
      }
      // a method to delete all instances of Band
      public static void DeleteAll()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();
        SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
        cmd.ExecuteNonQuery();
        conn.Close();
      }
    }
}
