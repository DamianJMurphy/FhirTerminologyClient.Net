
namespace FhirTerminologyClient.Net
{
    class Testcaller
    {
        static void Main(string[] args)
        {
            //            ValueSetQuery v1 = new ValueSetQuery();
            //            v1.SetValueSetExpression("<!272141005");
            //            ValueSetExpansionRequest vser = new ValueSetExpansionRequest();
            //            vser.AddExpansionRequest(v1);
            //            ExpansionResultSet esr = (ExpansionResultSet)vser.Query();
            //            LookupQuery l1 = new LookupQuery();
            //            l1.SetCode("371923003");
            //            LookupQuery l2 = new LookupQuery();
            //            l2.SetCode("ABCDEF");
            //            LookupQuery l3 = new LookupQuery();
            //            l3.SetCode("419048002");
            //            LookupRequest lq = new LookupRequest();
            //            lq.AddLookupQuery(l1);
            //            lq.AddLookupQuery(l2);
            //            lq.AddLookupQuery(l3);
            //                    ValidationQuery v1 = new ValidationQuery();
            //                    v1.SetValueSetExpression("<!272141005");
            //                    v1.SetCode("371923003");
            //            ValidationQuery v2 = new ValidationQuery();
            //                    v2.SetValueSetExpression("<!272379006");
            //            v2.SetCode("371923003");
            //            ValidationQuery v3 = new ValidationQuery();
            //            ValueSetValidationRequest vsvr = new ValueSetValidationRequest();
            //            vsvr.AddValidationRequest(v1);
            //            vsvr.AddValidationRequest(v2);
            //            ValidationResultSet vsr = (ValidationResultSet)vsvr.Query();
            SubsumesQuery v1 = new SubsumesQuery();
            v1.SetCodeA("419048002");
            v1.SetCodeB("272379006");
            SubsumesQuery v2 = new SubsumesQuery();
            //        v2.setCodeA("272379006");
            v2.SetCodeA("ABCDEF");
            v2.SetCodeB("419048002");
            SubsumesQuery v3 = new SubsumesQuery();
            v3.SetCodeA("272379006");
            v3.SetCodeB("27123005");
            SubsumesRequest sr = new SubsumesRequest();
            sr.AddSubsumesQuery(v1);
            sr.AddSubsumesQuery(v2);
            sr.AddSubsumesQuery(v3);
            SubsumesQuery v4 = new SubsumesQuery();
            v4.SetCodeB("419048002");
            v4.SetCodeA("272379006");
            sr.AddSubsumesQuery(v4);
            SubsumesResultSet srs = (SubsumesResultSet)sr.Query();

            //            LookupResultSet lrs = (LookupResultSet)lq.Query();
            int i = 0;
        }
    }
}
