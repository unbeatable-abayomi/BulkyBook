using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
	public class EmailSender : IEmailSender
	{
		private readonly EmailOptions emailOptions;

		public EmailSender(IOptions<EmailOptions> options)
		{
			emailOptions = options.Value;

		}
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			return Execute(emailOptions.SendGridKey, subject, htmlMessage, email);
		}

		private Task Execute(string sendGridkey, string subject, string message, string email)
		{
			//var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
			var client = new SendGridClient(sendGridkey);
			var from = new EmailAddress("admin@bulkybooks.com", "Bulky Books");
		  
			var to = new EmailAddress(email, "User End");
			//var plainTextContent = "and easy to do anywhere, even with C#";
			//var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
			var msg = MailHelper.CreateSingleEmail(from, to, subject, message,"");
		   return client.SendEmailAsync(msg);
		}
	}
}
