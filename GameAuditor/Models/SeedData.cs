using GameAuditor.Database;
using Microsoft.EntityFrameworkCore;
/*
namespace GameAuditor.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "Имя поста 1",
                        Content = "Сам пост №1",
                        TagType = "тег 1, тег 2"
                    },
                    new Post
                    {
                        Title = "Имя поста 2",
                        Content = "Сам пост №2",
                        TagType = "тег 3, тег 6"
                    },
                    new Post
                    {
                        Title = "Имя поста 3",
                        Content = "Сам пост №3",
                        TagType = "тег 4, тег 3"
                    },
                    new Post
                    {
                        Title = "Имя поста 4",
                        Content = "Сам пост №4",
                        TagType = "тег 2, тег 5"
                    }
                );
                context.SaveChanges();
            }
            if (!context.Games.Any())
            {
                context.Games.AddRange(
                    new Game
                    {
                        Name = "Имя игры 1",
                        Platforms = {"PC"},
                        Genres = "жанр 2, жанр 4",
                        Description = "описание №1",
                        ReleaseDate = DateTime.Now
                    },
                    new Game
                    {
                        Name = "Имя игры 2",
                        Platforms = "платформа №3, платформа №4",
                        Genres = "жанр 4, жанр 5",
                        Description = "описание №2"
                    },
                    new Game
                    {
                        Name = "Имя игры 3",
                        Platforms = "платформа №1, платформа №4",
                        Genres = "жанр 1, жанр 2",
                        ReleaseDate = DateTime.Now
                    },
                    new Game
                    {
                        Name = "Имя игры 4",
                        Platforms = "платформа №2, платформа №4",
                        Genres = "жанр 2, жанр 4"
                    }
                );;
                context.SaveChanges();
            }
        }
    }
}
*/