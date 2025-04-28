using AutoMapper;
using RS.Core.Application.Dtos.Email;
using RS.Core.Application.Interfaces.Repositories;
using RS.Core.Domain.Settings;

namespace RS.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
