﻿namespace Common.Utils.Models;
public class EmailSetting
{
    public string From { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public int Port { get; set; }
    public string EnableSsl { get; set; } = null!;
}
