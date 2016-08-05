using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    // a method to delete records between tests
    public void Dispose()
    {
      Venue.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfNamessAreTheSame()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("The Moore");
      Venue secondVenue = new Venue("The Moore");
      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("The Crocodile");
      //Act
      testVenue.Save();
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};
      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Venue testVenue = new Venue("The Neptune");
      //Act
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];
      int result = savedVenue.GetId();
      int testId = testVenue.GetId();
      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("El Corazon");
      testVenue.Save();
      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());
      //Assert
      Assert.Equal(testVenue, foundVenue);
    }
    [Fact]
    public void Test_Update_UpdatesVenueInDatabase()
    {
      //Arrange
      string name = "The Vogue";
      Venue testVenue = new Venue(name);
      testVenue.Save();
      string newName = "The NiteLite";
      //Act
      testVenue.Update(newName);
      string result = testVenue.GetName();
      //Assert
      Assert.Equal(newName, result);
    }
    [Fact]
    public void Test_Delete_DeletesVenueFromDatabase()
    {
      //Arrange
      string name1 = "El Corazon";
      Venue testVenue1 = new Venue(name1);
      testVenue1.Save();
      string name2 = "The Vogue";
      Venue testVenue2 = new Venue(name2);
      testVenue2.Save();
      //Act
      testVenue1.Delete();
      List<Venue> resultVenues = Venue.GetAll();
      List<Venue> testVenueList = new List<Venue> {testVenue2};
      //Assert
      Assert.Equal(testVenueList, resultVenues);
    }
  }
}
