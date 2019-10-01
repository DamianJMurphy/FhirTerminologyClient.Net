
namespace FhirTerminologyClient.Net
{
    public class SubsumesQuery : IQueryData
    {
        private string codeA = null;
        private string codeB = null;

        /**
         * @return the codeA
         */
        public string GetCodeA()
        {
            return codeA;
        }

        /**
         * @param codeA the codeA to set
         */
        public void SetCodeA(string c)
        {
            codeA = c;
        }

        /**
         * @return the codeB
         */
        public string GetCodeB()
        {
            return codeB;
        }

        /**
         * @param codeB the codeB to set
         */
        public void SetCodeB(string c)
        {
            codeB = c;
        }
    }
}
