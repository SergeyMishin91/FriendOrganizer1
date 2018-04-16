using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, FriendOrganizerDbContext>, 
        IMeetingRepository
    {
        public MeetingRepository(FriendOrganizerDbContext context) : base(context)
        {
        }

        public override Task<Meeting> GetByIdAsync(int id)
        {
            return Context.Meetings
                .Include(m => m.Friends)
                .SingleAsync(m => m.Id == id);
        }
    }
}
