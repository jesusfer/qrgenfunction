using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QRCoder;

namespace QRGenFunction
{
    public static class QRGenerator
    {
        [FunctionName("qrgen")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string data = req.Query["data"];

            if(string.IsNullOrEmpty(data))
            {
                return new BadRequestResult();
            }

            // Generate de QRCode
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            // Save the QRCode as a image and send it in the response.
            var qrCodeBytes = qrCode.GetGraphic(10);
            return new FileContentResult(qrCodeBytes, "image/png");
        }
    }
}
