using System.Collections;
using System.Collections.Generic;
using Assets.Notifier;
using UnityEngine;
using UniRx;

public static class NotifierTest{

    [RuntimeInitializeOnLoadMethod]
    public static void TestSend()
    {

        Notifier.Chatwork.Notify("通知テスト").Subscribe();
        Notifier.Slack.Notify("通知テスト").Subscribe();
    }
}
