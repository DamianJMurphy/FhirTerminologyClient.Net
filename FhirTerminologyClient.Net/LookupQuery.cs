
namespace FhirTerminologyClient.Net
{
    public class LookupQuery : IQueryData
    {
        private string code = null;

        /**
         * @return the code
         */
        public string GetCode()
        {
            return code;
        }

        /**
         * @param code the code to set
         */
        public void SetCode(string c)
        {
            code = c;
        }

    }
}
