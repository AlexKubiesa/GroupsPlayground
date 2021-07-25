using System;
using GroupsPlayground.Persistence.Common;
using GroupsPlayground.Persistence.Framework;

namespace GroupsPlayground.Persistence.Groups
{
    public class GroupsSession : AppSession
    {
        public GroupsSession()
        {
            AddRepository(GroupRepository = new GroupRepository(Context));
        }

        public GroupRepository GroupRepository { get; }
    }
}