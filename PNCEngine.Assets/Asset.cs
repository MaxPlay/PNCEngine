namespace PNCEngine.Assets
{
    public abstract class Asset<T> where T : class
    {
        #region Protected Fields

        protected string filename;
        protected T resource;

        #endregion Protected Fields

        #region Private Fields

        private static long nextID;
        private long id;

        #endregion Private Fields

        #region Public Constructors

        public Asset(string filename)
        {
            id = nextID++;
            this.filename = filename;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Filename
        {
            get { return filename; }
        }

        public long ID
        {
            get { return id; }
        }

        public T Resource
        {
            get { return resource; }
        }

        #endregion Public Properties

        #region Public Methods

        public abstract Asset<T> Clone();

        public abstract bool Load();

        #endregion Public Methods

        #region Protected Methods

        protected void assignNewID()
        {
            id = nextID++;
        }

        #endregion Protected Methods
    }
}