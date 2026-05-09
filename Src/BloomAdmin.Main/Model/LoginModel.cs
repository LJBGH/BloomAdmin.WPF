using System.Drawing;
using System.Windows.Media.Imaging;
using BloomAdmin.Main.Common;

namespace BloomAdmin.Main.Model
{
    public class LoginModel : NotifyBase
    {
        private string _account;
        public string Account
        {
            get { return _account; }
            set
            {
                _account = value;
                this.DoNotify();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.DoNotify();
            }
        }

        private string _captchaCode;
        public string CaptchaCode
        {
            get { return _captchaCode; }
            set
            {
                _captchaCode = value;
                this.DoNotify();
            }
        }

        private BitmapImage _captchaImage;
        public BitmapImage CaptchaImage
        {
            get { return _captchaImage; }
            set
            {
                _captchaImage = value;
                this.DoNotify();
            }
        }
    }
}
