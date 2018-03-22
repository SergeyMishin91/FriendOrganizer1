using FriendOrganizer.Model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            yield return new Friend { FirstName = "Сергей", LastName = "Мишин" };
            yield return new Friend { FirstName = "Анджелина", LastName = "Джоли" };
            yield return new Friend { FirstName = "Брэд", LastName = "Питт" };
            yield return new Friend { FirstName = "Жерар", LastName = "Пике" };
        }
    }
}
