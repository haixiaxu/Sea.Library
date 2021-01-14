using RayPI.Entity;
using RayPI.IService;
using RayPI.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RayPI.Service
{
    public class StudentService : BaseDB, IStudent
    {
        public SimpleClient<Student> sdb = new SimpleClient<Student>(BaseDB.GetClient());
        public bool AddStudent(Student student)
        {
           return sdb.Insert(student);
        }

        public bool Dels(dynamic[] ids)
        {
            return sdb.DeleteByIds(ids);
        }

        public TableModel<Student> GetPageList(int pageIndex, int pageSize)
        {
            PageModel pageModel = new PageModel() {
                PageIndex = pageIndex, PageSize = pageSize
            };
            Expression<Func<Student, bool>> ex = (c => 1 == 1);
            List<Student> students = sdb.GetPageList(ex, pageModel);
            TableModel<Student> tableModel = new TableModel<Student>()
            {
                Code = 0,
                Count = pageModel.PageCount,
                Data = students,
                Msg = "成功"
            };
            return tableModel; 

        }

        public Student GetStudent(long id)
        {
            return sdb.GetById(id);
        }

        public bool UpdateStudent(Student student)
        {
            return sdb.Update(student);
        }
    }
}
