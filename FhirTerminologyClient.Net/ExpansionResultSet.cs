using System;
using Hl7.Fhir.Model;

namespace FhirTerminologyClient.Net
{
    class ExpansionResultSet : AbstractResultSet
    {
        internal ExpansionResultSet(string s)
        {
            Parse(s);
        }

        internal ExpansionResultSet(Exception e)
        {
            exception = e;
        }

        protected override Result GetResult(Bundle.EntryComponent j)
        {
            Result r = new Result();

            try
            {
                string s = j.Response.Status;
                r.SetStatus(s);
                if (s.Equals("200"))
                {
                    ValueSet vs = (ValueSet)j.Resource;
                    r.SetValueSet(vs);
                }
                else
                {
                    OperationOutcome ouch = (OperationOutcome)j.Resource;
                    r.SetOperationOutcome(ouch);
                }
            }
            catch (Exception e)
            {
                r.SetException(e);
            }
            return r;
        }
    }
}
