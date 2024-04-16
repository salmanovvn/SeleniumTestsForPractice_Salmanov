using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using FluentAssertions;

namespace Seleniumtests_salmanov;

public class SeleniumTestsForPractice
{
    public WebDriver driver;

    [SetUp]
    public void Authorize()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArguments("--no-sandbox",
            "--start-maximized", "--disable-extensions");
        //driver = new ChromeDriver(options);
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        //IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
        //IWebElement element = wait.Until(ExpectedConditions. ElementToBeClickable(By.CssSelector("input[name='Username']")));
        IWebElement login = driver.FindElement(By.Id("Username"));
        login.SendKeys("oooethalon@yandex.ru");
        IWebElement pass = driver.FindElement(By.Id("Password"));
        pass.SendKeys("PerfectDays2!");
        // войти 
        IWebElement enter = driver.FindElement(By.Name("button"));
        enter.Click();
    }

    [Test]
public void Authorization()
{
    IWebElement current = driver.FindElement(By.CssSelector("img[alt='Логотип']"));
    current.Should().NotBeNull();
}
    [Test]
    public void GetCommunity()
    {
        Thread.Sleep(3000); //увидеть чтоб открылись сообщества
        IWebElement sideMenu = driver.FindElement(By.CssSelector("button[data-tid='SidebarMenuButton']"));
        sideMenu.Click();

        IWebElement communityButton =
            driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div[3]/div/div[1]/div[2]/div/a[4]"));
        communityButton.Click();
        IWebElement communityHeader = driver.FindElement(By.CssSelector("section[data-tid='PageHeader']"));
        communityHeader.Should().NotBeNull();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/communities");
        Thread.Sleep(3000); //увидеть чтоб открылись сообщества
    }

    [Test]
    public void CreateCommunity()
    {
        GetCommunity();            
        IWebElement createCommunity = driver.FindElement(By.CssSelector("svg[viewBox='0 0 18 18']"));
        createCommunity.Click();IWebElement nameCommunity = driver.FindElement(By.CssSelector("label[data-tid='Name']"));
        nameCommunity.SendKeys("name");
        IWebElement descriptionCommunity = driver.FindElement(By.CssSelector("label[data-tid='Message']"));
        descriptionCommunity.SendKeys("name1");
        IWebElement confirmCreation = driver.FindElement(By.CssSelector("span[data-tid='CreateButton']"));
        confirmCreation.Click();
        DeleteCommunity();        }
    public void DeleteCommunity()
    {
        IWebElement deleteCommunity = driver.FindElement(By.CssSelector("button[data-tid='DeleteButton']"));
        deleteCommunity.Click();
        IWebElement confirmDeleteCommunity = driver.FindElement(By.ClassName("react-ui-button-caption"));
        confirmDeleteCommunity.Click();        
        //string currentUrl = driver.Url;
        //Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");        
    }

    //[Test]
    public void DropDowmMenu()
    {
        IWebElement dropDownMenu = driver.FindElement(By.CssSelector("div[data-tid='Avatar']"));
        dropDownMenu.Click();
        IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Thread.Sleep(3000);
        //IWebElement waitForMenu = wait.Until(ExpectedConditions.ElementIsVisible(
        //    By.CssSelector(
        //       "body > div.react-ui > div > div > div > div > div > div > span.sc-iGkqmO.ckwDLe.react-ui-162kz0e > span")));
        dropDownMenu.Click();
        Thread.Sleep(3000);
        //IWebElement waitForMenu = wait.Until.not(ExpectedConditions.ElementToBeClickable(
        //    By.CssSelector(
        //      "body > div.react-ui > div > div > div > div > div > div > span.sc-iGkqmO.ckwDLe.react-ui-162kz0e > span"))));
        
        //

    }

    [Test]
    public void ChangeSecondEmail()
    {
        Thread.Sleep(3000);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/profile/settings/edit");
        //Thread.Sleep(7000);
        // String h1community = driver.FindElement(By.CssSelector("#root > section > section.sc-ckTSus.kVjkXt > section.sc-fujyAs.sc-crzoAE.cZJxtI.DykGo > div.sc-ksluID.kFmqyc > h1")).Text;
        // h1community.Should().Be("Сообщества");
        // IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        // IWebElement waitForEdit = wait.Until(ExpectedConditions.ElementIsVisible(
        //      By.CssSelector(
        //         "#root > section > section.sc-ckTSus.kVjkXt > section.sc-fujyAs.sc-crzoAE.cZJxtI.DykGo > div.sc-eYWepU.klKumv > button:nth-child(1)")));
        //
        //String h1EditProfile = driver.FindElement(By.XPath("#root > section > section.sc-ckTSus.kVjkXt > section.sc-fujyAs.sc-crzoAE.cZJxtI.DykGo > div.sc-ksluID.kFmqyc > h1")).Text;
        //h1EditProfile.Should().Be("Редактирование профиля");
        IWebElement secondEmail = driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/section[3]/div[2]/div[3]/label[2]/span[2]/input"));

        secondEmail.SendKeys(Keys.Control+"a");
        //Thread.Sleep(3000);
        secondEmail.SendKeys(Keys.Backspace);
        //Thread.Sleep(3000);
        secondEmail.SendKeys("second@SecondEmail.ru");
        //Thread.Sleep(3000);
        IWebElement confirmChange = driver.FindElement(By.CssSelector("button[class='sc-juXuNZ kVHSha']"));
        confirmChange.Click();
        //Thread.Sleep(3000);
    }





    [TearDown] 
    public void TearDown()
    { driver.Quit();}

}
// IWebElement h1News = driver.FindElement(By.CssSelector("[data-tid='Title']"));
// h1News.Should().NotBeNull();