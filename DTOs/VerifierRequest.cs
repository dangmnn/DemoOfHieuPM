using Microsoft.AspNetCore.Http;

namespace DemoVerify.DTOs
{
    public class VerifierRequest
    {
        public IFormFile Image { get; set; }
    }
}