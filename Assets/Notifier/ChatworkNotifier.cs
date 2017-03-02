using System.Collections.Generic;
using UniRx;
using UniRx.WebRequest;

namespace Assets.Notifier
{
    public class ChatworkNotifier : NotifierBase
    {

        public string ApiToken { get; private set; }
        public string RoomId { get; private set; }


        public ChatworkNotifier(string apiToken, string roomId)
        {
            ApiToken = apiToken;
            RoomId = roomId;
        }


        protected override IObservable<Unit> CreateNotifyObservable(string message)
        {
            var header = new Dictionary<string,string>()
            {
                { "X-ChatWorkToken",ApiToken}
            };
            var data = new Dictionary<string, string>()
            {
                { "body",message}
            };
            var request = ObservableWebRequest.Post("https://api.chatwork.com/v1/rooms/" + RoomId + "/messages", data, header);

            return request.Do(UnityEngine.Debug.Log).AsUnitObservable();
        }
    }
}
