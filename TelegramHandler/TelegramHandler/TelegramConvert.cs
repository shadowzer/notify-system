namespace TelegramHandler
{
	public class TelegramConvert : IMessageConverter
	{
		public object Convert(Message message) => new TelegramMessage
		{
			chat_id = message.Id,
			text = message.Text
		};
	}
}