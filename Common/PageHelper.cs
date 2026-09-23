using System.Windows.Forms;

namespace Common
{
    public class PageHelper
    {
        /// <summary>
        /// 页面切换方法，将传入的页面添加进传入的容器内
        /// </summary>
        /// <typeparam name="T">传入页面的类型</typeparam>
        /// <param name="container">传入的页面容器</param>
        /// <param name="userControl">传入的用户控件页面对象</param>
        public static void SwitchPage<T>(Control container, ref UserControl userControl) where T : UserControl, new()
        {
            // 清空页面
            container.Controls.Clear();
            // 如果页面不存在则创建一个
            if (userControl == null)
            {
                userControl = new T();
            }
            userControl.Dock = DockStyle.Fill;
            container.Controls.Add(userControl);
        }
    }
}
