namespace Application.User
{
   public class EmailDto
    {
        public string SenderAddress { get; set; }
        public string ReceiverAddress { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }

    }
}