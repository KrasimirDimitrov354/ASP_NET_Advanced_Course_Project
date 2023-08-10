namespace SithAcademy.Services.Infrastructure;

using Ganss.Xss;

public class Sanitizer
{
    public string Sanitize(string html = "")
    {
        HtmlSanitizer sanitizer = new HtmlSanitizer();
        string sanitized = sanitizer.Sanitize(html);
        return sanitized;
    }
}
