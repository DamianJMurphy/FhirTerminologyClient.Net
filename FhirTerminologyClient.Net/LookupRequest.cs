using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FhirTerminologyClient.Net
{
    public class LookupRequest : IRequest
    {
        private readonly List<LookupQuery> requests = new List<LookupQuery>();
        private byte[] content = null;

        private static readonly string bundleTemplate;
        private static readonly string entryTemplate;

        private static readonly string OPERATIONTYPE = "$lookup";

#pragma warning disable IDE0044 // Add readonly modifier
        private static Exception bootException = null;
#pragma warning restore IDE0044 // Add readonly modifier


        static string ReadResource(string r)
        {
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(r))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        static LookupRequest()
        {
            try
            {
                bundleTemplate = ReadResource("FhirTerminologyClient.Net.BundleTemplate");
                entryTemplate = ReadResource("FhirTerminologyClient.Net.LookupEntry");
            }
            catch (Exception e)
            {
                bootException = e;
            }
        }

        public LookupRequest()
        {
            if (bootException != null)
            {
                throw bootException;
            }
        }

        public void AddLookupQuery(LookupQuery l)
        {
            requests.Add(l);
        }

        public string GetUrlContextPath()
        {
            return "";
        }

        public bool IsPost()
        {
            return true;
        }

        public void MakeBody()
        {
            string bundle = bundleTemplate.Replace("__ENTRIES__", MakeEntries());
            content = Encoding.UTF8.GetBytes(bundle);
        }

        private string MakeEntries()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < requests.Count; i++)
            {
                LookupQuery v = requests[i];
                sb.Append(MakeEntry(v));
                if (i < requests.Count - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();

        }

        private string MakeEntry(LookupQuery l)
        {
            return entryTemplate.Replace("__CODE__", l.GetCode());
        }

        public AbstractResultSet Query()
        {
            HttpCall c = new HttpCall(this);
            LookupResultSet r = null;
            try
            {
                Task<string> ts = c.Call();
                ts.Wait();
                string s = ts.Result;
                r = new LookupResultSet(s);
            }
            catch (Exception e)
            {
                return new LookupResultSet(e);
            }
            for (int i = 0; i < requests.Count; i++)
            {
                r.AddRequestData(i, requests[i]);
            }
            r.SetOperationType(OPERATIONTYPE);
            return r;
        }

        public byte[] SerialiseContent()
        {
            return content;
        }
    }
}
