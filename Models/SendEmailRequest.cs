namespace EmailService.Models
{
    public class SendEmailRequest
    {
        public string Subject { get; set; }
        public string TextContent { get; set; }
        public string HtmlContent { get; set; }
        public List<string> Recipients { get; set; }
    }
}
