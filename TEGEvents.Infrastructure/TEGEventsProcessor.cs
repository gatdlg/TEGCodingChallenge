using TEGEvents.Core;

namespace TEGEvents.Infrastructure
{
    public class TEGEventsProcessor
    {
        public static async Task<TEGEventVenue> LoadEvents()
        {
            var url = "https://teg-coding-challenge.s3.ap-southeast-2.amazonaws.com/events/event-data.json";

            using (HttpResponseMessage response = await TEGEventsApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var eventVenue = await response.Content.ReadAsAsync<TEGEventVenue>();
                        MapVenueName(ref eventVenue);
                        return eventVenue;
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private static void MapVenueName(ref TEGEventVenue eventVenue)
        {
            var venues = eventVenue.Venues.ToDictionary(venue => venue.Id, venue => venue);

            foreach (var ev in eventVenue.Events)
            {
                if (!venues.ContainsKey(ev.VenueId))
                {
                    continue;
                }

                var venue = venues[ev.VenueId];

                if (venue != null)
                {
                    ev.VenueName = venue.Name;
                    ev.Location = venue.Location;
                    ev.Capacity = venue.Capacity;
                }
            }
        }
    }
}
