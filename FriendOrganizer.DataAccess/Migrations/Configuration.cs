namespace FriendOrganizer.DataAccess.Migrations
{
    using FriendOrganizer.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDbContext context)
        {
            context.Friends.AddOrUpdate(
                f => f.FirstName,
                new Friend { FirstName = "Сергей", LastName = "Мишин" },
                new Friend { FirstName = "Анджелина", LastName = "Джоли" },
                new Friend { FirstName = "Брэд", LastName = "Питт" },
                new Friend { FirstName = "Жерар", LastName = "Пике" }
            );

            context.ProgrammingLanguages.AddOrUpdate(
                p1 => p1.Name,
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "TypeScript" },
                new ProgrammingLanguage { Name = "Swift" },
                new ProgrammingLanguage { Name = "Java" }
            );

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
                new FriendPhoneNumber
                {
                    Number = "+375 29 5073362",
                    FriendId = context.Friends.First().Id
                });

            context.Meetings.AddOrUpdate(m => m.Title,
                new Meeting
                {
                    Title = "Watching soccer",
                    DateFrom = new DateTime(2018, 5, 26),
                    DateTo = new DateTime(2018, 5, 26),
                    Friends = new List<Friend>
                    {
                        context.Friends.Single(f=>
                        f.FirstName == "Сергей" && f.LastName == "Мишин"),
                        context.Friends.Single(f=>
                        f.FirstName == "Анджелина" && f.LastName == "Джоли")
                    }
                });
        }
    }
}
