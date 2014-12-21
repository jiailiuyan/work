using System.ComponentModel;
using System.Windows.Media;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// This is the Type for property MetroControl.Identity, and is the expected data type for MetroControl.IdentityTemplate.
    /// </summary>
    public class MetroIdentity : INotifyPropertyChanged
    {
        private ImageSource _ImageSource;
        /// <summary>
        /// Get or set an image source to associate with the user.
        /// </summary>
        public ImageSource ImageSource
        {
            get { return _ImageSource; }
            set
            {
                if (value != _ImageSource)
                {
                    _ImageSource = value;
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        private string _UserName;
        /// <summary>
        /// Get or set the user name.
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (value != _UserName)
                {
                    _UserName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        private string _FirstName;
        /// <summary>
        /// Get or set the user's first name.
        /// </summary>
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (value != _FirstName)
                {
                    _FirstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        private string _LastName;
        /// <summary>
        /// Get or set the user's last name.
        /// </summary>
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (value != _LastName)
                {
                    _LastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        /// <summary>
        /// Called to raise PropertyChanged event.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var h = PropertyChanged;
            if (h != null)
                h(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Implements INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
