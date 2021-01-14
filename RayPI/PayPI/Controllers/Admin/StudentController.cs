using Microsoft.AspNetCore.Mvc;
using RayPI.Business.Admin;
using RayPI.Entity;

namespace PayPI.Controllers.Admin
{
    /// <summary>
    /// 后台  学生模板
    /// </summary>
    [Produces("application/json")]
    [Route("api/admin/[controller]")]
    public class StudentController : Controller
    {

        private StudentBll bll = new StudentBll();

        #region base
        /// <summary>
        /// 获取学生分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetStudentPageList(int pageIndex, int pageSize)
        {
            return Json(bll.GetPageList(pageIndex, pageSize));
        }



        /// <summary>
        /// 获取单个学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetStudentById(long id)
        {
            return Json(bll.GetStudentById(id));
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(Student student)
        {
            if (student == null)
                return Json("参数为空");
            return Json(bll.Add(student));
        }

        /// <summary>
        /// 编辑学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Student")]
        public JsonResult Update(Student student)
        {
            if (student == null)
                return Json("参数为空");
            return Json(bll.Update(student));
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonResult Dels(dynamic[] ids)
        {
            if (ids.Length == 0)
                return Json("参数为空");
            return Json(bll.Dels(ids));
        }
        #endregion
    }
}