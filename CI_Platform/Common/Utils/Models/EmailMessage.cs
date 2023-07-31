using Microsoft.AspNetCore.Http;
using MimeKit;
namespace Common.Utils.Models;

public class EmailMessage
{
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public IFormFileCollection? Attachments { get; set; }

    public EmailMessage(IEnumerable<string> to, string subject, string content, IFormFileCollection? attachments = null)
    {
        To = new List<MailboxAddress>();

        To.AddRange(to.Select(x => new MailboxAddress("email", x)));
        Subject = subject;
        Content = content;
        Attachments = attachments;
    }
}
