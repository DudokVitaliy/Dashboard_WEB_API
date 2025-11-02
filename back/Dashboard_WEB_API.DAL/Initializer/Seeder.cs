using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.DAL.Initializer
{
    public static class Seeder
    {
        public static async void Seed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await context.Database.MigrateAsync();

            if (!await roleManager.Roles.AnyAsync())
            {
                var roleAdmin = new ApplicationRole { Name = "admin" };
                var roleUser = new ApplicationRole { Name = "user" };

                await roleManager.CreateAsync(roleAdmin);
                await roleManager.CreateAsync(roleUser);

                var admin = new ApplicationUser
                {
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                    UserName = "admin"
                };

                var user = new ApplicationUser
                {
                    Email = "user@mail.com",
                    EmailConfirmed = true,
                    UserName = "user"
                };

                await userManager.CreateAsync(admin, "qwerty");
                await userManager.CreateAsync(user, "qwerty");

                await userManager.AddToRoleAsync(admin, "admin");
                await userManager.AddToRoleAsync(user, "user");
            }

            if (!context.Genres.Any())
            {
                var genres = new GenreEntity[]
                {
                    new GenreEntity { Name = "Аркадні", NormalizedName = "АРКАДНІ" },
                    new GenreEntity { Name = "Спортивні", NormalizedName = "СПОРТИВНІ" },
                    new GenreEntity { Name = "Симулятори", NormalizedName = "СИМУЛЯТОРИ" },
                    new GenreEntity { Name = "Стратегії", NormalizedName = "СТРАТЕГІЇ" },
                    new GenreEntity { Name = "Пригодницькі", NormalizedName = "ПРИГОДНИЦЬКІ" }
                };

                await context.Genres.AddRangeAsync(genres);
                await context.SaveChangesAsync();
                await context.SaveChangesAsync();

                var games = new GameEntity[]
               {
                    // --- Аркадні ---
                    new GameEntity
                    {
                        Name = "Pixel Runner",
                        Description = "Динамічна аркада з ретро-графікою, де ви керуєте бігуном, уникаючи перешкод.",
                        Publisher = "IndieVision",
                        Developer = "RetroBits Studio",
                        Price = 199,
                        ReleaseDate = new DateTime(2022, 5, 14).ToUniversalTime(),
                        Genres = [genres[0]]
                    },
                    new GameEntity
                    {
                        Name = "Sky Blaster",
                        Description = "Аркадна гра зі стріляниною у небі та нескінченними хвилями ворогів.",
                        Publisher = "SkyGames",
                        Developer = "BluePixel",
                        Price = 249,
                        ReleaseDate = new DateTime(2021, 11, 2).ToUniversalTime(),
                        Genres = [genres[0]]
                    },
                    new GameEntity
                    {
                        Name = "Fruit Smash",
                        Description = "Весела гра, де потрібно розбивати фрукти на час.",
                        Publisher = "CasualFun",
                        Developer = "SmileSoft",
                        Price = 99,
                        ReleaseDate = new DateTime(2023, 3, 7).ToUniversalTime(),
                        Genres = [genres[0]]
                    },
                    new GameEntity
                    {
                        Name = "Neon Dash",
                        Description = "Неонова аркада з музикою, де ви керуєте кубом у світі перешкод.",
                        Publisher = "FuturePlay",
                        Developer = "SynthWave Devs",
                        Price = 179,
                        ReleaseDate = new DateTime(2020, 9, 21).ToUniversalTime(),
                        Genres = [genres[0]]
                    },
                    new GameEntity
                    {
                        Name = "Alien Invaders",
                        Description = "Класична аркада з битвою проти хвиль прибульців.",
                        Publisher = "ArcadeMasters",
                        Developer = "RetroPlanet",
                        Price = 129,
                        ReleaseDate = new DateTime(2019, 6, 12).ToUniversalTime(),
                        Genres = [genres[0]]
                    },
                
                    // --- Спортивні ---
                    new GameEntity
                    {
                        Name = "Street Football 2024",
                        Description = "Аркадний футбол на вулицях великих міст.",
                        Publisher = "UrbanGames",
                        Developer = "KickSoft",
                        Price = 899,
                        ReleaseDate = new DateTime(2024, 2, 5).ToUniversalTime(),
                        Genres = [genres[1]]
                    },
                    new GameEntity
                    {
                        Name = "Pro Tennis League",
                        Description = "Реалістичний симулятор тенісних турнірів.",
                        Publisher = "SportsVision",
                        Developer = "BallLogic",
                        Price = 749,
                        ReleaseDate = new DateTime(2022, 7, 18).ToUniversalTime(),
                        Genres = [genres[1]]
                    },
                    new GameEntity
                    {
                        Name = "Basketball Stars",
                        Description = "Баскетбольна аркада з мультиплеєром.",
                        Publisher = "GameWorld",
                        Developer = "HoopStudio",
                        Price = 699,
                        ReleaseDate = new DateTime(2021, 4, 9).ToUniversalTime(),
                        Genres = [genres[1]]
                    },
                    new GameEntity
                    {
                        Name = "Winter Extreme",
                        Description = "Спортивна гра з лижами, сноубордом і зимовими змаганнями.",
                        Publisher = "FrostyPlay",
                        Developer = "SnowSoft",
                        Price = 599,
                        ReleaseDate = new DateTime(2020, 12, 1).ToUniversalTime(),
                        Genres = [genres[1]]
                    },
                    new GameEntity
                    {
                        Name = "Boxing Champion",
                        Description = "Бокс із кар'єрою від новачка до чемпіона світу.",
                        Publisher = "RingMasters",
                        Developer = "PowerGlove",
                        Price = 799,
                        ReleaseDate = new DateTime(2023, 9, 22).ToUniversalTime(),
                        Genres = [genres[1]]
                    },
                
                    // --- Симулятори ---
                    new GameEntity
                    {
                        Name = "City Builder Pro",
                        Description = "Симулятор будівництва сучасного міста.",
                        Publisher = "UrbanVision",
                        Developer = "MegaSim Studio",
                        Price = 1199,
                        ReleaseDate = new DateTime(2021, 5, 30).ToUniversalTime(),
                        Genres = [genres[2]]
                    },
                    new GameEntity
                    {
                        Name = "Flight Captain",
                        Description = "Авіаційний симулятор з реалістичною фізикою польотів.",
                        Publisher = "AeroPlay",
                        Developer = "SkyDream",
                        Price = 1399,
                        ReleaseDate = new DateTime(2020, 10, 15).ToUniversalTime(),
                        Genres = [genres[2]]
                    },
                    new GameEntity
                    {
                        Name = "Farm Life 2022",
                        Description = "Симулятор ферми з доглядом за тваринами та врожаєм.",
                        Publisher = "GreenGames",
                        Developer = "VillageSoft",
                        Price = 899,
                        ReleaseDate = new DateTime(2022, 8, 2).ToUniversalTime(),
                        Genres = [genres[2]]
                    },
                    new GameEntity
                    {
                        Name = "Train Control",
                        Description = "Симулятор керування поїздами.",
                        Publisher = "RailWorld",
                        Developer = "SteelTrack Studio",
                        Price = 1099,
                        ReleaseDate = new DateTime(2019, 3, 10).ToUniversalTime(),
                        Genres = [genres[2]]
                    },
                    new GameEntity
                    {
                        Name = "Cooking Master",
                        Description = "Симулятор кухаря у ресторані зі складними рецептами.",
                        Publisher = "FoodPlay",
                        Developer = "TastyGames",
                        Price = 499,
                        ReleaseDate = new DateTime(2023, 11, 4).ToUniversalTime(),
                        Genres = [genres[2]]
                    },
                
                    // --- Стратегії ---
                    new GameEntity
                    {
                        Name = "Empire Rise",
                        Description = "Глобальна стратегія з побудовою імперії.",
                        Publisher = "WorldForge",
                        Developer = "IronLogic",
                        Price = 1299,
                        ReleaseDate = new DateTime(2021, 6, 19).ToUniversalTime(),
                        Genres = [genres[3]]
                    },
                    new GameEntity
                    {
                        Name = "Battle Tactics",
                        Description = "Тактична стратегія з битвами у реальному часі.",
                        Publisher = "WarVision",
                        Developer = "TacticSoft",
                        Price = 999,
                        ReleaseDate = new DateTime(2020, 2, 7).ToUniversalTime(),
                        Genres = [genres[3]]
                    },
                    new GameEntity
                    {
                        Name = "Galactic Conquest",
                        Description = "Космічна стратегія з колонізацією планет.",
                        Publisher = "StarPlay",
                        Developer = "Nebula Devs",
                        Price = 1399,
                        ReleaseDate = new DateTime(2022, 9, 27).ToUniversalTime(),
                        Genres = [genres[3]]
                    },
                    new GameEntity
                    {
                        Name = "Castle Defense",
                        Description = "Стратегія оборони замку з ордами ворогів.",
                        Publisher = "MedievalGames",
                        Developer = "KnightSoft",
                        Price = 499,
                        ReleaseDate = new DateTime(2019, 12, 3).ToUniversalTime(),
                        Genres = [genres[3]]
                    },
                    new GameEntity
                    {
                        Name = "Civilization Builder",
                        Description = "Покрокова стратегія розвитку цивілізації.",
                        Publisher = "GlobalSoft",
                        Developer = "VisionForge",
                        Price = 1499,
                        ReleaseDate = new DateTime(2023, 5, 20).ToUniversalTime(),
                        Genres = [genres[3]]
                    },
                
                    // --- Пригодницькі ---
                    new GameEntity
                    {
                        Name = "Lost Island",
                        Description = "Пригодницька гра про виживання на безлюдному острові.",
                        Publisher = "AdventurePlay",
                        Developer = "Survivor Studio",
                        Price = 799,
                        ReleaseDate = new DateTime(2020, 8, 11).ToUniversalTime(),
                        Genres = [genres[4]]
                    },
                    new GameEntity
                    {
                        Name = "Mystic Forest",
                        Description = "Подорож через магічний ліс з головоломками.",
                        Publisher = "EnchantedGames",
                        Developer = "GreenLeaf",
                        Price = 649,
                        ReleaseDate = new DateTime(2021, 10, 29).ToUniversalTime(),
                        Genres = [genres[4]]
                    },
                    new GameEntity
                    {
                        Name = "Pirate’s Journey",
                        Description = "Пригоди капітана-пірата у пошуках скарбів.",
                        Publisher = "SeaGames",
                        Developer = "BlackFlag Studio",
                        Price = 999,
                        ReleaseDate = new DateTime(2019, 7, 23).ToUniversalTime(),
                        Genres = [genres[4]]
                    },
                    new GameEntity
                    {
                        Name = "Ancient Tombs",
                        Description = "Пригодницька гра з дослідженням стародавніх руїн.",
                        Publisher = "HistoryPlay",
                        Developer = "LostKey Devs",
                        Price = 899,
                        ReleaseDate = new DateTime(2022, 4, 6).ToUniversalTime(),
                        Genres = [genres[4]]
                    },
                    new GameEntity
                    {
                        Name = "Time Traveler",
                        Description = "Подорож крізь епохи з небезпечними пригодами.",
                        Publisher = "ChronoVision",
                        Developer = "InfinitySoft",
                        Price = 1199,
                        ReleaseDate = new DateTime(2023, 1, 16).ToUniversalTime(),
                        Genres = [genres[4]]
                    }
               };

                await context.Games.AddRangeAsync(games);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
