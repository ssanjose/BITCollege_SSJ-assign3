using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices;
using Utility;
using BITCollege_SSJ.Data;

/*
 *  Student Name: Symon Kurt San Jose
 *  Program: Business Information Technology
 *  Course: ADEV-3008 (204640) Programming 3
 *  Student ID: 0344572
 *  Date Created: 9/8/2020
 *  Date Updated: 9/20/2020
 */

namespace BITCollege_SSJ.Models
{
    /// <summary>
    /// Student Model: Model to represent a student.
    /// </summary>
    public class Student
    {
        //--------------------------------------[ Attributes ]--------------------------------------
        private BITCollege_SSJContext dataContext = new BITCollege_SSJContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("GradePointState")]
        [Display(Name ="Grade Point\nState")]
        public int GradePointStateId { get; set; }

        [ForeignKey("AcademicProgram")]
        [Display(Name = "Academic\nProgram")]
        public int? AcademicProgramId { get; set; }

        [Required]
        [Range(10000000, 99999999)]
        [Display(Name = "Student\nNumber")]
        public long StudentNumber { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "First\nName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "Last\nName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string Address { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string City { get; set; }

        [Required]
        [RegularExpression("(AB|BC|MB|NB|NL|NS|NT|NU|ON|PE|QC|SK|YT)", 
                            ErrorMessage = "Please enter a valid Canadian province code.")]
        public string Province { get; set; }

        [Required]
        [RegularExpression("[^D,F,I,O,Q,U,W,Z,0-9][0-9][^D,F,I,O,Q,U,0-9] [0-9][^D,F,I,O,Q,U,0-9][0-9]",
                            ErrorMessage ="Please enter a valid postal code.")]
        [Display(Name ="Postal\nCode")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name ="Date\nCreated")]
        [DisplayFormat(DataFormatString ="{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Display(Name ="Grade Point\nAverage")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Range(0.0, 4.5)]
        public double? GradePointAverage { get; set; }

        [Required]
        [Display(Name ="Oustanding\nFees")]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = true)]
        public double OutstandingFees { get; set; }

        public string Notes { get; set; }

        [Display(Name ="Name")]
        public string FullName 
        {
            get
            {
                return String.Format($"{this.FirstName} {this.LastName}");
            }
        }

        [Display(Name ="Address")]
        public string FullAddress
        {
            get
            {
                return String.Format($"{this.Address} {this.City} {this.Province}, {this.PostalCode}");
            }
        }

        //--------------------------------------[ Methods ]--------------------------------------
        /// <summary>
        /// Method to change the GradePointStates of the student.
        /// </summary>
        public void ChangeState()
        {
            int originalGradePointStateId;
            do
            {
                originalGradePointStateId = this.GradePointStateId;
                dataContext.GradePointStates.Find(originalGradePointStateId).StateChangeCheck(this);
            } while (this.GradePointStateId != originalGradePointStateId);
        }

        //--------------------------------------[ Navigational Properties ]--------------------------------------
        /// <summary>
        /// Navigation property for the GradePointState model.
        /// </summary>
        public virtual GradePointState GradePointState { get; set; }

        /// <summary>
        /// Navigation property for the AcademicProgram model.
        /// </summary>
        public virtual AcademicProgram AcademicProgram { get; set; }

        /// <summary>
        /// Navigation property for the Registration model.
        /// </summary>
        public virtual ICollection<Registration> Registration { get; set; }
    }

    /// <summary>
    /// AcademicProgram Model: Model to represent an academic program.
    /// </summary>
    public class AcademicProgram
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AcademicProgramId { get; set; }

        [Required]
        [Display(Name ="Program")]
        public string ProgramAcronym { get; set; }

        [Required]
        [Display(Name ="Program\nName")]
        public string Description { get; set; }

        //  Navigational Properties
        /// <summary>
        /// Navigation property for the Student model.
        /// </summary>
        public virtual ICollection<Student> Student { get; set; }

