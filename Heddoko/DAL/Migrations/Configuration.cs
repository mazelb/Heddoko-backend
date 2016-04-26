namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL.Models;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<DAL.HDContext>
    {
        public Configuration()
        {
            //TODO Disable it
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            CommandTimeout = 360000;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(DAL.HDContext context)
        {
            if (Config.AllowInitData)
            {
                //TODO add seed
                Tags(context);
                Users(context);
                Groups(context);
                Profiles(context);
                Suits(context);
            }
        }

        private void Users(HDContext context)
        {
            Passphrase pwd = PasswordHasher.Hash("p@ssword");

            context.Users.AddOrUpdate(
                  p => p.Email,
                   new User
                   {
                       Email = "admin@heddoko.co",
                       Username = "heddoko.admin",
                       Status = UserStatusType.Active,
                       Password = pwd.Hash,
                       Salt = pwd.Salt,
                       FirstName = "Admin",
                       LastName = "",
                       Role = UserRoleType.Admin
                   },
                   new User
                   {
                       Email = "awesomeathlete111@heddoko.co",
                       Username = "awesomeathlete111",
                       Status = UserStatusType.Active,
                       Password = pwd.Hash,
                       Salt = pwd.Salt,
                       FirstName = "Awesome",
                       LastName = "Athlete",
                       Role = UserRoleType.User
                   },
                  new User
                  {
                      Email = "awesomecoach111@heddoko.co",
                      Username = "awesomecoach111",
                      Status = UserStatusType.Active,
                      Password = pwd.Hash,
                      Salt = pwd.Salt,
                      FirstName = "Awesome",
                      LastName = "Coach",
                      Role = UserRoleType.Analyst
                  }
              );
            context.SaveChanges();
        }

        private void Groups(HDContext context)
        {

        }

        private void Profiles(HDContext context)
        {

        }

        private void Tags(HDContext context)
        {
            List<string> tags = new List<string>();
            tags.AddRange(new List<string>(){
                "American Football",
                "Cross Fit", "Curling",
                "Diving",
                "Figure Skating", "Football", "Golf",
                "Hockey",
                "Soccer", "Speed Skating", "Swimming", "Synchronized Swimming",
                "Tennis",
                "Weight Lifting",

                // General movements.
                "Back Squat", "Behind the Neck Press", "Behind The Neck Push Jerk",
                "Bench Fly", "Bench Press",
                "Bent Over Row", "Bicep Curl",
                "Box Squat", "Bulgarian Squat",
                "Clean", "Clean Off the Blocks", "Counter Movement Jump",
                "Deadlift", "Decline Bench Press", "Dribble",
                "Front Raise", "Front Squat",
                "Goblet Squat",
                "Half Squat",
                "Hang Clean", "Hang Power Clean", "Hang Power Snatch", "Hang Snatch", "Hip Thrust",
                "Incline Bench Fly", "Incline Bench Press",
                "Jump Squat",
                "Kettlebell Swing",
                "Lateral Raise", "Lunge",
                "Medicine Ball Slam",
                "One Arm Snatch", "Overhead Press", "Overhead Squat",
                "Power Clean", "Power Clean Off the Blocks", "Power Snatch", "Power Snatch Off the Blocks",
                "Push Jerk", "Push Press",
                "Reverse Fly", "Romanian Deadlift",
                "Seated Military Press",
                "Snatch", "Snatch Off the Blocks", "Split Jerk", "Split Squat",
                "Squat Jump", "Standing Military Press", "Step-ups", "Stiff-legged Deadlift", "Sumo Deadlift",
                "Trap Bar Deadlift", "Travelling Lunges",
                "Wide Grip Behind the Neck Push Jerk", "Wide Grip Deadlift",

                // Additional tags.
                "Barbell",
                "Dumbbell",
                "Narrow Grip",
                "Wide Grip"
            });


            context.Tags.AddOrUpdate(
                p => p.Title,
                tags.Select(c => new Tag()
                {
                    Title = c
                }).ToArray()
            );
            context.SaveChanges();
        }

        private void Suits(HDContext context)
        {

        }
    }
}
