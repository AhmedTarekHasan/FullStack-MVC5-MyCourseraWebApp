namespace MyCourseraWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MyCourseraWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyCourseraWebApp.Models.ApplicationDbContext context)
        {
            SeedSampleData(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private void SeedSampleData(ApplicationDbContext context)
        {
            #region Adding Application User Types
            context.ApplicationUserTypes.AddOrUpdate(t => t.Name,
                new ApplicationUserType { Name = "Manager" },
                new ApplicationUserType { Name = "Student" },
                new ApplicationUserType { Name = "Instructor" }
            );
            #endregion

            var rolesStore = new RoleStore<IdentityRole>(context);
            var rolesManager = new RoleManager<IdentityRole>(rolesStore);

            #region Adding Admin Role
            if (!rolesManager.RoleExists("Admin"))
            {
                rolesManager.Create(new IdentityRole() { Id = "Admin", Name = "Admin" });
            }
            #endregion

            #region Adding User Role
            if (!rolesManager.RoleExists("User"))
            {
                rolesManager.Create(new IdentityRole() { Id = "User", Name = "User" });
            }
            #endregion

            var usersStore = new UserStore<ApplicationUser>(context);
            var usersManager = new UserManager<ApplicationUser>(usersStore);

            #region Adding Super Admin User
            ApplicationUser superAdmin = usersManager.FindByName("superadmin@admin.com");

            if (superAdmin == null)
            {
                superAdmin = new ApplicationUser();
                superAdmin.Email = "superadmin@admin.com";
                superAdmin.UserName = "superadmin@admin.com";
                superAdmin.EmailConfirmed = true;
                superAdmin.FullName = "Super Admin";
                superAdmin.LockoutEnabled = false;
                superAdmin.UserTypeId = 1;
                usersManager.Create(superAdmin, "Xyz78901_");
                superAdmin = usersManager.FindByName("superadmin@admin.com");
            }

            if (!superAdmin.Roles.Any(r => r.RoleId == "Admin"))
            {
                usersManager.AddToRole(superAdmin.Id, "Admin");
            }
            #endregion

            #region Adding Instructor 1 User
            ApplicationUser instructor1 = usersManager.FindByName("instructor1@instructor.com");

            if (instructor1 == null)
            {
                instructor1 = new ApplicationUser();
                instructor1.Email = "instructor1@instructor.com";
                instructor1.UserName = "instructor1@instructor.com";
                instructor1.EmailConfirmed = true;
                instructor1.FullName = "Instructor 1";
                instructor1.LockoutEnabled = false;
                instructor1.UserTypeId = 3;
                usersManager.Create(instructor1, "Xyz78901_");
                instructor1 = usersManager.FindByName("instructor1@instructor.com");
            }

            if (!instructor1.Roles.Any(r => r.RoleId == "User"))
            {
                usersManager.AddToRole(instructor1.Id, "User");
            }
            #endregion

            #region Adding Instructor 2 User
            ApplicationUser instructor2 = usersManager.FindByName("instructor2@instructor.com");

            if (instructor2 == null)
            {
                instructor2 = new ApplicationUser();
                instructor2.Email = "instructor2@instructor.com";
                instructor2.UserName = "instructor2@instructor.com";
                instructor2.EmailConfirmed = true;
                instructor2.FullName = "Instructor 2";
                instructor2.LockoutEnabled = false;
                instructor2.UserTypeId = 3;
                usersManager.Create(instructor2, "Xyz78901_");
                instructor2 = usersManager.FindByName("instructor2@instructor.com");
            }

            if (!instructor2.Roles.Any(r => r.RoleId == "User"))
            {
                usersManager.AddToRole(instructor2.Id, "User");
            }
            #endregion

            #region Adding Instructor 3 User
            ApplicationUser instructor3 = usersManager.FindByName("instructor3@instructor.com");

            if (instructor3 == null)
            {
                instructor3 = new ApplicationUser();
                instructor3.Email = "instructor3@instructor.com";
                instructor3.UserName = "instructor3@instructor.com";
                instructor3.EmailConfirmed = true;
                instructor3.FullName = "Instructor 3";
                instructor3.LockoutEnabled = false;
                instructor3.UserTypeId = 3;
                usersManager.Create(instructor3, "Xyz78901_");
                instructor3 = usersManager.FindByName("instructor3@instructor.com");
            }

            if (!instructor3.Roles.Any(r => r.RoleId == "User"))
            {
                usersManager.AddToRole(instructor3.Id, "User");
            }
            #endregion

            #region Adding Student 1 User
            ApplicationUser student1 = usersManager.FindByName("student1@student.com");

            if (student1 == null)
            {
                student1 = new ApplicationUser();
                student1.Email = "student1@student.com";
                student1.UserName = "student1@student.com";
                student1.EmailConfirmed = true;
                student1.FullName = "Student 1";
                student1.LockoutEnabled = false;
                student1.UserTypeId = 2;
                usersManager.Create(student1, "Xyz78901_");
                student1 = usersManager.FindByName("student1@student.com");
            }

            if (!student1.Roles.Any(r => r.RoleId == "User"))
            {
                usersManager.AddToRole(student1.Id, "User");
            }
            #endregion

            #region Adding Student 2 User
            ApplicationUser student2 = usersManager.FindByName("student2@student.com");

            if (student2 == null)
            {
                student2 = new ApplicationUser();
                student2.Email = "student2@student.com";
                student2.UserName = "student2@student.com";
                student2.EmailConfirmed = true;
                student2.FullName = "Student 2";
                student2.LockoutEnabled = false;
                student2.UserTypeId = 2;
                usersManager.Create(student2, "Xyz78901_");
                student2 = usersManager.FindByName("student2@student.com");
            }

            if (!student2.Roles.Any(r => r.RoleId == "User"))
            {
                usersManager.AddToRole(student2.Id, "User");
            }
            #endregion

            #region Adding Student 3 User
            ApplicationUser student3 = usersManager.FindByName("student3@student.com");

            if (student3 == null)
            {
                student3 = new ApplicationUser();
                student3.Email = "student3@student.com";
                student3.UserName = "student3@student.com";
                student3.EmailConfirmed = true;
                student3.FullName = "Student 3";
                student3.LockoutEnabled = false;
                student3.UserTypeId = 2;
                usersManager.Create(student3, "Xyz78901_");
                student3 = usersManager.FindByName("student3@student.com");
            }

            if (!student3.Roles.Any(r => r.RoleId == "User"))
            {
                usersManager.AddToRole(student3.Id, "User");
            }
            #endregion

            #region Adding Courses
            string longDescription = "This is a course description which is meant to be long enough for testing purposes. That's why what you are reading now is totally useless and is not related by any mean to any actual course. This is a small piece of info I had to share with you as I don't want to upset you. However, if you are still reading up to this point then that means that you have a quiet sense of humor and want to know what I could say more. So, I really don't have any more to say as I am feeling tired right now and above that I am really embarrassed by you still reading these gibberish words coming from deep inside my crazy thoughts. You can offer help by the way, you can send me ideas about what to say to count a huge number of words but for sure with pumping life into it. That;s it, I am tired and can't write any more... I even know that there is a typo (Tha;s) but right now I am lazy enough to even getting back to fix it. Ok, goodbye for now and wish me luck with this.";

            context.Courses.AddOrUpdate(
                c => c.Id,
                new Course("Course 01", longDescription, DateTime.Today.AddDays(-1), DateTime.Today.AddDays(10)),
                new Course("Course 02", longDescription, DateTime.Today.AddDays(2), DateTime.Today.AddDays(10)),
                new Course("Course 03", longDescription, DateTime.Today.AddDays(-10), DateTime.Today.AddDays(-2)),
                new Course("Course 04", longDescription, DateTime.Today.AddDays(0), DateTime.Today.AddDays(5)),
                new Course("Course 05", longDescription, DateTime.Today.AddDays(0), DateTime.Today.AddDays(9)),
                new Course("Course 06", longDescription, DateTime.Today.AddDays(0), DateTime.Today.AddDays(20)),
                new Course("Course 07", longDescription, DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)),
                new Course("Course 08", longDescription, DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)),
                new Course("Course 09", longDescription, DateTime.Today.AddDays(3), DateTime.Today.AddDays(3)),
                new Course("Course 10", longDescription, DateTime.Today.AddDays(4), DateTime.Today.AddDays(9)),
                new Course("Course 11", longDescription, DateTime.Today.AddDays(1), DateTime.Today.AddDays(3)),
                new Course("Course 12", longDescription, DateTime.Today.AddDays(2), DateTime.Today.AddDays(4)),
                new Course("Course 13", longDescription, DateTime.Today.AddDays(-1), DateTime.Today.AddDays(2)),
                new Course("Course 14", longDescription, DateTime.Today.AddDays(-3), DateTime.Today.AddDays(5)),
                new Course("Course 15", longDescription, DateTime.Today.AddDays(-8), DateTime.Today.AddDays(20)),
                new Course("Course 16", longDescription, DateTime.Today.AddDays(4), DateTime.Today.AddDays(6)),
                new Course("Course 17", longDescription, DateTime.Today.AddDays(4), DateTime.Today.AddDays(9)),
                new Course("Course 18", longDescription, DateTime.Today.AddDays(1), DateTime.Today.AddDays(12)),
                new Course("Course 19", longDescription, DateTime.Today.AddDays(2), DateTime.Today.AddDays(3)),
                new Course("Course 20", longDescription, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(4)),
                new Course("Course 21", longDescription, DateTime.Today.AddDays(-9), DateTime.Today.AddDays(-3)),
                new Course("Course 22", longDescription, DateTime.Today.AddDays(1), DateTime.Today.AddDays(11)),
                new Course("Course 23", longDescription, DateTime.Today.AddDays(2), DateTime.Today.AddDays(11)),
                new Course("Course 24", longDescription, DateTime.Today.AddDays(2), DateTime.Today.AddDays(20)),
                new Course("Course 25", longDescription, DateTime.Today.AddDays(0), DateTime.Today.AddDays(2)),
                new Course("Course 26", longDescription, DateTime.Today.AddDays(8), DateTime.Today.AddDays(9)),
                new Course("Course 27", longDescription, DateTime.Today.AddDays(0), DateTime.Today.AddDays(3)),
                new Course("Course 28", longDescription, DateTime.Today.AddDays(0), DateTime.Today.AddDays(5)),
                new Course("Course 29", longDescription, DateTime.Today.AddDays(0), DateTime.Today.AddDays(9)),
                new Course("Course 30", longDescription, DateTime.Today.AddDays(1), DateTime.Today.AddDays(15))
            );
            #endregion

            #region Adding Instructors To Courses
            context.CoursesInstructors.AddOrUpdate(
                ci => new { ci.CourseId, ci.InstructorId },
                new CourseInstructor { CourseId = 1, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 2, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 3, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 4, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 5, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 6, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 7, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 8, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 9, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 10, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 11, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 12, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 13, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 14, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 15, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 16, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 17, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 18, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 19, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 20, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 21, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 22, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 23, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 24, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 25, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 26, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 27, InstructorId = instructor3.Id },
                new CourseInstructor { CourseId = 28, InstructorId = instructor1.Id },
                new CourseInstructor { CourseId = 29, InstructorId = instructor2.Id },
                new CourseInstructor { CourseId = 30, InstructorId = instructor3.Id }
            );
            #endregion

            #region Adding Students To Courses
            context.CoursesAttenders.AddOrUpdate(
                ca => new { ca.CourseId, ca.AttenderId },
                new CourseAttender() { CourseId = 1, AttenderId = student1.Id },
                new CourseAttender() { CourseId = 1, AttenderId = student2.Id },
                new CourseAttender() { CourseId = 1, AttenderId = instructor2.Id },
                new CourseAttender() { CourseId = 1, AttenderId = instructor3.Id },
                new CourseAttender() { CourseId = 2, AttenderId = student1.Id },
                new CourseAttender() { CourseId = 2, AttenderId = student3.Id },
                new CourseAttender() { CourseId = 2, AttenderId = instructor1.Id },
                new CourseAttender() { CourseId = 3, AttenderId = instructor1.Id },
                new CourseAttender() { CourseId = 3, AttenderId = student3.Id },
                new CourseAttender() { CourseId = 4, AttenderId = student2.Id },
                new CourseAttender() { CourseId = 4, AttenderId = instructor3.Id },
                new CourseAttender() { CourseId = 5, AttenderId = student3.Id }
            );
            #endregion

            #region Adding Notification Types
            /*context.NotificationTypes.AddOrUpdate(
                t => t.Name,
                new NotificationType("CourseCreated", "This notification type is related to course creation"),
                new NotificationType("CourseUpdated", "This notification type is related to course update"),
                new NotificationType("CourseCancelled", "This notification type is related to course cancellation")
            );*/
            #endregion

            #region Adding Welcome Notification Message
            context.AdminMessageNotifications.AddOrUpdate(
                n => n.Id,
                new AdminMessageNotification("<span style='font-weight: bold;'>Welcome to MyCoursera</span>", null, superAdmin.Id)
            );

            context.UserNotifications.AddOrUpdate(
                un => un.Id,
                new UserNotification(1, instructor1.Id),
                new UserNotification(1, instructor2.Id),
                new UserNotification(1, instructor3.Id),
                new UserNotification(1, student1.Id),
                new UserNotification(1, student2.Id),
                new UserNotification(1, student3.Id)
            );
            #endregion
        }
    }
}