        /// <summary>
        /// Navigation property for the Course model.
        /// </summary>
        public virtual ICollection<Course> Course { get; set; }
    }

    /// <summary>
    /// GradePointState Model: Model to represent the grade point state.
    /// </summary>
    public abstract class GradePointState
    {
        //--------------------------------------[ Attributes ]--------------------------------------
        protected static BITCollege_SSJContext dataContext = new BITCollege_SSJContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int GradePointStateId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name ="Lower\nLimit")]
        public double LowerLimit { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Upper\nLimit")]
        public double UpperLimit { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tuition\nRate\nFactor")]
        public double TuitionRateFactor { get; set; }

        [Display(Name ="Grade\nPoint\nState")]
        public string Description
        {
            get
            {
                return StringFormatter.Remove(GetType().Name, "State\\w*");
            }
        }

        //--------------------------------------[ Methods ]--------------------------------------
        /// <summary>
        /// Method to be overwritten to adjust the tuition rate of the student.
        /// </summary>
        /// <param name="student">The student whose tuition rate to be adjusted.</param>
        /// <returns>The tuition rate of the student.</returns>
        public virtual double TuitionRateAdjustment(Student student)
        {
            return 0;
        }

        /// <summary>
        /// Method to be overwritten to check and change the state.
        /// </summary>
        /// <param name="student"></param>
        public virtual void StateChangeCheck(Student student) { }

        //--------------------------------------[ Navigational Properties ]--------------------------------------
        /// <summary>
        /// Navigation property for the Student model.
        /// </summary>
        public virtual ICollection<Student> Student { get; set; }
    }

    /// <summary>
    /// SuspendedState Model: Model to represent the suspended state in the database.
    /// </summary>
    public class SuspendedState : GradePointState
    {
        //--------------------------------------[ Attributes ]--------------------------------------
        private static SuspendedState suspendedState;
        private const double LOWER_LIMIT = 0.00,
                             UPPER_LIMIT = 1.00,
                             TUITION_RATE_FACTOR = 1.10;

        //--------------------------------------[ Constructor ]--------------------------------------
        /// <summary>
        /// Private constructor for SuspendedState.
        /// </summary>
        private SuspendedState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.TuitionRateFactor = TUITION_RATE_FACTOR;
        }

        //--------------------------------------[ Methods ]--------------------------------------
        /// <summary>
        /// Method to get the instance of SuspendedState to use.
        /// </summary>
        /// <returns>Returns the instance of SuspendedState.</returns>
        public static SuspendedState GetInstance()
        {
            if(suspendedState == null)
            {
                suspendedState = dataContext.SuspendedStates.SingleOrDefault();
                if(suspendedState == null)
                {
                    suspendedState = new SuspendedState();
                    dataContext.SuspendedStates.Add(suspendedState);
                    dataContext.SaveChanges();
                }
            }
            return suspendedState;
        }

        /// <summary>
        /// Method to adjust the tuition rate of the student.
        /// </summary>
        /// <param name="student">The student whose tuition rate to be adjusted.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double addedTuitionRateFactor = 0.00;
            if (student.GradePointAverage < 0.50)
                addedTuitionRateFactor = 0.05;
            else if (student.GradePointAverage < 0.75)
                addedTuitionRateFactor = 0.02;

            return (this.TuitionRateFactor + addedTuitionRateFactor);
        }

        /// <summary>
        /// Method to check and change the state of the student.
        /// </summary>
        /// <param name="student">The student whose state to be changed.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < this.LowerLimit)
                student.GradePointAverage = this.LowerLimit;
            if (student.GradePointAverage > this.UpperLimit)
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
            dataContext.SaveChanges();
        }

    }

    /// <summary>
    /// ProbationState Model: Model to represent the probation state in the database.
    /// </summary>
    public class ProbationState : GradePointState
    {
        //--------------------------------------[ Attributes ]--------------------------------------
        private static ProbationState probationState;
        private const double LOWER_LIMIT = 1.00,
                             UPPER_LIMIT = 2.00,
                             TUITION_RATE_FACTOR = 1.075;

        //--------------------------------------[ Constructor ]--------------------------------------
        /// <summary>
        /// Private constructor for ProbationState.
        /// </summary>
        private ProbationState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.TuitionRateFactor = TUITION_RATE_FACTOR;
        }

        //--------------------------------------[ Methods ]--------------------------------------
        /// <summary>
        /// Method to get the instance of ProbationState to be used.
        /// </summary>
        /// <returns>The instance of ProbationState.</returns>
        public static ProbationState GetInstance()
        {
            if(probationState == null)
            {
                probationState = dataContext.ProbationStates.SingleOrDefault();
                if(probationState == null)
                {
                    probationState = new ProbationState();
                    dataContext.ProbationStates.Add(probationState);
                    dataContext.SaveChanges();
                }
            }
            return probationState;
        }

        /// <summary>
        /// Method to adjust the tuition rate of the student.
        /// </summary>
        /// <param name="student">The student whose tuition rate to be adjusted.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double newTuitionRateFactor = 1.035;
            if (dataContext.Registrations.Where(x => x.StudentId == student.StudentId && x.Grade != null).Count() < 5)
                newTuitionRateFactor = this.TuitionRateFactor;

            return newTuitionRateFactor;
        }

        /// <summary>
        /// Method to check and change the state of the student.
        /// </summary>
        /// <param name="student">The student whose state to be changed.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < this.LowerLimit)
                student.GradePointStateId = SuspendedState.GetInstance().GradePointStateId;
            if (student.GradePointAverage > this.UpperLimit)
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            dataContext.SaveChanges();
        }
    }

    /// <summary>
    /// RegularState Model: Model to represent the regular state in the database.
    /// </summary>
    public class RegularState : GradePointState
    {
        //--------------------------------------[ Attributes ]--------------------------------------
        private static RegularState regularState;
        private const double LOWER_LIMIT = 2.00,
                             UPPER_LIMIT = 3.70,
                             TUITION_RATE_FACTOR = 1.00;

        //--------------------------------------[ Contructor ]--------------------------------------
        /// <summary>
        /// The constructor for RegularState.
        /// </summary>
        private RegularState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.TuitionRateFactor = TUITION_RATE_FACTOR;
        }

        //--------------------------------------[ Methods ]--------------------------------------
        /// <summary>
        /// Method to get the instance of RegularState to be used.
        /// </summary>
        /// <returns>The instance of RegularState.</returns>
        public static RegularState GetInstance()
        {
            if(regularState == null)
            {
                regularState = dataContext.RegularStates.SingleOrDefault();
                if(regularState == null)
                {
                    regularState = new RegularState();
                    dataContext.RegularStates.Add(regularState);
                    dataContext.SaveChanges();
                }
            }
            return regularState;
        }

        /// <summary>
        /// Method to adjust the tuition rate of the student.
        /// </summary>
        /// <param name="student">The student whose tuition rate to be adjusted.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            return this.TuitionRateFactor;
        }

        /// <summary>
        /// Method to check and change the state of the student.
        /// </summary>
        /// <param name="student">The student whose state to be changed.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < this.LowerLimit)
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
            if (student.GradePointAverage > this.UpperLimit)
                student.GradePointStateId = HonoursState.GetInstance().GradePointStateId;
            dataContext.SaveChanges();
        }
    }

    /// <summary>
    /// HonoursState Model: Model to represent the honours state in the database.
    /// </summary>
    public class HonoursState : GradePointState
    {
        //--------------------------------------[ Attributes ]--------------------------------------
        private static HonoursState honoursState;
        private const double LOWER_LIMIT = 3.70,
                             UPPER_LIMIT = 4.50,
                             TUITION_RATE_FACTOR = 0.90;

        //--------------------------------------[ Constructor ]--------------------------------------
        /// <summary>
        /// The constructor for HonoursState.
        /// </summary>
        private HonoursState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.TuitionRateFactor = TUITION_RATE_FACTOR;
        }

        //--------------------------------------[ Methods ]--------------------------------------
        /// <summary>
        /// Method to get the instance of HonoursState to be used.
        /// </summary>
        /// <returns>The instance of HonoursState.</returns>
        public static HonoursState GetInstance()
        {
            if(honoursState == null)
            {
                honoursState = dataContext.HonoursStates.SingleOrDefault();
                if(honoursState == null)
                {
                    honoursState = new HonoursState();
                    dataContext.HonoursStates.Add(honoursState);
                    dataContext.SaveChanges();
                }
            }
            return honoursState;
        }

        /// <summary>
        /// Method to adjust the tuition rate of the student.
        /// </summary>
        /// <param name="student">The student whose tuition rate to be adjusted.</param>
        /// <returns>The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double discountedTuitionRateFactor = 0.05;
            if (dataContext.Registrations.Where(x => x.StudentId == student.StudentId && x.Grade != null).Count() < 5)
                discountedTuitionRateFactor = 0;
            if (student.GradePointAverage > 4.25)
                discountedTuitionRateFactor += 0.02;

            return (TuitionRateFactor - discountedTuitionRateFactor);
        }

        /// <summary>
        /// Method to check and change the state of the student.
        /// </summary>
        /// <param name="student">The student whose state to be changed.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage > this.UpperLimit)
                student.GradePointAverage = this.UpperLimit;
            if (student.GradePointAverage < this.LowerLimit)
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            dataContext.SaveChanges();
        }
    }

    /// <summary>
    /// Course Model: Model to represent a course.
    /// </summary>
    public abstract class Course
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseId { get; set; }

        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Required]
        [Display(Name ="Course\nNumber")]
        public string CourseNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name ="Credit\nHours")]
        public double CreditHours { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tuition\nAmount")]
        public double TuitionAmount { get; set; }

        [Display(Name ="Course\nType")]
        public string CourseType
        {
            get
            {
                return StringFormatter.Remove(GetType().Name, "Course\\d*");
            }
        }

        public string Notes { get; set; }

        //  Navigational Properties
        /// <summary>
        /// Navigation property for the AcademicProgram model.
        /// </summary>
        public virtual AcademicProgram AcademicProgram { get; set; }

        /// <summary>
        /// Navigation property for the Registration model.
        /// </summary>
        public virtual ICollection<Registration> Registration { get; set; }
    }

    /// <summary>
    /// GradedCourse Model: Model to represent a graded course.
    /// </summary>
    public class GradedCourse : Course
    {
        [Required]
        [Display(Name ="Assignment\nWeight")]
        [DisplayFormat(DataFormatString ="{0:P}", ApplyFormatInEditMode = true)]
        public double AssignmentWeight { get; set; }

        [Required]
        [Display(Name = "Midterm\nWeight")]
        [DisplayFormat(DataFormatString = "{0:P}", ApplyFormatInEditMode = true)]
        public double MidtermWeight { get; set; }

        [Required]
        [Display(Name = "Final\nWeight")]
        [DisplayFormat(DataFormatString = "{0:P}", ApplyFormatInEditMode = true)]
        public double FinalWeight { get; set; }
    }

    /// <summary>
    /// MasteryCourse Model: Model to represent how many attempts have been made
    ///                      on the course.
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }
    }

    /// <summary>
    /// AuditCourse Model: Model to represent an auditing of a course.
    /// </summary>
    public class AuditCourse : Course { }

    /// <summary>
    /// Registration Model: Model to represent a registration.
    /// </summary>
    public class Registration
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name ="Registration\nNumber")]
        public long RegistrationNumber { get; set; }

        [Required]
        [Display(Name = "Registration\nDate")]
        [DisplayFormat(DataFormatString ="{0:d}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }

        [DisplayFormat(NullDisplayText ="Ungraded", ApplyFormatInEditMode = true)]
        [Range(0, 1)]
        public double? Grade { get; set; }

        public string Notes { get; set; }

        //  Navigational Properties
        /// <summary>
        /// Navigation property for the Student model.
        /// </summary>
        public virtual Student Student { get; set; }

        /// <summary>
        /// Navigation property for the Course model.
        /// </summary>
        public virtual Course Course { get; set; }
    }
}