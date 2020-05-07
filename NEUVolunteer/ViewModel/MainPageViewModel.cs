using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using NEUVolunteer.Implementation;

namespace NEUVolunteer.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(VolunteerImplementation x, ManagerImplementation y)
        {
            _vi = x;
            _vi.Init();
            _mi = y;
        }
        private VolunteerImplementation _vi;
        private ManagerImplementation _mi;

        private Page MainPage => _mainPage ??
          (_mainPage = Application.Current.MainPage as Page);

        private Page _mainPage;

        /******** 继承方法 ********/

        /// <summary>
        /// 显示警告。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="message">信息。</param>
        /// <param name="button">按钮文字。</param>
        public async Task ShowAlertAsync(string title, string message,
            string button) => await MainPage.DisplayAlert(title, message, button);

        private string _username;
        public string UserName
        {
            get => _username;
            set => Set(nameof(UserName), ref _username, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => Set(nameof(Password), ref _password, value);
        }

        private RelayCommand _buttonCommand;

        public RelayCommand ButtonCommand =>
            _buttonCommand ?? (_buttonCommand = new RelayCommand(async () => await ButtonCommandFunction()));

        async Task ButtonCommandFunction()
        {
            Console.WriteLine("OK");
            int z;
            if (_username == null || _password == null || _username == "" || _password == "")
            {
                ShowAlertAsync("登录失败", "用户名或密码不能为空", "确认");
            }
            else if (_username[0] >= 'a' && _username[0] <= 'z' || _username[0] >= 'A' && _username[0] <= 'Z')
            {
                //管理员
                Manager x = await _mi.Getpassword(_username);
                if (x == null || x.password != _password)
                {
                    //登录失败
                    ShowAlertAsync("登录失败", "用户名或密码错误，请输入正确的用户名和密码", "确认");

                }
                else
                {
                    //密码正确，跳转界面
                    ShowAlertAsync("登录成功", "成功", "确认");
                }

            }

            else
            {
                //志愿者
                Volunteer x = await _vi.Getpassword(_username);
                if (x == null || x.password != _password)
                {
                    //登录失败
                    ShowAlertAsync("登录失败", "用户名或密码错误，请输入正确的用户名和密码", "确认");
                }
                else
                {
                    //密码正确，跳转界面
                    ShowAlertAsync("登录成功", "成功", "确认");


                }
            }






        }

    }
}
