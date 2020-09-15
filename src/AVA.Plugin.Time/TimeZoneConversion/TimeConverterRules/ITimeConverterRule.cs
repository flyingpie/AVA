namespace ExpNodaTime.TimeConverterRules
{
	public interface ITimeConverterRule
	{
		string Name { get; }

		bool TryParse(TimeConverterContext context, out TimeConverterResult result);
	}
}