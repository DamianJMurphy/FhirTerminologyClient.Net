using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FhirTerminologyClient.Net
{
    public class ValueSetValidationRequest : IRequest
    {
        private readonly List<ValidationQuery> requests = new List<ValidationQuery>();
        private byte[] content = null;

        private static readonly string bundleTemplate;
        private static readonly string entryTemplate;
        private static readonly string valueSetTemplate;

        private static readonly string OPERATIONTYPE = "$validate-code";

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

        static ValueSetValidationRequest()
        {
            try
            {
                bundleTemplate = ReadResource("FhirTerminologyClient.Net.BundleTemplate");
                entryTemplate = ReadResource("FhirTerminologyClient.Net.ValueSetValidationEntry");
                valueSetTemplate = ReadResource("FhirTerminologyClient.Net.ValueSet");
            }
            catch (Exception e)
            {
                bootException = e;
            }
        }

        public ValueSetValidationRequest()
        {
            if (bootException != null)
            {
                throw bootException;
            }
        }

        public void AddValidationRequest(ValidationQuery q)
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
                ValidationQuery v = requests[i];
                sb.Append(MakeEntry(v));
                if (i < requests.Count - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        private string MakeEntry(ValidationQuery v)
        {
            string coded = entryTemplate.Replace("__CODE__", v.GetCode());
            if (v.GetValueSet() != null)
            {
                return coded.Replace("__VALUE_SET__", v.GetValueSet());
            }
            string vs = valueSetTemplate.Replace("__ECL_EXPRESSION__", v.GetValueSetExpression());
            vs = vs.Replace("__STATUS__", v.GetStatus());
            return coded.Replace("__VALUE_SET__", vs);
        }

        public AbstractResultSet Query()
        {
            HttpCall c = new HttpCall(this);
            ValidationResultSet r = null;
            try
            {
                Task<string> ts = c.Call();
                ts.Wait();
                string s = ts.Result;
                r = new ValidationResultSet(s);
            }
            catch (Exception e)
            {
                return new ValidationResultSet(e);
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
