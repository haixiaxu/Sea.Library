using RayPI.Entity;
using RayPI.Model;

namespace RayPI.IService
{
    public interface IStudent
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        TableModel<Student> GetPageList(int pageIndex, int pageSize);

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(long id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        bool AddStudent(Student student);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        bool UpdateStudent(Student student);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Dels(dynamic[] ids);
    }
}
