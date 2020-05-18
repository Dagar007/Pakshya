using System.Threading.Tasks;
using Application.Interfaces;
using Application.User;
using Microsoft.Extensions.Options;
using Amazon.SimpleEmail;
using Amazon;
using Amazon.SimpleEmail.Model;
using System.Collections.Generic;
using System;

namespace Infrastructure.Security
{
    public class EmailServiceAWS : IEmailService
    {
        private readonly IOptions<AwsSettings> _settings;
        private readonly string _key;
        private readonly string _secret;
        private readonly string _region;
        public EmailServiceAWS(IOptions<AwsSettings> settings)
        {
            _settings = settings;
            _key = settings.Value.AccessKey;
            _region = settings.Value.Region;
            _secret = settings.Value.Secret;

        }
        public async Task SendEmail(EmailDto email)
        {
            using (var client = new AmazonSimpleEmailServiceClient(_key, _secret, RegionEndpoint.APSouth1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = email.SenderAddress,
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { email.ReceiverAddress }
                    },
                    Message = new Message
                    {
                        Subject = new Content(email.Subject),
                        Body = new Body
                        {
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = email.TextBody
                            }
                        }
                    }

                };

                try
                {
                    var response = await client.SendEmailAsync(sendRequest);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}