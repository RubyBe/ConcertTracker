using Nancy;
using System;
using System.Collections.Generic;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        List<Venue> allVenues = new List<Venue>{};
        return View["index.cshtml", allVenues];
      };
      Get["/bands"] = _ => {
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Get["/venues"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues.cshtml", AllVenues];
      };
      Get["/venues/new"] = _ => {
        return View["venues_form.cshtml"];
      };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["success.cshtml"];
      };
      Get["venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue SelectedVenue = Venue.Find(parameters.id);
        List<Band> VenueBands = SelectedVenue.GetBands();
        List<Band> AllBands = Band.GetAll();
        model.Add("venue", SelectedVenue);
        model.Add("venueBands", VenueBands);
        model.Add("allBands", AllBands);
        return View["venue.cshtml", model];
      };
      Post["venue/add_band"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        venue.AddBand(band);
        return View["success.cshtml"];
      };
      Get["/bands/new"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["bands_form.cshtml", AllVenues];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"], Request.Form["venue-id"]);
        newBand.Save();
        return View["success.cshtml"];
      };
      Get["bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band SelectedBand = Band.Find(parameters.id);
        List<Venue> BandVenues = SelectedBand.GetVenues();
        List<Venue> AllVenues = Venue.GetAll();
        model.Add("band", SelectedBand);
        model.Add("bandVenues", BandVenues);
        model.Add("allVenues", AllVenues);
        return View["band.cshtml", model];
      };
      Post["band/add_venue"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        band.AddVenue(venue);
        return View["success.cshtml"];
      };
    }
  }
}
