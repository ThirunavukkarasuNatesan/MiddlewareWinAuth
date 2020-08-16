using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MiddlewareWinAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            var host = new WebHostBuilder()
                //configure Kestrel web server
                .UseKestrel(options =>
                {
                    //configure certificates
                    options.Listen(IPAddress.Loopback, 4430, listenOptions =>
                    {
                        //client certificate configuration
                        var httpsOptions = new HttpsConnectionAdapterOptions();
                        httpsOptions.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                        httpsOptions.CheckCertificateRevocation = true;
                        httpsOptions.ClientCertificateValidation +=
                            (certificate2, chain, arg3) =>
                            {
                                //this is where we verify the thumbprint of a connected client matches the thumbprint we expect
                                //NOTE: this is just a simple example of verifying a client cert.
                                return certificate2.Thumbprint.Equals(config["clientcertificate"],
                                    StringComparison.InvariantCultureIgnoreCase);
                            };

                        //server certificate configuration
                        //in this case we're loading the cert from a file
                        //this Server cert was created using IIS, but can be created using `makecert.exe` as well
                        var certBuffer = File.ReadAllBytes(@"Certificate/localhost.pfx");
                        var serverCert = new X509Certificate2(certBuffer, "123");

                        httpsOptions.ServerCertificate = serverCert;
                        listenOptions.UseHttps(httpsOptions);
                    });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                //.UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
