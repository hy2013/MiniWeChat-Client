﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using protocol;

namespace MiniWeChat
{
    public class GroupChatPanel : ChatPanel
    {
        private GroupItem _groupItem;

        public override void OnEnter(object param = null)
        {
            _chatLog = param as ChatLog;

            Init(param);

            _groupItem = GlobalGroup.GetInstance().GetGroup(_chatLog.chatID);
            if (_groupItem != null)
            {
                if (_labelGuestUserName)
                {
                    _labelGuestUserName.text = _groupItem.groupName;
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            UIManager.GetInstance().DestroySingleUI(EUIType.GroupChatPanel);
        }

        public override void OnClickFriendDetailButton()
        {
            StateManager.GetInstance().PushState<GroupDetailPanel>(EUIType.GroupDetailPanel, _groupItem);
        }

        public override void OnClickSendEmotionButton(int index)
        {
            ChatDataItem chatDataItem = new ChatDataItem
            {
                sendUserId = GlobalUser.GetInstance().UserId,
                receiveUserId = _groupItem.groupId,
                date = System.DateTime.Now.Ticks,
                chatType = ChatDataItem.ChatType.IMAGE,
                chatBody = index.ToString(),
                targetType = ChatDataItem.TargetType.GROUP,
            };
            GlobalChat.GetInstance().SendChatReq(chatDataItem);

            AddBubbleFrame(chatDataItem);

            UpdateChatBubbleGrid();
        }
    }
}

