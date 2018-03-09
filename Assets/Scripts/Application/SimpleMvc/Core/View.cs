using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleMvc
{
    public class View : MonoBehaviour, IView
    {
        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="view"></param>
        /// <param name="messages"></param>
        protected void RegisterMessage(IView view, List<string> messages)
        {
            if (messages == null || messages.Count == 0) return;
            Controller.Instance.Register(view, messages.ToArray());
        }

        /// <summary>
        /// 移除消息
        /// </summary>
        /// <param name="view"></param>
        /// <param name="messages"></param>
        protected void RemoveMessage(IView view, List<string> messages)
        {
            if (messages == null || messages.Count == 0) return;
            Controller.Instance.Remove(view, messages.ToArray());
        }

        public virtual void OnMessage(IMessage message)
        {
        }
    }
}
