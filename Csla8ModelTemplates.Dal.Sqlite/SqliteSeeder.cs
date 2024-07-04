using Csla8ModelTemplates.Entities;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Sqlite
{
    /// <summary>
    /// Database seeder.
    /// </summary>
    public static class SqliteSeeder
    {
        private static readonly Random random = new(DateTime.Now.Millisecond);

        /// <summary>
        /// Initializes the database schema and fills it with demo data.
        /// </summary>
        /// <param name="isDevelopment">The database context.</param>
        /// <param name="isDevelopment">Indicates whether the app is running in development mode.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static async Task Run(
            SqliteContext context,
            bool isDevelopment,
            string contentRootPath
        )
        {
            // The SQLite provider for EF6 does not support Code First / Migrations workflow,
            // so you must manually create the database outside of your EF6 workflow.

            //Console.WriteLine("Waiting 10 seconds for the SQLite server to start...");
            //Thread.Sleep(10000);
            Console.WriteLine("Initializing the SQLite database...");
            //await context.Database.MigrateAsync();

            await DeleteAllData(context);
            await ReseedAllTables(context);

            await CreateTeamsPlayers(context);
            await CreateGroupsPersons(context);
            await CreateFolders(context);
        }

        #region Delete all data

        private static async Task DeleteAllData(
            SqliteContext context
            )
        {
            await RemoveFolders(context, null);

            var delGroupPersons = await context.GroupPersons.ToListAsync();
            if (delGroupPersons.Count > 0)
            {
                context.GroupPersons.RemoveRange(delGroupPersons);
                await context.SaveChangesAsync();
            }
            var delPersons = await context.Persons.ToListAsync();
            if (delPersons.Count > 0)
            {
                context.Persons.RemoveRange(delPersons);
                await context.SaveChangesAsync();
            }
            var delGroups = await context.Groups.ToListAsync();
            if (delGroups.Count > 0)
            {
                context.Groups.RemoveRange(delGroups);
                await context.SaveChangesAsync();
            }
            var delPlayers = await context.Players.ToListAsync();
            if (delPlayers.Count > 0)
            {
                context.Players.RemoveRange(delPlayers);
                await context.SaveChangesAsync();
            }
            var delTeams = await context.Teams.ToListAsync();
            if (delTeams.Count > 0)
            {
                context.Teams.RemoveRange(delTeams);
                await context.SaveChangesAsync();
            }
        }

        private static async Task RemoveFolders(
            SqliteContext context,
            long? parentKey
            )
        {
            var delFolders = await context.Folders
                .Where(e => e.ParentKey == parentKey)
                .ToListAsync();

            foreach (var delFolder in delFolders)
                await RemoveFolders(context, delFolder.FolderKey);

            context.Folders.RemoveRange(delFolders);
            await context.SaveChangesAsync();
        }

        #endregion

        #region Reseed all tables

        private static async Task ReseedAllTables(
            SqliteContext context
            )
        {
            await context.Database.ExecuteSqlRawAsync("DELETE FROM SQLITE_SEQUENCE WHERE NAME = 'Teams';");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM SQLITE_SEQUENCE WHERE NAME = 'Players';");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM SQLITE_SEQUENCE WHERE NAME = 'Groups';");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM SQLITE_SEQUENCE WHERE NAME = 'Persons';");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM SQLITE_SEQUENCE WHERE NAME = 'Folders';");
        }

        #endregion

        #region Create teams & players

        private static async Task CreateTeamsPlayers(
            SqliteContext context
            )
        {
            // Create teams.
            for (int i = 0; i < 50; i++)
            {
                int serialNumber = i + 1;
                Team team = new Team
                {
                    TeamGuid = Guid.NewGuid(),
                    TeamCode = $"T-{serialNumber.ToString("0000")}",
                    TeamName = $"Team entry number {serialNumber}",
                };
                await context.Teams.AddAsync(team);
                await context.SaveChangesAsync();

                // Create team's players.
                int count = random.Next(1, 5);
                for (int j = 0; j < count; j++)
                {
                    int index = j + 1;
                    await context.Players.AddAsync(new Player
                    {
                        TeamKey = team.TeamKey,
                        PlayerCode = $"P-{serialNumber.ToString("0000")}-{index}",
                        PlayerName = $"Item entry number {serialNumber}.{index}",
                    });
                }
                await context.SaveChangesAsync();
            }
        }

        #endregion

        #region Create groups and persons

        private static async Task CreateGroupsPersons(
            SqliteContext context
            )
        {
            // Create groups.
            List<long> groupKeys = new List<long>();
            for (int i = 0; i < 20; i++)
            {
                int serialNumber = i + 1;
                Group group = new Group
                {
                    GroupCode = $"G-{serialNumber.ToString("00")}",
                    GroupName = $"Group No. {serialNumber}",
                };
                await context.Groups.AddAsync(group);
                await context.SaveChangesAsync();
                groupKeys.Add(group.GroupKey!.Value);
            }

            // Create persons.
            List<long> personKeys = new List<long>();
            for (int i = 0; i < 20; i++)
            {
                int serialNumber = i + 1;
                Person person = new Person
                {
                    PersonCode = $"XY-{serialNumber.ToString("00")}",
                    PersonName = $"Person #{serialNumber}",
                };
                await context.Persons.AddAsync(person);
                await context.SaveChangesAsync();
                personKeys.Add(person.PersonKey!.Value);
            }

            // Create group-person relations.
            foreach (long groupKey in groupKeys)
            {
                int count = random.Next(1, 5);
                List<long> tempKeys = personKeys.GetRange(0, 20);
                for (int j = 0; j < count; j++)
                {
                    int index = random.Next(1, 20 - j);
                    long personKey = tempKeys[index];
                    await context.GroupPersons.AddAsync(new GroupPerson
                    {
                        GroupKey = groupKey,
                        PersonKey = personKey
                    });
                    tempKeys.Remove(personKey);
                }
            }
            await context.SaveChangesAsync();
        }

        #endregion

        #region Craete folders

        private static async Task CreateFolders(
            SqliteContext context
            )
        {
            await CreateFolderLevel(context, 1, null, null, null);
        }

        private static async Task CreateFolderLevel(
            SqliteContext context,
            int level,
            long? parentKey,
            long? rootKey,
            string? parentPath
            )
        {
            int count = level == 1 ? 3 : random.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int folderOrder = i + 1;
                Folder folder = CreateFolder(
                    parentKey,
                    rootKey,
                    folderOrder,
                    parentPath
                    );
                await context.Folders.AddAsync(folder);
                await context.SaveChangesAsync();

                if (level == 1)
                {
                    folder.RootKey = folder.FolderKey;
                    await context.SaveChangesAsync();
                }

                if (level < 4)
                {
                    string path = parentPath == null
                        ? folderOrder.ToString()
                        : $"{parentPath}.{folderOrder}";
                    await CreateFolderLevel(
                        context,
                        level + 1,                      // level
                        folder.FolderKey,               // parentKey
                        rootKey ?? folder.FolderKey,    // teamKey
                        path                            // parentPath
                        );
                }
            }
        }

        private static Folder CreateFolder(
            long? parentKey,
            long? rootKey,
            int? folderOrder,
            string? parentPath
            )
        {
            return new Folder
            {
                ParentKey = parentKey,
                RootKey = rootKey,
                FolderOrder = folderOrder,
                FolderName = parentPath == null
                    ? $"Folder entry number {folderOrder}"
                    : $"Folder entry number {parentPath}.{folderOrder}"
            };
        }

        #endregion
    }
}
