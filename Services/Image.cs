namespace Akan.Services
{
    public class Image
    {
        private string _url;

        public Image(string url)
        {
           _url = url;
        }

        public string getUrl()
        {
            return _url;
        }
    }
}