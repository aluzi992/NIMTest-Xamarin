using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Com.Netease.Nimlib.Sdk;
using Com.Netease.Nimlib.Sdk.Auth;
using Com.Netease.Nimlib.Sdk.Msg;
using Java.Lang;
using Com.Netease.Nimlib.Sdk.Msg.Model;
using Com.Netease.Nimlib.Sdk.Msg.Constant;

namespace NIMTest.Droid
{
    [Activity(Label = "NIMTest", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            LoginInfo info = new LoginInfo("liuying", "password");
            SDKOptions sdkOptions = new SDKOptions();
            sdkOptions.AppKey = "2195afcf73bed16e01db1c51d1f1d5e5";            
            NIMClient.Init(this.ApplicationContext, info, sdkOptions);

            var s = NIMClient.GetService(Java.Lang.Class.FromType(typeof(IAuthService)));
            var s2 = s.JavaCast<IAuthService>();
            if (s2 != null)
            {
                var callback = new NIMLoginCallback();
                var f = s2.Login(info);//This will crash!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                f.SetCallback(callback);
            }

            /*
            var s3 = NIMClient.GetService(Java.Lang.Class.FromType(typeof(IMsgServiceObserve)));
            var s4 = (s3 as IMsgServiceObserve);
            var observe = new NIMMsgStatusObserve();
            s4.ObserveMsgStatus(observe, false);

            var s5 = NIMClient.GetService(Java.Lang.Class.FromType(typeof(IMsgService)));
            var s6 = (s5 as IMsgService);
            var callback2 = new NIMLoginCallback();
            IMMessage newMsg = MessageBuilder.CreateTextMessage("liubin", SessionTypeEnum.P2p, "测试信息-来自Xamarin");
            s6.SendMessage(newMsg, false).SetCallback(callback2);
            */
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    class NIMMsgStatusObserve : Java.Lang.Object, IObserver
    {
        public void OnEvent(Java.Lang.Object p0)
        {
            Console.WriteLine($"NIMMsgStatusObserve - OnEvent:{p0}");
        }
    }

    class NIMLoginCallback : Java.Lang.Object, IRequestCallback
    {
        public void OnException(Throwable p0)
        {
            Console.WriteLine($"-----------------NIMLoginCallback OnException:{p0}");
        }

        public void OnFailed(int p0)
        {
            Console.WriteLine($"----------------NIMLoginCallback OnFailed:{p0}");
        }

        public void OnSuccess(Java.Lang.Object p0)
        {
            Console.WriteLine($"----------------NIMLoginCallback 成功！");
        }
    }
}