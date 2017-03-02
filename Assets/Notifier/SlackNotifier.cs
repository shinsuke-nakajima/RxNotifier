using System;
using System.Collections.Generic;
using UniRx;
using UniRx.WebRequest;

namespace Assets.Notifier
{
    class SlackNotifier : NotifierBase
    {
        public readonly string ApiToken;
        public readonly string Channel;
        public readonly string UserName;


        public SlackNotifier(string apiToken,string channel, string userName = "クライアントbot")
        {
            if (string.IsNullOrEmpty(apiToken) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(channel))
            {
                throw new NullReferenceException("apiToken or userName is null or empty");
            }

            ApiToken = apiToken;
            Channel = channel;
            UserName = userName;
        }

        protected IObservable<string> GetRequest(string message)
        {
            var data = new Dictionary<string,string>();

            data["text"] = message;
            data["channel"] = Channel;
            data["username"] = UserName;

            return ObservableWebRequest.Post(string.Format("https://slack.com/api/chat.postMessage?token={0}", ApiToken), data);
        }

        protected override IObservable<Unit> CreateNotifyObservable(string message)
        {
            return GetRequest(message).Do(UnityEngine.Debug.Log).AsUnitObservable();
        }

        /// <summary>
        /// メッセージを表示用に加工します
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override string ProcessMessage(string message)
        {
            
            return Guid.NewGuid() + base.ProcessMessage(message);
        }
    }
}
