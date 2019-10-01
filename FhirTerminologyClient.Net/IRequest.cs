
namespace FhirTerminologyClient.Net
{
    public interface IRequest
    {
        string GetUrlContextPath();
        void MakeBody();
        byte[] SerialiseContent();

        bool IsPost();

        /**
         *
         * @return
         * @throws Exception
         */
        AbstractResultSet Query();

    }
}
