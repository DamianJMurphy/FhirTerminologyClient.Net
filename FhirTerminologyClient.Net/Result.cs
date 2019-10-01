using System;
using Hl7.Fhir.Model;

namespace FhirTerminologyClient.Net
{
    public class Result
    {
        private Exception exception = null;
        private String status = null;
        private IQueryData requestData = null;
        private OperationOutcome operationOutcome = null;
        private ValueSet valueSet = null;
        private Parameters parameters = null;

        internal Result() { }

        internal void SetValueSet(ValueSet v) { valueSet = v; }
        internal void SetParameters(Parameters p) { parameters = p; }
        internal void SetOperationOutcome(OperationOutcome o) { operationOutcome = o; }

        internal void SetException(Exception e) { exception = e; }
        internal void SetStatus(String s) { status = s; }
        internal void SetRequestData(IQueryData rd) { requestData = rd; }
    }
}
