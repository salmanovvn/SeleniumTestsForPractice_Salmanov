using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Seleniumtest_salmanov;

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
        //driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        // driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        //
        // Implicit Wait можно использовать для:
        //
        // ожидания полной загрузки страницы — pageLoadTimeout();
        // ожидания появления элемента на странице — implicitlyWait();
        // ожидания выполнения асинхронного запроса — setScriptTimeout();
        //
        
        
        // перейти на хост
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        // ввести логин,пароль
        Thread.Sleep(2000);
        
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("oooethalon@yandex.ru");
        var pass = driver.FindElement(By.Id("Password"));
        pass.SendKeys("PerfectDays2!");
        // войти 
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        Thread.Sleep(2000);
        //await Task.Delay(5000);
        // проверить что нужная страница
        //var currentUrl = driver.Url;
        //Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");
      
      
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
        Thread.Sleep(2000);
        //создать сообщество
        var createCommunity = driver.FindElement(By.ClassName("vPeNx"));
        createCommunity.Click();
        Thread.Sleep(2000);
        //ввести название, описание
        var nameCommunity = driver.FindElement(By.ClassName("react-ui-seuwan"));
        nameCommunity.SendKeys("name");
        var descriptionCommunity = driver.FindElement(By.ClassName("react-ui-r3t2bi"));
        descriptionCommunity.SendKeys("name1");
        //подтвердить
        var confirmCreation = driver.FindElement(By.ClassName("react-ui-j884du"));
        confirmCreation.Click();
        //перейти в новое сообщество
        // Thread.Sleep(2000);
        // var pageNewCommunity = driver.FindElement(By.ClassName("sc-eCApnc"));
        // pageNewCommunity.Click();
        Thread.Sleep(2000);
        // //kebab menu
        //
        // var kebabMenu = driver.FindElement(By.ClassName("gkKHtQ"));
        // kebabMenu.Click();
        // var settings = driver.FindElement(By.ClassName("sc-eWnToP"));
        // settings.Click();
        // Thread.Sleep(2000);
        var deleteCommunity = driver.FindElement(By.ClassName("jzrpUm"));
        deleteCommunity.Click();
        Thread.Sleep(2000);
        var confirmDeleteCommunity = driver.FindElement(By.ClassName("react-ui-j884du"));
        confirmDeleteCommunity.Click();
        
         //проверить что нужная страница
        var currentUrl = driver.Url;
        Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");
        
        // var searchByName = driver.FindElement(By
        // searchByName.Click();
        //xpath = " //input[@name='passwd'] ";
        //выйти
        driver.Quit();
    }
   
    // public void CreateCommunity()
    //
    // {
    //
    // }

    // void UseThreadSleep(int sleepMilliseconds = 2000)
    // {
    //     Console.WriteLine($"Before sleep: Thread id = {Environment.CurrentManagedThreadId}");
    //     Thread.Sleep(sleepMilliseconds);
    //     Console.WriteLine($"After sleep: Thread id = {Environment.CurrentManagedThreadId}");
    // }
    // static async Task UseTaskDelay(int delayMilliseconds = 2000)
    // {
    //     Console.WriteLine($"Before delay: Thread id = {Environment.CurrentManagedThreadId}");
    //     await Task.Delay(delayMilliseconds);
    //     Console.WriteLine($"After delay: Thread id = {Environment.CurrentManagedThreadId}");
    // }
    // {
    
}