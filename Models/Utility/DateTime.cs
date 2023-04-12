namespace Models.Utility
{
    public static class DateTime
    {
        public static System.DateTime Now
        {
            get
            {
                System.Globalization.CultureInfo currentCulture =
                    System.Threading.Thread.CurrentThread.CurrentCulture;
                try
                {
                    System.Globalization.CultureInfo englishCulture =
                        new System.Globalization.CultureInfo(name: "en-US");

                    System.Threading.Thread.CurrentThread.CurrentCulture = englishCulture;

                    System.DateTime now = System.DateTime.Now;

                    return now;
                }
                finally
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
                }
            }
        }
    }
}
