namespace DataDapper.Tools
{
	public class Options : object
	{
		public Options(string connectionString) : base()
		{
            ConnectionString = connectionString;
        }

		// **********
		public string ConnectionString { get; set; }
		// **********
	}
}
