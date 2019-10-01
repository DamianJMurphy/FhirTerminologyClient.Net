using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace FhirTerminologyClient.Net
{
    public abstract class AbstractResultSet
    {
        protected List<Result> results = null;
        protected Exception exception = null;
        protected string operationType = null;
        protected string fhirData = null;
        protected DateTime performedDate = DateTime.Now;

        protected abstract Result GetResult(Bundle.EntryComponent j);

        protected void Parse(string s)
        {
            results = new List<Result>();
            fhirData = s;
            try
            {
                FhirJsonParser jp = new FhirJsonParser();
                Bundle b = jp.Parse<Bundle>(s);
                
                foreach (Bundle.EntryComponent entry in b.Entry)
                {
                    Result r = GetResult(entry);
                    results.Add(r);

                }                
            }
            catch (Exception e)
            {
                exception = e;
            }
        }

        internal void AddRequestData(int n, IQueryData rd)
        {
            results[n].SetRequestData(rd);
        }

        public DateTime GetPerformedDate() { return performedDate; }
        public string GetFhirData() { return fhirData; }

        internal void SetOperationType(string t) { operationType = t; }
        public bool IsError() { return (exception != null); }
        public Exception GetException() { return exception; }
        public string GetError()
        {
            if (exception == null)
                return null;
            return exception.Message;
        }
        public string GetErrorDetails()
        {
            if (exception == null)
                return null;
            StringBuilder sb = new StringBuilder(exception.ToString());
            Exception e = exception.InnerException;
            while (e != null)
            {
                sb.Append(e.ToString());
                e = e.InnerException;
            }
            return sb.ToString();
        }
    }
}
