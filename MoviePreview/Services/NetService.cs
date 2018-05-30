using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Data.Xml.Dom;

namespace MoviePreview.Services
{
    public class NetService
    {
        private async Task<string> Get(string url)
        {
            //Create an HTTP client object
            HttpClient httpClient = new HttpClient();

            Uri requestUri = new Uri(url);

            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = null;
            }
            return httpResponseBody;
        }


        public async Task<JsonObject> GetJson(string url)
        {
            try
            {
                return JsonObject.Parse(await Get(url));
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<XmlDocument> GetXml(string url)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(await Get(url));
            return doc;
        }
    }
}
