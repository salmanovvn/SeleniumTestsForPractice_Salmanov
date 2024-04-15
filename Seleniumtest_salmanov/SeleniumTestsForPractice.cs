using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Seleniumtest_salmanov;

public class SeleniumTestsForPractice
{

    //ChromeOptions options = new ChromeOptions();
    //ChromeOptions options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
    WebDriver driver = new ChromeDriver();

    [Test]
    public void Authorization()
    {
        

        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        
        IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
        IWebElement waitForInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name='Username']"))); 
       
        //driver = new ChromeDriver();
        //WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));    
        //By elem_dynamic = By.id("dynamic-element");
        //wait.until(ExpectedConditions.presenceOfElementLocated(elem_dynamic));
        
        
        
        IWebElement login = driver.FindElement(By.CssSelector("input[name='Username']"));
        login.SendKeys("oooethalon@yandex.ru");
        IWebElement pass = driver.FindElement(By.CssSelector("input[id='Password']"));
        pass.SendKeys("PerfectDays2!");
        IWebElement enter = driver.FindElement(By.CssSelector("button[value='login']"));
        enter.Click();
        Thread.Sleep(2000);
        
    }
    public void CreateCommunity()
        {
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
            Thread.Sleep(2000);
            //создать сообщество
            IWebElement createCommunity = driver.FindElement(By.ClassName("vPeNx"));
            createCommunity.Click();
            Thread.Sleep(2000);
            //ввести название, описание
            IWebElement nameCommunity = driver.FindElement(By.ClassName("react-ui-seuwan"));
            nameCommunity.SendKeys("name");
            IWebElement descriptionCommunity = driver.FindElement(By.ClassName("react-ui-r3t2bi"));
            descriptionCommunity.SendKeys("name1");
            //подтвердить
            IWebElement confirmCreation = driver.FindElement(By.ClassName("react-ui-j884du"));
            confirmCreation.Click();
            Thread.Sleep(2000);
        }
    public void DeleteCommunity()
    {
        IWebElement deleteCommunity = driver.FindElement(By.ClassName("jzrpUm"));
        deleteCommunity.Click();
        Thread.Sleep(2000);
        IWebElement confirmDeleteCommunity = driver.FindElement(By.ClassName("react-ui-j884du"));
        confirmDeleteCommunity.Click();
        string currentUrl = driver.Url;
        Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");
        
    }
    public void CloseDriver()
    {
        driver.Quit();
    }
}
