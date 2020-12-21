using System;

namespace RayPI.Entity
{
    /// <summary>
    /// 
    /// </summary>
   // [SugarTable("Student")]
    public class Student
    {
        public long TId { get; set; }
        /// <summary>
        /// 班级Id
        /// </summary>
        public long ClassId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}
