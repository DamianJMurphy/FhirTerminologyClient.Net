using System;
using System.Collections.Generic;

namespace FhirTerminologyClient.Net
{
    public class Issue
    {
        private String code = null;
        private String diagnostics = null;
        private String severity = null;
        private List<string> expression = null;

        public Issue(String c, String d, String s)
        {
            code = c;
            diagnostics = d;
            severity = s;
        }

        public void AddExpression(String s)
        {
            if (expression == null)
            {
                expression = new List<string>();
            }
            expression.Add(s);
        }

        public List<string> GetExpressions()
        {
            if (expression == null)
            {
                return null;
            }
            return expression;
        }

        /**
         * @return the code
         */
        public String GetCode()
        {
            return code;
        }

        /**
         * @param code the code to set
         */
        public void SetCode(String code)
        {
            this.code = code;
        }

        /**
         * @return the diagnostics
         */
        public String GetDiagnostics()
        {
            return diagnostics;
        }

        /**
         * @param diagnostics the diagnostics to set
         */
        public void SetDiagnostics(String diagnostics)
        {
            this.diagnostics = diagnostics;
        }

        /**
         * @return the severity
         */
        public String GetSeverity()
        {
            return severity;
        }

        /**
         * @param severity the severity to set
         */
        public void SetSeverity(String severity)
        {
            this.severity = severity;
        }
    }
}
