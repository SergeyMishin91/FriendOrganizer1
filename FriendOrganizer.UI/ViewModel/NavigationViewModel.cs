﻿using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupDataService;
        private IMeetingLookupDataService _meetingLookupDataService;
        private IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public ObservableCollection<NavigationItemViewModel> Meetings { get; }

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService,
            IMeetingLookupDataService meetingLookupDataService,
            IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            Meetings = new ObservableCollection<NavigationItemViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().
                Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailDeleted(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailDeleted(Meetings, args);
                    break;
            }
        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items, 
            AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailSaved(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(Meetings, args);
                    break;
            }


        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items, 
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(args.Id,
                    args.DisplayMember,
                    args.ViewModelName,
                    _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
            
        }

        public async Task LoadAsync()
        {
            //lookup - contained friends list
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
                Friends.Add(new NavigationItemViewModel(item.Id, 
                    item.DisplayMember,
                    nameof(FriendDetailViewModel),
                    _eventAggregator));

            //lookup - contained friends list
            lookup = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meetings.Clear();
            foreach (var item in lookup)
                Meetings.Add(new NavigationItemViewModel(item.Id,
                    item.DisplayMember,
                    nameof(MeetingDetailViewModel),
                    _eventAggregator));
        }



    }
}
