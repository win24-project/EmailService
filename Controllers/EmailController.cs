using EmailService.Models;
using EmailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        if (request.Recipients == null || !request.Recipients.Any())
            return BadRequest("At least one recipient is required");

        var status = await _emailService.SendEmailAsync(request);

        return Ok(new { status, recipients = request.Recipients });
    }
}
