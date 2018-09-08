using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    class ResourcePathContext : ResourcePathStrategy
    {
        private ResourcePathStrategy resourcePathStrategy;

        public ResourcePathContext(ResourcePathStrategy strategy)
        {
            this.resourcePathStrategy = strategy;
        }

        public void assignResourcePathStrategy(ResourcePathStrategy strategy)
        {
            this.resourcePathStrategy = strategy;
        }

        public override string getBasePath()
        {
            return this.resourcePathStrategy.getBasePath();
        }
        public override string getName()
        {
            return this.resourcePathStrategy.getName();
        }

    }

    abstract class ResourcePathStrategy
    {
        /// <summary>
        /// 获取资源文件根目录
        /// </summary>
        /// <returns></returns>
        abstract public string getBasePath();

        /// <summary>
        /// 获取策略名称
        /// </summary>
        /// <returns></returns>
        abstract public string getName();

        /// <summary>
        /// 检查指定文件是否存在，存在则说明该策略可用
        /// </summary>
        /// <param name="fileName">文件路径请以/开始</param>
        /// <returns></returns>
        public bool checkFileExists(string fileName)
        {
            return File.Exists(@getBasePath() + fileName);
        }
    }
    class ReleaseResourcePathStratage : ResourcePathStrategy
    {
        override public string getBasePath()
        {
            return Application.StartupPath;
        }
        public override string getName()
        {
            return "Release";
        }
    }

    class DebugResourcePathStratage : ResourcePathStrategy
    {
        private string basePath = new DirectoryInfo(Application.StartupPath).Parent.Parent.FullName;
        override public string getBasePath()
        {
            return basePath;
        }
        public override string getName()
        {
            return "Debug";
        }
    }
}
