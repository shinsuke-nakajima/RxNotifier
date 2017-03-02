using UniRx;

namespace Assets.Notifier
{
    public interface INotifier
    {
        /// <summary>
        /// 通知を実行します
        /// </summary>
        /// <param name="message"></param>
        IObservable<Unit> Notify(string message);

    }

    public abstract class NotifierBase : INotifier
    {
        /// <summary>
        /// 通知をします
        /// 通知が送られないと判断された場合即座にOnCompleteが発行されます
        /// </summary>
        /// <param name="message">通知メッセージ</param>
        /// <returns></returns>
        public IObservable<Unit> Notify(string message)
        {
            if (!CanNotifior(message)) return Observable.Empty<Unit>();
            message = ProcessMessage(message);
            return CreateNotifyObservable(message);
        }


        /// <summary>
        /// 通知処理を実装します
        /// </summary>
        /// <param name="message">加工後のメッセージ、このメッセージをそのまま通知してください</param>
        /// <returns>通知の完了を示すObservable</returns>
        protected abstract IObservable<Unit> CreateNotifyObservable(string message);

        /// <summary>
        /// このメッセージを実際に通知してよいかを判断します
        /// </summary>
        /// <param name="message">加工前メッセージ</param>
        /// <returns>通知してよい場合はtrue/さもなくばfalse</returns>
        protected virtual bool CanNotifior(string message)
        {
            return true;
        }


        /// <summary>
        /// メッセージを加工します
        /// </summary>
        /// <param name="message">加工前メッセージ</param>
        /// <returns>加工後メッセージ</returns>
        protected virtual string ProcessMessage(string message)
        {
            return message;
        }
    }

    public class MockNotifier : INotifier
    {
        public static readonly MockNotifier Default = new MockNotifier();
        public IObservable<Unit> Notify(string message)
        {
            //nothing to do
            return Observable.Empty<Unit>();
        }

    }
}
