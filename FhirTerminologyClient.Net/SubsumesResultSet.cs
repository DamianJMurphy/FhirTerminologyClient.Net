using System;
using Hl7.Fhir.Model;

namespace FhirTerminologyClient.Net
{
    public class SubsumesResultSet : AbstractResultSet
    {
        internal SubsumesResultSet(string s)
        {
            Parse(s);
        }

        internal SubsumesResultSet(Exception e)
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
                    Parameters p = (Parameters)j.Resource;
                    r.SetParameters(p);
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
