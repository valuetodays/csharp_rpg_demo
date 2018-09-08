using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    class ResourceContextDeterminer
    {
        // 资源文件的根目录
        private static string basePath = null;

        /// <summary>
        /// 获取asset\目录下的资源文件
        /// </summary>
        /// <param name="fileName">文件路径中不要再使用asset，文件路径请以\开始，不以\开始会自动添加</param>
        /// <returns></returns>
        public static string GetAssetPath(string fileName)
        {
            if (basePath == null)
            {
                basePath = DetermineResourceBasePath() + "\\asset";
            }
            fileName = fileName.Replace("/", "\\");
            String finalFileName = basePath + (fileName.StartsWith("\\") ? "" : "\\") + fileName;
            if (!File.Exists(@finalFileName))
            {
                Console.WriteLine("File " + finalFileName + " does not exists.");
            }
            return finalFileName;
        }

        private static string DetermineResourceBasePath()
        {
            ResourcePathContext assetContext = new ResourcePathContext(new ReleaseResourcePathStratage());
            string fileName = "/asset/map1.png";
            Console.WriteLine("=== 尝试使用 " + assetContext.getName() + "模式加载资源……");
            bool flag = assetContext.checkFileExists(fileName);
            if (!flag)
            {
                assetContext.assignResourcePathStrategy(new DebugResourcePathStratage());
                Console.WriteLine("=== 加载资源失败，切换为 " + assetContext.getName() + "模式");
                flag = assetContext.checkFileExists(fileName);
            }
            if (!flag)
            {
                MessageBox.Show("启动失败：找不到资源文件。");
                Common.exit();
            }
            Console.WriteLine("========使用模式 " + assetContext.getName() + " 运行程序。========");
            return assetContext.getBasePath();
        }

    }
}
