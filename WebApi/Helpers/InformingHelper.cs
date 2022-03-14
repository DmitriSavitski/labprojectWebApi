using BLL.Models;
using System.Net;
using System.Net.Mail;

namespace WebApi.Helpers
{
	public static class InformingHelper
	{
		public static bool SendInfo(string email, string text, UsersDto user)
		{
			var toAddress = new MailAddress(email);
			var mailProvider = user.EmailDto.Split('@')[1].ToString();
			string subject = "Информирование";

			System.Net.Mail.SmtpClient smtp = null;

			switch (mailProvider)
			{
				case "gmail.com":
					smtp = new SmtpClient
					{
						Host = "smtp.gmail.com",
						Port = 587,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						Credentials = new NetworkCredential(user.EmailDto, user.EmailPasswordDto),
						Timeout = 20000
					};
					break;
				case "yandex.by":
					smtp = new SmtpClient
					{
						Host = "smtp.yandex.com",
						Port = 587,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						Credentials = new NetworkCredential(user.EmailDto, user.EmailPasswordDto),
						Timeout = 20000
					};
					break;
				case "mail.ru":
					smtp = new SmtpClient
					{
						Host = "smtp.mail.ru",
						Port = 2525,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						Credentials = new NetworkCredential(user.EmailDto, user.EmailPasswordDto),
						Timeout = 20000
					};
					break;
			}

			using (var message = new MailMessage(new MailAddress(user.EmailDto), toAddress)
			{
				Subject = subject,
				Body = text
			})
			{
				smtp.Send(message);
			}

			//MailMessage message = new MailMessage(user.EmailDto, email, "Информирование", text);

			//SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
			//client.EnableSsl = true;
			//client.DeliveryMethod = SmtpDeliveryMethod.Network;
			//client.UseDefaultCredentials = false;
			//client.Credentials = new System.Net.NetworkCredential("dmitri.savitskii@gmail.com", "Savok4807781");
			//client.Send(message);

			return true;
		}
	}
}
