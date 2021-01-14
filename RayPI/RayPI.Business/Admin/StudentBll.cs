using RayPI.Entity;
using RayPI.IService;
using RayPI.Model;

namespace RayPI.Business.Admin
{
    public  class StudentBll
    {
        private IStudent IService = new Service.StudentService();

        public Student GetStudentById(long id)
        {
            return IService.GetStudent(id);
        }


        public TableModel<Student> GetPageList(int pageIndex, int pageSize)
        {
            return IService.GetPageList(pageIndex, pageSize);
        }

        public MessageModel<Student> Add(Student entity)
        {
            if (IService.AddStudent(entity))
                return new MessageModel<Student> { Success = true, Msg = "操作成功" };
            else
                return new MessageModel<Student> { Success = false, Msg = "操作失败" };
        }

        public MessageModel<Student> Update(Student entity)
        {
            if (IService.UpdateStudent(entity))
                return new MessageModel<Student> { Success = true, Msg = "操作成功" };
            else 
                return new MessageModel<Student> { Success = false, Msg = "操作失败" };
        }

        public MessageModel<Student> Dels(dynamic[] ids)
        {
            if (IService.Dels(ids))
                return new MessageModel<Student> { Success = true, Msg = "操作成功" };
            else
                return new MessageModel<Student> { Success = false, Msg = "操作失败" };
        }
    }
}
