using fickle;

namespace AlertDialog
{
    internal class Builder
    {
        public Builder(MainPage mainPage)
        {
            MainPage = mainPage;
        }

        public MainPage MainPage { get; }
    }
}