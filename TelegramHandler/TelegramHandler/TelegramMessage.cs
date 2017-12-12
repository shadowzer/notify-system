namespace TelegramHandler
{
	public class TelegramMessage
	{
		public string chat_id { get; set; }
		public string text { get; set; }

		public override string ToString()
		{
			return "Id: " + chat_id + ", Text: " + text;
		}
	}
}