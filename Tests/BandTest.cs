using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    // a method to delete records between tests
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
    [Fact]
    public void Test_BandsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Band firstBand = new Band("The LoudMouths");
      Band secondBand = new Band("The LoudMouths");
      //Assert
      Assert.Equal(firstBand, secondBand);
    }
    [Fact]
    public void Test_Save_SavesBandToDatabase()
    {
      //Arrange
      Band testBand = new Band("The LoudMouths");
      testBand.Save();
      List<Band> testList = new List<Band>{testBand};
      //Act
      List<Band> resultList = Band.GetAll();
      //Assert
      Assert.Equal(testList, resultList);
    }
    [Fact]
    public void Test_Save_AssignsIdToBandObject()
    {
      //Arrange
      Band testBand = new Band("The Rough Nighters");
      testBand.Save();
      //Act
      Band savedBand = Band.GetAll()[0];
      int result = savedBand.GetId();
      int testId = testBand.GetId();
      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
    public void Test_Find_FindsBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("The NoiseMakers");
      testBand.Save();
      //Act
      Band foundBand = Band.Find(testBand.GetId());
      //Assert
      Assert.Equal(testBand, foundBand);
    }
  }
}
