using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace InternalSystems.Service
{
    public class CertificateService
    {
        public static bool RemoteCertificateValidationCallback(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            if (sender is System.Net.WebRequest)
            {
                System.Net.WebRequest Request = (System.Net.WebRequest)sender;

                switch (Request.RequestUri.Host)
                {
                    case "localhost":
                        return true;
                }
            }

            return false;
        }

    }
}

