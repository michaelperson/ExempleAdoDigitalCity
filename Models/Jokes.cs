namespace Models
{
    public class Jokes
    {
        private int _id;
        private string _title;
        private string _body;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        public string Body
        {
            get
            {
                return _body;
            }

            set
            {
                _body = value;
            }
        }
    }
}
