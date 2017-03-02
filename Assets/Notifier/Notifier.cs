using UnityEngine;

namespace Assets.Notifier
{
    /// <summary>
    /// いろいろなNotifiorを定義する場所
    /// プロダクトごとに変えてください
    /// </summary>
    static class Notifier
    {
        /// <summary>
        /// デフォルトの通知
        /// 設定可能
        /// </summary>
        public static INotifier Default = MockNotifier.Default;

        /// <summary>
        /// チャットワーク通知
        /// このまま使わないでください。(部屋など変えてください)
        /// </summary>
        public static readonly INotifier Chatwork;

        private const string SlackApiToken = "xxxx";
        public static readonly INotifier Slack;

        static Notifier() { 


            Chatwork = new ChatworkNotifier("xxxx", "xxxx");


            string channel = "";
            if (UnityEngine.Application.isEditor)
            {
                channel = "error-editor";
            }
            else if (UnityEngine.Application.platform == RuntimePlatform.IPhonePlayer)
            {
                channel = "error-ios";
            }
            else if (UnityEngine.Application.platform == RuntimePlatform.Android)
            {
                channel = "error-android";
            }
            Slack = new SlackNotifier(SlackApiToken,channel,"クライアントエラー通知君");
        }
    }
}
