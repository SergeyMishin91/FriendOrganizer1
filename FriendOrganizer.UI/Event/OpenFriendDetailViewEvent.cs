using Prism.Events;

namespace FriendOrganizer.UI.Event
{
    // <int> is the specific parameter like friendId
    public class OpenFriendDetailViewEvent:PubSubEvent<int?>
    {
    }
}
