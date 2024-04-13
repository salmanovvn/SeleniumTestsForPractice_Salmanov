using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seleniumtests_salmanov;

public class SeleniumTestsForPractice
{
    [Test]
    public void Authorization ()
    {
        
        // зайти в браузер 
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox",
            "--start-maximized", "--disable-extensions");
        var driver = new ChromeDriver(options);
        // перейти на хост
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        // ввести логин,пароль
        Thread.Sleep(3000);
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("oooethalon@yandex.ru");
        var pass = driver.FindElement(By.Id("Password"));
        pass.SendKeys("PerfectDays2!");
        // войти 
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        Thread.Sleep(3000);
        // проверить что нужная страница
        var currentUrl = driver.Url;
        Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");
        
        //выйти
        driver.Quit();
    }
}