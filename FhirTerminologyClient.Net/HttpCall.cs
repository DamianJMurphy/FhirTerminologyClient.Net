using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FhirTerminologyClient.Net
{
    internal class HttpCall
    {
        private Uri url = null;
        private IRequest request = null;

        internal HttpCall(IRequest r)
        {
            request = r;
            StringBuilder sb = new StringBuilder("https://ontoserver.dataproducts.nhs.uk/fhir");
            sb.Append(r.GetUrlContextPath());
            url = new Uri(sb.ToString());
            if (!url.Scheme.Equals("https"))
            {
                throw new Exception("FHIR Terminology Server requires HTTPS: " + url.ToString());
            }
        }

        internal async Task<string> Call()
        {
            HttpResponseMessage response = null;
            if (request.IsPost()) {
                request.MakeBody();
                ByteArrayContent bac = new ByteArrayContent(request.SerialiseContent());
                HttpRequestMessage rq = new HttpRequestMessage();
                rq.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/fhir+json"));
                bac.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/fhir+json");
                bac.Headers.ContentLength = request.SerialiseContent().Length;
                rq.Content = bac;
                rq.Method = HttpMethod.Post;
                rq.RequestUri = url;
                HttpClient client = new HttpClient();
                response = client.SendAsync(rq).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
            }
            else
            {
                // Use GET
            }
            return null;
        }
    }
}
