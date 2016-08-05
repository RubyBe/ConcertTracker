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

    [Fact]
    public void Test_AddVenue_AddsVenueToBand()
    {
      //Arrange
      Band testBand = new Band("The Loud Mouths");
      testBand.Save();
      Venue testVenue1 = new Venue("El Corazon");
      testVenue1.Save();
      Venue testVenue2 = new Venue("The NiteLite");
      testVenue2.Save();
      List<Venue> testList = new List<Venue>{testVenue1, testVenue2};
      //Act
      testBand.AddVenue(testVenue1);
      testBand.AddVenue(testVenue2);
      List<Venue> resultList = testBand.GetVenues();

      //Assert
      Assert.Equal(testList, resultList);
    }
    // [Fact]
    // public void Test_GetBands_ReturnsAllVenueBands()
    // {
    //   //Arrange
    //   Venue testVenue = new Venue("The Catwalk");
    //   testVenue.Save();
    //   Band testBand1 = new Band("The LoudMouths");
    //   testBand1.Save();
    //   Band testBand2 = new Band("The NoiseMakers");
    //   testBand2.Save();
    //   List<Band> testList = new List<Band> {testBand1};
    //   //Act
    //   testVenue.AddBand(testBand1);
    //   List<Band> savedBands = testVenue.GetBands();
    //   //Assert
    //   Assert.Equal(testList, savedBands);
    // }
  }
}
