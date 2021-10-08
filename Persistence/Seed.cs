using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
namespace Persistence
{
     public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any() && !context.Topics.Any())
            {
                string[] roleNames = { "Admin",  "Member" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                         await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com",
                        JoinData = DateTime.UtcNow
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com",
                        JoinData = DateTime.UtcNow
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com",
                        JoinData = DateTime.UtcNow
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");

                    if (user.UserName == "bob")
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    await userManager.AddToRoleAsync(user, "Member");
                }

                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Talks"
                    },
                    new Category
                    {
                        Name = "Beginner"
                    },
                    new Category
                    {
                        Name = "Acoustic"
                    },
                    new Category
                    {
                        Name = "Electric"
                    },
                    new Category
                    {
                        Name = "Bass"
                    }
                    
                };
                
                await context.Categories.AddRangeAsync(categories);

                var topics= new List<Topic>
                {
                    new Topic
                    {
                        Title = "A Modern Method for Guitar Vol 1 - Thread Index",
                        Category = categories[1],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[0],
                        Body = "Okay it's Jan. 1st... 8:15 AM on Jan 1st in Melbourne."+
                               "So let the fun begin. This week, through Jan. 8th, we will be discussing and working on pages 1 through 7 (next week will be pages 8 through 11."+
                               "Here are the links where you can post questions, comments, recordings etc. not specific to this thread:"
                    },
                    new Topic
                    {
                        Title = "Where to begin",
                        Category = categories[1],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[1],
                        Body = "Hey everyone. Im still new to the guitar but I know chords and can listen to songs and try to play along. Learn learning jazz and blues, I’m not sure where to begin. Where’s the best place to begin. Can you recommend books/videos? To get the most out of learning!"
                    },
                    new Topic
                    {
                        Title = "Struggling with a plectrum - necessary?",
                        Category = categories[0],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[2],
                        Body = "Hi - new here. I've come from a classical background, first violin, then classical guitar, but I really love blues and jazz. I've picked up the electric guitar alongside my classical, but I just cannot get the hang of a plectrum. I've been trying to get used to it for over a year, but I always end up using my fingers in frustration after 30 minutes in a practice session."+
                        "Are plectrums strictly necessary? I've heard that some jazz and blues guitarists don't use a plectrum at all - is this true? Browsing through this site, some of the lessons are done purely fingerstyle, but it seems to be a minority."
                    },
                    new Topic
                    {
                        Title = "YOUR DAILY DOSE OF GO PRACTICE?",
                        Category = categories[0],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[2],
                        Body = "I am ready for your comments"
                    },
                    new Topic
                    {
                        Title = "DOES ANYONE WANT ANYTHING TRANSCRIBED?",
                        Category = categories[0],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[2],
                        Body = "Just aiming to improve my ear so hit me up"
                    },
                    new Topic
                    {
                        Title = "DROP CHORDS",
                        Category = categories[2],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[1],
                        Body = "I have just started working on the chord ebook and I’m a bit confused with the drop chords. The definition in the free ebook is that the 3rd highest note is moved to the bass for a drop 3 chord. " + "" +
                               "The chord ebook however doesn’t follow that formula gives R 7 3 5 as the chord structure.what am I missing?"
                    },
                    new Topic
                    {
                        Title = "WHAT SOUND IS HERBIE HANCOCK HANGING ON HERE",
                        Category = categories[2],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[0],
                        Body = "hi all, long time no post from me.i was finding guitar a bit of a drag and my lack of progress was bothering me so been trying to just play along to as much stuff as i can , not worrying too much of what works and why and let me ears guide me a bit more. I think its been worthwhile and ive realised theres a hell of a lot that can be got out of limiting yourself to what you know!"
                    },
                    new Topic
                    {
                        Title = "What's up TDPRI ??",
                        Category = categories[3],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[0],
                        Body = "Just signed up, love the forum. Fullerton native here but posting from my job down the street from the Rickenbacker plant in Santa Ana. Here's my current rig; Squier Bullet-Tele with upgraded neck & pickups(installed & setup by Steve Soest) straight into a new-ish Princeton Reverb. "
                    },
                    new Topic
                    {
                        Title = "Hey kids!",
                        Category = categories[3],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[2],
                        Body = "I earned an one week ban at TGP for having a sense of humor about Echo Park Guitars. Looks like I'll be hanging out here this week while I serve my sentence. Time to look around at more than just the B Bender forum..."
                    },
                    new Topic
                    {
                        Title = "Hello from NJ",
                        Category = categories[3],
                        CreatedAt = DateTime.UtcNow,
                        Creator = users[1],
                        Body = "New login in here. Been playing guitar & bass for many years, started in my teens and now I'm well on the grey side of 50! Mainly I play bass these days and my regular gigs of the past few years have kept me in the bassists role;"
                    }
                };

                await context.Topics.AddRangeAsync(topics);
                await context.SaveChangesAsync();
            }
        }
    }
}