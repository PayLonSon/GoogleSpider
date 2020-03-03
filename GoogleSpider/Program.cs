using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            //設定輸入參數
            startCrawlerasync("火災");
           // Console.ReadLine();
        }

        private static async Task startCrawlerasync(string keyword)
        {
            //
            String newsUrl = "&source=lnms&tbm=nws";
            String parseTarget = string.Format(
                "https://www.google.com.tw/search?q={0}&start={1}0{2}",
                keyword, 0, newsUrl);
            Console.WriteLine(parseTarget);

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(parseTarget);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.Manage().Timeouts().PageLoad  = TimeSpan.FromSeconds(2);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(2);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            Console.WriteLine("[INFO] wait for 2000 second");
            //找到Post的Container
            var PostContainerElement = driver.FindElements(By.XPath("//div[@class='g']/div"));
            Console.WriteLine("[INFO]"+PostContainerElement.Count);
            //每一行
            //var Rows = PostContainerElement.FindElements(By.ClassName("_70iju"));
            foreach (var row in PostContainerElement)
            {
                var title = row.FindElement(By.TagName("h3"));
                var webUrl = row.FindElement(By.TagName("a"));
                var img = row.FindElement(By.TagName("img"));
                var getDateRows = row.FindElements(By.TagName("span"));
                var getDateRow = getDateRows.Last();
                Console.WriteLine(title.Text);
                Console.WriteLine(webUrl.GetAttribute("href"));
                Console.WriteLine(img.GetAttribute("src"));
                Console.WriteLine(getDateRow.Text);
            }
            driver.Close();

        }

    }
}
