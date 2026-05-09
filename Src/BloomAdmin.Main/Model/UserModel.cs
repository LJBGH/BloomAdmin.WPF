using BloomAdmin.Main.Common;
using BloomAdmin.Main.Common.Enums;

namespace BloomAdmin.Main.Model
{
    public class UserModel : NotifyBase
    {
        private string _account;

        public string Account
        {
            get { return _account; }
            set { _account = value; this.DoNotify();}
        }


        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; this.DoNotify();}
        }

        private GenderEnum _gender;

        public GenderEnum Gender
        {
            get { return _gender; }
            set { _gender = value; this.DoNotify();}
        }


        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; this.DoNotify();}
        }

        private string _avatar;

        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; this.DoNotify();}
        }
    }
}
