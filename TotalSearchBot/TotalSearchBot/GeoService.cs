using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace TotalSearchBot
{
    public class GeoLocation
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }

    public class GeoGeometry
    {
        public GeoLocation Location { get; set; }
    }

    public class GeoResult
    {
        public GeoGeometry Geometry { get; set; }
    }

    public class GeoResponse
    {
        public string Status { get; set; }
        public GeoResult[] Results { get; set; }
    }

    public static class GeoService
    {

        public static decimal[] GetGeolocation(string adresse)
        {
            string url = "http://maps.googleapis.com/maps/api/geocode/" +
                "json?address=" + adresse + "&sensor=false";

            WebResponse response = null;
            decimal[] location = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = request.GetResponse();
                if (response != null)
                {
                    string str = null;
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(stream))
                        {
                            str = streamReader.ReadToEnd();
                        }
                    }

                    GeoResponse geoResponse = JsonConvert.DeserializeObject<GeoResponse>(str);
                    if (geoResponse.Status == "OK")
                    {
                        if (geoResponse.Results.Length > 0)
                        {
                            location = new decimal[2] 
                            {
                                geoResponse.Results[0].Geometry.Location.Lat,
                                geoResponse.Results[0].Geometry.Location.Lng
                            };                          
                        }
                    } 
                    else
                    {
                        //Console.WriteLine ("JSON response failed, status is '{0}'", geoResponse.Status);
                    }
                }
            } 
            catch (Exception ex)
            {
                //Console.WriteLine (ex.Message);
            } 
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return location;

        
        }

    }
}
