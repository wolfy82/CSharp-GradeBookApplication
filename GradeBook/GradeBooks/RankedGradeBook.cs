using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name) => Type = GradeBookType.Ranked;

        public override char GetLetterGrade(double averageGrade)
        {

            var grade = 'F';
            var threshold = Students.Count * .2;
            var gradeLevel = 1;

            var orderedStudents = Students.OrderBy(x => x.AverageGrade).Reverse().ToList();

            // Loop through student grades to determine where the new grade falls
            // Increments the grade bump when the threshold is met.
            for (var i = 0; i < orderedStudents.Count; i++)
            {
                
                if (orderedStudents[i].AverageGrade > averageGrade)
                {
                    if ((i + 1) % Convert.ToInt32(threshold.ToString()) == 0)
                    {
                        gradeLevel += 1;
                    }
                }
                else
                    break;                                                                
            }
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }
            else
            {
                switch (gradeLevel)
                {
                    case 1:
                        grade = 'A';
                        break;
                    case 2:
                        grade = 'B';
                        break;
                    case 3:
                        grade = 'C';
                        break;
                    case 4:
                        grade = 'D';
                        break;
                    default:
                        grade = 'F';
                        break;
                }
            }
            
            return grade;
            
        }
    }
}
