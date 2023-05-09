using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data
{
    //Random data created with the help of ChatGPT.
    public class Seed
    {
        private readonly ISeedRepo _repo;

        public Seed(ISeedRepo repo)
        {
            _repo = repo;
        }

        public void SeedAll()
        {
            int count = _repo.GetUserCount();

            if (count == 0)
            {
                SeedUsers();
                SeedFollowers();
                SeedPosts();
                SeedFeeds();
                SeedNotifications();
                SeedComments();
            }
        }

        private void SeedUsers()
        {
            CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);

            List<User> users = new List<User>()
            {
                //1. Normal guy
                new User()
                {
                    UserId = 1,
                    Username = "ethanbennett",
                    FirstName = "Ethan",
                    LastName = "Bennett",
                    Email = "ethanbennett@mail.com",
                    Bio = "Just an ordinary guy navigating life's ups and downs with a smile. Coffee lover, adventure seeker, and eternal optimist. Embracing everyday moments and striving for personal growth. Let's connect and share stories along this journey! 🌟☕️ #EverydayAdventures #PositiveVibes",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    IsActive = true,
                    ProfilePhoto = new System.Guid("b5bfd72d-0cc9-46fb-9b74-05ebbd8a0f52")
                },

                //2. Software developer
                new User()
                {
                    UserId = 2,
                    Username = "ameliaross",
                    FirstName = "Amelia",
                    LastName = "Ross",
                    Email = "ameliaross@mail.com",
                    Bio = "Passionate software developer crafting elegant solutions in the digital realm. Embracing the power of code to build innovative applications and solve complex problems. Constantly learning, exploring new technologies, and striving for clean, efficient code. Let's turn ideas into reality! 💻🚀 #CodeWizard #TechEnthusiast",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    IsActive = true,
                    ProfilePhoto = new System.Guid("0943421a-6dd5-489a-ba9a-2dcbb9bbba83")
                },

                //3. Musician
                new User()
                {
                    UserId = 3,
                    Username = "benjaminsullivan",
                    FirstName = "Benjamin",
                    LastName = "Sullivan",
                    Email = "benjaminsullivan@mail.com",
                    PasswordSalt = passwordSalt,
                    Bio = "Melody weaver, spreading musical vibes and capturing hearts through my art. Exploring genres, crafting soulful tunes, and sharing stories that resonate. Let the rhythm guide us on this sonic journey. Join me and let's create harmonious moments together! 🎵✨ #MusicianLife #SoulfulSounds",
                    PasswordHash = passwordHash,
                    IsActive = true,
                    ProfilePhoto = new System.Guid("a79f25e8-0bdb-49c5-a87e-ead3d6e4a13d")
                },

                //4. Cook
                new User()
                {
                    UserId = 4,
                    Username = "oliviafoster",
                    FirstName = "Olivia",
                    LastName = "Foster",
                    Email = "oliviafoster@mail.com",
                    Bio = "Passionate cook exploring the culinary world one recipe at a time. Embracing flavors, experimenting with ingredients, and serving up delicious creations. Food lover, recipe creator, and kitchen adventurer. Join me on this tasty journey! 🍽️🔪 #CookingAdventures #FoodieLife",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    IsActive = true,
                    ProfilePhoto = new System.Guid("e34da253-02f9-4265-b759-12377af0f47b")
                },

                //5. Mathematician
                new User()
                {
                    UserId = 5,
                    Username = "henryanderson",
                    FirstName = "Henry",
                    LastName = "Anderson",
                    Email = "henryanderson@mail.com",
                    Bio = "Mathematics enthusiast exploring the beauty of numbers, patterns, and logic. Equations, proofs, and theorems are my language. Embracing the elegance of mathematical structures and unraveling complex problems. Join me on this journey of discovery and intellectual curiosity! 🔢✍️ #Mathematics #ProblemSolver",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    IsActive = true,
                    ProfilePhoto = new System.Guid("faef1d15-6864-4ecf-baa9-f727d01bb15b")
                }
            };

            _repo.AddUsers(users);
        }

        private void SeedFollowers()
        {
            List<Follower> followers = new List<Follower>()
            {
                //1. Normal guy
                new Follower() { FollowerId = 1, FolloweeId = 2 },
                new Follower() { FollowerId = 1, FolloweeId = 3 },
                new Follower() { FollowerId = 1, FolloweeId = 4 },
                new Follower() { FollowerId = 1, FolloweeId = 5 },

                //2. Software developer
                new Follower() { FollowerId = 2, FolloweeId = 3 },
                new Follower() { FollowerId = 2, FolloweeId = 4 },
                new Follower() { FollowerId = 2, FolloweeId = 5 },

                //3. Musician
                new Follower() { FollowerId = 3, FolloweeId = 5 },

                //4. Cook
                new Follower() { FollowerId = 4, FolloweeId = 1 },
                new Follower() { FollowerId = 4, FolloweeId = 5 },

                //5. Mathematician
                new Follower() { FollowerId = 5, FolloweeId = 2 },
                new Follower() { FollowerId = 5, FolloweeId = 3 }
            };

            _repo.AddFollowers(followers);
        }

        private void SeedPosts()
        {
            Random rnd = new Random();

            List<Post> posts = new List<Post>()
            {
                //1. Normal guy
                new Post() { PostId = 1, UserId = 1, Text = "Just had the most amazing sushi dinner! 🍣 #FoodieLife #SushiLover", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 2, UserId = 1, Text = "Excited for the weekend getaway! Time to relax and recharge. #WeekendVibes", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 3, UserId = 1, Text = "Can't believe it's already May! Time flies. #TimeFlies #HelloMay", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 4, UserId = 1, Text = "Just finished a great workout session. Feeling pumped and energized! 💪 #FitnessMotivation", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 5, UserId = 1, Text = "Movie night with friends tonight. Popcorn and laughter guaranteed! 🍿🎥 #MovieNight", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 6, UserId = 1, Text = "Exploring new hiking trails today. Nature's beauty never fails to amaze me. #NatureLover #HikingAdventures", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 7, UserId = 1, Text = "Finally got my hands on the latest book by my favorite author. Can't wait to dive into the story! 📚 #Bookworm", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 8, UserId = 1, Text = "Sunday brunch with a view. Good food, good company, and a beautiful day. #BrunchTime", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 9, UserId = 1, Text = "Just witnessed a stunning sunset. Nature's artwork is unparalleled. #SunsetMagic #NatureBeauty", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                new Post() { PostId = 10, UserId = 1, Text = "Excited to start a new project at work. Time to challenge myself and learn something new! #CareerGrowth", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) },
                
                //2. Software developer
                new Post() { PostId = 11, UserId = 2, Text = "Just pushed out a new update for our app! Excited for our users to check out the latest features. #SoftwareDevelopment #AppUpdates", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 12, UserId = 2, Text = "Spent the whole day debugging a particularly nasty issue. Finally found the root cause and fixed it. #DebuggingLife #CodeWarrior", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 13, UserId = 2, Text = "Received some great feedback on our latest release. Time to incorporate it into our development process. #FeedbackLoop #ContinuousImprovement", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 14, UserId = 2, Text = "It's always satisfying to see your code run smoothly after hours of hard work. #CodingLife #SoftwareEngineering", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 15, UserId = 2, Text = "Attended an informative conference on new software development technologies. So much to learn and explore! #TechConference #SoftwareInnovation", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 16, UserId = 2, Text = "Just finished a code review for a fellow developer. Always happy to provide constructive feedback and help each other grow. #CodeReviews #Collaboration", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 17, UserId = 2, Text = "Starting a new project from scratch today. Excited to dive in and create something awesome! #NewProject #SoftwareDevelopment", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 18, UserId = 2, Text = "Learned a new coding technique today. Can't wait to experiment and see how it can enhance our current projects. #ContinuousLearning #CodeInnovation", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 19, UserId = 2, Text = "Spent the morning optimizing our database queries. Performance improvements are always worth the effort. #DatabaseOptimization #SoftwarePerformance", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 20, UserId = 2, Text = "Nothing beats the feeling of delivering a project on time and within budget. Hard work pays off! #ProjectManagement #SoftwareDelivery", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,

                //3. Musician
                new Post() { PostId = 21, UserId = 3, Text = "Rehearsing with the band today, getting ready for our upcoming gig. Can't wait to hit the stage! 🎶 #MusicLife #BandPractice", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 22, UserId = 3, Text = "Just finished recording a new track in the studio. Excited to share it with you all soon! 🎵 #StudioLife #NewMusic", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 23, UserId = 3, Text = "Jamming with fellow musicians is always a great way to spark creativity and explore new musical ideas. #MusicalCollaboration #CreativeFlow", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 24, UserId = 3, Text = "Getting lost in the music while practicing. It's such a wonderful feeling when you're in the zone. 🎸🎹 #MusicianLife #FlowState", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 25, UserId = 3, Text = "Attended an inspiring concert last night. Music has the power to move hearts and souls. #LiveMusic #MusicalExperience", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 26, UserId = 3, Text = "Today's songwriting session was full of inspiration. Capturing emotions through melodies and lyrics is pure magic. ✨ #Songwriting #MusicCreation", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 27, UserId = 3, Text = "Just got off stage after an incredible performance. The energy from the crowd was electrifying! 🎤🎶 #LivePerformance #MusicThrills", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 28, UserId = 3, Text = "Reflecting on the power of music to connect people from all walks of life. It truly is a universal language. #MusicUnites #GlobalVibes", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 29, UserId = 3, Text = "Spending the afternoon experimenting with new sounds and instruments. Pushing boundaries and embracing creativity. 🎧 #MusicExperimentation #SoundExploration", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 30, UserId = 3, Text = "Releasing my latest album was a labor of love. Thank you all for the support and love for my music! 🙏🎵 #MusicRelease #Grateful", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                
                //4. Cook
                new Post() { PostId = 31, UserId = 4, Text = "Experimenting with a new recipe today. Can't wait to see how these flavors come together! #CookingAdventures #RecipeTesting", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 32, UserId = 4, Text = "Just perfected a classic dish. Time to sit back, savor the flavors, and enjoy the fruits of my labor. #CookingMastery #DeliciousDelights", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 33, UserId = 4, Text = "Exploring the local farmers market for fresh ingredients. Supporting local producers and getting inspiration for new dishes. #FarmersMarket #FreshProduce", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 34, UserId = 4, Text = "Food is not just about taste; it's an art form. Plating a dish beautifully can elevate the dining experience. #FoodArt #PlatingPerfection", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 35, UserId = 4, Text = "Trying out a new cooking technique today. Always excited to learn and grow in the kitchen. #CulinarySkills #CookingExperiments", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 36, UserId = 4, Text = "Every chef knows the importance of a well-stocked pantry. Stocking up on essentials and specialty ingredients today. #KitchenEssentials #PantryStaples", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 37, UserId = 4, Text = "Hosting a dinner party tonight. Can't wait to share my culinary creations with friends and create lasting memories. #DinnerParty #FoodieGathering", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 38, UserId = 4, Text = "Food brings people together. Nothing beats the joy of seeing smiles on faces as they enjoy a meal I've prepared. #FoodLove #SharedHappiness", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 39, UserId = 4, Text = "Exploring different cuisines and flavors from around the world. The culinary world is so diverse and fascinating! #GlobalFlavors #CulinaryJourney", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 40, UserId = 4, Text = "Baking is therapeutic for the soul. The aroma of freshly baked bread fills the kitchen, creating a cozy and comforting atmosphere. #BakingBliss #KitchenAromas", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
   
                //5. Mathematician
                new Post() { PostId = 41, UserId = 5, Text = "Just solved a challenging proof that had been puzzling me for days. Persistence pays off in the world of mathematics! #Mathematics #Proofs", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 42, UserId = 5, Text = "Numbers have a beauty of their own. Delving into complex equations and uncovering their patterns is a thrilling adventure. #Mathematics #NumberTheory", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 43, UserId = 5, Text = "Attended a fascinating lecture on abstract algebra today. It's incredible how mathematical structures can represent and explain so much. #MathematicsLecture #AbstractAlgebra", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 44, UserId = 5, Text = "Mathematicians see the world through a different lens, where patterns and logic intertwine. It's a captivating journey of discovery. #MathematicsMinds #LogicalThinking", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 45, UserId = 5, Text = "Spent hours exploring the depths of calculus. It's astonishing how it describes and predicts the behavior of so many natural phenomena. #Calculus #MathematicalModeling", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 46, UserId = 5, Text = "Mathematical beauty lies in simplicity. Unraveling complex problems often requires finding the elegant, underlying principles. #MathematicsBeauty #ElegantSolutions", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 47, UserId = 5, Text = "The thrill of finding a counterexample and disproving a conjecture is unmatched. It's all about questioning and seeking the truth. #MathematicalConjectures #Counterexamples", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 48, UserId = 5, Text = "Collaborating with fellow mathematicians is invigorating. Together, we tackle grand challenges and push the boundaries of knowledge. #MathematicalCollaboration #Teamwork", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 49, UserId = 5, Text = "Exploring the depths of geometry today. Its visual nature and intricate theorems never cease to amaze me. #Geometry #MathematicalVisuals", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) } ,
                new Post() { PostId = 50, UserId = 5, Text = "Mathematics is not just numbers; it's a language that unveils the secrets of the universe. I feel fortunate to be part of this world. #MathematicsPassion #UniversalLanguage", LikeCount = rnd.Next(0, 1000), RetweetCount = rnd.Next(0, 100), CommentCount = rnd.Next(0, 1000), CreatedDate = DateTime.Now.AddMinutes(rnd.Next(0, 100)* -1) }
            };

            _repo.AddPosts(posts);
        }

        private void SeedFeeds()
        {
            int counter = 1;
            List<User> users = _repo.GetUsers();
            List<Post> posts = _repo.GetPosts();
            List<Follower> followers = _repo.GetFollowers();

            List<Feed> feeds = new List<Feed>(50);

            foreach (User user in users)
            {
                List<Follower> userFolloweds = followers
                    .Where(x => x.FollowerId == user.UserId)
                    .ToList();

                int[] followedIds = userFolloweds
                    .Select(x => x.FolloweeId)
                    .ToArray()
                    //Send feed to the user too.
                    .Union(new int[] { user.UserId })
                    .ToArray();

                List<Post> userPosts = posts
                    .Where(x => followedIds.Contains(x.UserId))
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                foreach (Post post in userPosts)
                {
                    Feed feed = new Feed()
                    {
                        FeedId = counter++,
                        UserId = user.UserId,
                        PostId = post.PostId,
                        Liked = counter % 2 == 0,
                        Reposted = counter % 2 == 1
                    };
                    feeds.Add(feed);
                }
            }

            _repo.AddFeeds(feeds);
        }

        private void SeedNotifications()
        {
            int counter = 1;

            List<User> users = _repo.GetUsers();

            List<Notification> notifications = new List<Notification>(users.Count * 3);

            foreach (User user in users)
            {
                notifications.Add(new Notification()
                {
                    NotificationId = counter++,
                    UserId = user.UserId,
                    Text = "X liked your post.",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    IsRead = true,
                });

                notifications.Add(new Notification()
                {
                    NotificationId = counter++,
                    UserId = user.UserId,
                    Text = "There was a login to your account from a new device.",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    IsRead = true,
                });

                notifications.Add(new Notification()
                {
                    NotificationId = counter++,
                    UserId = user.UserId,
                    Text = "Z followed you.",
                    CreatedDate = DateTime.Now.AddDays(-3),
                    IsRead = true,
                });
            }

            _repo.AddNotifications(notifications);
        }

        private void SeedComments()
        {
            int commentCounter = 1;
            int commentCounterCounter = 1;

            List<Post> posts = _repo.GetPosts();
            List<User> users = _repo.GetUsers();

            List<Comment> comments = new List<Comment>(posts.Count * 3);
            List<CommentCount> commentCounts = new List<CommentCount>(posts.Count * 3 * 3);

            foreach (Post post in posts)
            {
                int[] userIds = users
                    .Where(x => x.UserId != post.UserId)
                    .Select(x => x.UserId)
                    .ToArray();

                for (int i = 0; i < 3; i++)
                {
                    comments.Add(new Comment()
                    {
                        CommentId = commentCounter++,
                        PostId = post.PostId,
                        UserId = userIds[i],
                        Text = i == 0 ? "🙂" : i == 1 ? "💯" : "👏👏👏",
                        CreatedDate = post.CreatedDate.AddMinutes(i * 5 + 5)
                    });

                    foreach (int j in userIds)
                    {
                        CommentCount newCount = new CommentCount()
                        {
                            CommentCountId = commentCounterCounter++,
                            PostId = post.PostId,
                            CommentId = commentCounter - 1,
                            UserId = j,
                            Liked = j % 2 == 0,
                            Reposted = j % 2 == 1
                        };

                        commentCounts.Add(newCount);
                    }
                }
            };

            _repo.AddComments(comments);
            _repo.AddCommentCounts(commentCounts);
        }

        private void CreatePasswordHash(
        string password,
        out byte[] passwordHash,
        out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}