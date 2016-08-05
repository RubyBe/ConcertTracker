using Nancy;

namespace ConcertTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => "Hey Hey Hello";
    }
  }
}
