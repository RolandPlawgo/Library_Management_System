using LibraryManagementSystem.Authentication;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BookModel> Books { get; set; } = null!;
        public DbSet<UserAccountModel> UserAccounts { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BookModel>().HasData(
                new BookModel()
                {
                    Id = 1,
                    Title = "Psychology of Money",
                    Author = "Morgan Housel",
                    Description = "Doing well with money isn’t necessarily about what you know. It’s about how you behave. And behavior is hard to teach, even to really smart people.\r\nMoney—investing, personal finance, and business decisions—is typically taught as a math-based field, where data and formulas tell us exactly what to do. But in the real world people don’t make financial decisions on a spreadsheet. They make them at the dinner table, or in a meeting room, where personal history, your own unique view of the world, ego, pride, marketing, and odd incentives are scrambled together.\r\nIn The Psychology of Money, award-winning author Morgan Housel shares 19 short stories exploring the strange ways people think about money and teaches you how to make better sense of one of life’s most important topics.",
                    Publisher = "Harriman House",
                    Availability = true
                },
                new BookModel()
                {
                    Id = 2,
                    Title = "Thinking Fast and Slow",
                    Author = "Daniel Kahneman",
                    Description = "The New York Times Bestseller, acclaimed by author such as Freakonomics co- author Steven D. Levitt, Black Swan author Nassim Nicholas Taleb and Nudge co- author Richard Thaler, Thinking Fast and Slow offers a whole new look at the way our minds work, and how we make decisions.  Why is there more chance we'll believe something if it's in a bold type face? Why are judges more likely to deny parole before lunch? Why do we assume a good-looking person will be more competent?  The answer lies in the two ways we make choices: fast, intuitive thinking, and slow, rational thinking. This book reveals how our minds are tripped up by error and prejudice (even when we think we are being logical), and gives you practical techniques for slower, smarter thinking. It will enable to you make better decisions at work, at home, and in everything you do.",
                    Publisher = "Penguin Books 2012",
                    Availability = true
                },
                new BookModel()
                {
                    Id = 3,
                    Title = "Noise: A Flaw in Human Judgement",
                    Author = "Daniel Kahneman, Oliver Sibony, Cass R. Sunstein",
                    Description = "THE INTERNATIONAL BESTSELLER. A monumental, gripping book ... Outstanding SUNDAY TIMES",
                    Publisher = "Harpercollins Publishers",
                    Availability = true
                });
        }
    }
}
