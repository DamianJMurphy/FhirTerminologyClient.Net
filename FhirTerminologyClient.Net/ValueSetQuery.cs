
namespace FhirTerminologyClient.Net
{
    public class ValueSetQuery : IQueryData
    {
        protected string valueSetExpression = null;
        protected string valueSet = null;
        protected string status = "active";

        public ValueSetQuery() { }

        /**
         * @return the valueSetExpression
         */
        public string GetValueSetExpression()
        {
            return valueSetExpression;
        }

        /**
         * @return the valueSet
         */
        public string GetValueSet()
        {
            return valueSet;
        }

        /**
         * @param valueSetExpression the valueSetExpression to set
         */
        public void SetValueSetExpression(string v)
        {
            valueSetExpression = v;
        }

        /**
         * @param valueSet the valueSet to set
         */
        public void SetValueSet(string v)
        {
            valueSet = v;
        }

        /**
         * @return the status
         */
        public string GetStatus()
        {
            return status;
        }

        /**
         * @param status the status to set
         */
        public void SetStatus(string s)
        {
            status = s;
        }

    }
}
