﻿using System.Threading.Tasks;

namespace SRA.ApiRest.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string message);
    }
}
