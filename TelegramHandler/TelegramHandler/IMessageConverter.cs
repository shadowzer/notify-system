namespace TelegramHandler
{
	public interface IMessageConverter
	{
		object Convert(Message message);
	}
}