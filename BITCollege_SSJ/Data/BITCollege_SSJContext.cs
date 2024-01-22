using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BITCollege_SSJ.Data
{
    public class BITCollege_SSJContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BITCollege_SSJContext() : base("name=BITCollege_SSJContext")
        {
        }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.AcademicProgram> AcademicPrograms { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.GradePointState> GradePointStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.SuspendedState> SuspendedStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.ProbationState> ProbationStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.RegularState> RegularStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.HonoursState> HonoursStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.GradedCourse> GradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.AuditCourse> AuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_SSJ.Models.MasteryCourse> MasteryCourses { get; set; }
    }
}
