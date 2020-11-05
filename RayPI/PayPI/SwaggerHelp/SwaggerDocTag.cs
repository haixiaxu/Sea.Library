

using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PayPI.SwaggerHelp
{
    /// <summary>
    /// Swagger注释帮助类
    /// </summary>
    public class SwaggerDocTag : IDocumentFilter
    {

        /// <summary>
        /// 添加附加注释
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            /*swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag{Name="Values",Description="测试模块"}
            };*/
            swaggerDoc.Tags = GetControllerDesc();
        }
        /// <summary>
        /// 从xml注释中读取控制器注释
        /// </summary>
        /// <returns></returns>
        private List<OpenApiTag> GetControllerDesc()
        {
            var tagList = new List<OpenApiTag>();
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var xmlPath = Path.Combine(basePath, "APIHelp.xml");
            //检查xml注释文件是否存在
            if (!File.Exists(xmlPath))
                return tagList;
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            //三级节点的那么属性值
            string memberName = string.Empty;
            //控制器完整名称
            string controllerName = string.Empty;
            //控制器去Controller名称
            string key = string.Empty;
            //控制器注释
            string value = string.Empty;

            //循环三级节点member
            foreach (XmlNode node in xmlDoc.SelectNodes("//member"))
            {
                memberName = node.Attributes["name"].Value;
                //T:开头的代表类
                if (memberName.StartsWith("T:"))
                {
                    string[] arrPath = memberName.Split('.');
                    controllerName = arrPath[arrPath.Length - 1];
                    //Controller结尾代表控制器
                    if (controllerName.EndsWith("Controller"))
                    {
                        //注释节点
                        XmlNode summaryNode = node.SelectSingleNode("summary");
                        key = controllerName.Remove(controllerName.Length - "Controller".Length, "Controller".Length);
                        if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !tagList.Contains(new OpenApiTag { Name = key }))
                        {
                            value = summaryNode.InnerText.Trim();
                            tagList.Add(new OpenApiTag { Name = key, Description = value });
                        }
                    }
                }
            }
            return tagList;
        }
    }
}









