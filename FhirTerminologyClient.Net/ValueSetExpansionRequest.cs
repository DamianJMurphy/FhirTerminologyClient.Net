using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FhirTerminologyClient.Net
{
    public class ValueSetExpansionRequest : IRequest
    {
        private readonly List<ValueSetQuery> requests = new List<ValueSetQuery>();
        private byte[] content = null;

        private static readonly string bundleTemplate;
        private static readonly string entryTemplate;
        private static readonly string valueSetTemplate;

        private static readonly string OPERATIONTYPE = "$expand";

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

        static ValueSetExpansionRequest()
        {
            try
            {
                bundleTemplate = ReadResource("FhirTerminologyClient.Net.BundleTemplate");
                entryTemplate = ReadResource("FhirTerminologyClient.Net.ValueSetExpansionEntry");
                valueSetTemplate = ReadResource("FhirTerminologyClient.Net.ValueSet");
            }
            catch (Exception e)
            {
                bootException = e;
            }
        }

        public ValueSetExpansionRequest()
        {
            if (bootException != null)
            {
                throw bootException;
            }
        }

        public void AddExpansionRequest(ValueSetQuery q)
        {
            requests.Add(q);
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
                ValueSetQuery v = requests[i];
                sb.Append(MakeEntry(v));
                if (i < requests.Count - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        private string MakeEntry(ValueSetQuery v)
        {
            if (v.GetValueSet() != null)
            {
                return entryTemplate.Replace("__VALUE_SET__", v.GetValueSet());
            }
            String vs = valueSetTemplate.Replace("__ECL_EXPRESSION__", v.GetValueSetExpression());
            vs = vs.Replace("__STATUS__", v.GetStatus());
            return entryTemplate.Replace("__VALUE_SET__", vs);
        }

        public AbstractResultSet Query()
        {
            HttpCall c = new HttpCall(this);
            ExpansionResultSet r = null;
            try
            {
                Task<string> ts = c.Call();
                ts.Wait();
                string s = ts.Result;
                r = new ExpansionResultSet(s);
            }
            catch (Exception e)
            {
                return new ExpansionResultSet(e);
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
