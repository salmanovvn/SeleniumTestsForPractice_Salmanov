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
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        IWebElement login = driver.FindElement(By.Id("Username"));
        login.SendKeys("oooethalon@yandex.ru");
        IWebElement pass = driver.FindElement(By.Id("Password"));
        pass.SendKeys("PerfectDays2!");
        IWebElement enter = driver.FindElement(By.Name("button"));
        enter.Click();
    }

    [Test] //1
public void AuthorizationGetToNews()
{
    IWebElement current = driver.FindElement(By.CssSelector("div[data-tid='Feed']"));
    current.Should().NotBeNull();
}
    [Test] //2
    public void GetCommunity()
    {
        Thread.Sleep(3000); //увидеть чтоб открылись сообщества
        IWebElement communityButton=
            driver.FindElements(By.CssSelector("a[data-tid='Community']"))[0];
        communityButton.Click();
        IWebElement communityHeader = driver.FindElement(By.CssSelector("section[data-tid='PageHeader']"));
        communityHeader.Should().NotBeNull();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/communities");
        Thread.Sleep(3000); //увидеть чтоб открылись сообщества
    }

    [Test] //3
    public void CreateCommunity()
    {
        GetCommunity();            
        IWebElement createCommunity = driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/section/div[2]/span/button"));
        createCommunity.Click();
        IWebElement nameCommunity = driver.FindElement(By.CssSelector("label[data-tid='Name']"));
        string name = "1name";
        nameCommunity.SendKeys(name);
        IWebElement descriptionCommunity = driver.FindElement(By.CssSelector("label[data-tid='Message']"));
        descriptionCommunity.SendKeys("name1");
        IWebElement confirmCreation = driver.FindElement(By.CssSelector("span[data-tid='CreateButton']"));
        confirmCreation.Click();
        IWebElement getToNewCommunity =
            driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/section/span/a"));
        getToNewCommunity.Click();
        string checkName = driver.FindElement(By.CssSelector("div[data-tid='Title']")).Text;
        checkName.Should().Be(name);
    }
    public void DeleteCommunityInSettings()
    {
        IWebElement deleteCommunity = driver.FindElement(By.CssSelector("button[data-tid='DeleteButton']"));
        deleteCommunity.Click();
        IWebElement confirmDeleteCommunity = driver.FindElement(By.ClassName("react-ui-button-caption"));
        confirmDeleteCommunity.Click();        
        //string currentUrl = driver.Url;
        //Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");        
    }

    [Test] //5
    public void GetToEditProfile()
    {
        IWebElement dropDownMenu = driver.FindElement(By.CssSelector("div[data-tid='Avatar']"));
        dropDownMenu.Click();
        IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement editProfile = driver.FindElement(By.CssSelector("span[data-tid='ProfileEdit']"));
        editProfile.Click();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/profile/settings/edit");
    }

    [Test] //6
    public void ChangeSecondEmail()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/profile/settings/edit");
        IWebElement secondEmail = driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/section[3]/div[2]/div[3]/label[2]/span[2]/input"));
        secondEmail.SendKeys(Keys.Control+"a");
        secondEmail.SendKeys(Keys.Backspace);
        secondEmail.SendKeys("second@SecondEmail.ru");
        IWebElement confirmChange = driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/section[1]/div[2]/button[1]"));
        confirmChange.Click();
    }
    
    
    [Test] //7
    public void GetToSettingsMyCommunity() 
    {
        
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
        IWebElement myCommunity = driver.FindElements(By.CssSelector("a[data-tid='Item']"))[2];
        myCommunity.Click();
        IWebElement chooseFirstCommunity = driver.FindElements(By.CssSelector("a[data-tid='Link']")).First(el => el.Displayed);
        chooseFirstCommunity.Click();
        IWait<IWebDriver> wait=new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
        IWebElement element = wait.Until(ExpectedConditions. ElementToBeClickable(By.CssSelector("div[data-tid='Title']")));
        IWebElement dropdownButton = driver.FindElements(By.CssSelector("div[data-tid='DropdownButton']"))[1];
        dropdownButton.Click();
    
        IWebElement settings = driver.FindElement(By.CssSelector("span[data-tid='Settings']"));
        settings.Click();
        //string currentUrl = driver.Url;
        //Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");        
    }

    [Test] //8
    public void DeleteCommunity()
    {
        GetToSettingsMyCommunity();
        DeleteCommunityInSettings();
        AuthorizationGetToNews();
    }

    [Test] //8
    public void SearchAccounts()
    {
        Thread.Sleep(3000);
        IWebElement inputForSearch = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/header/div/div[2]/div/span2828"));  
        Console.WriteLine("yes");
        inputForSearch.Click();
        Console.WriteLine("yes");
        Thread.Sleep(3000);
        inputForSearch.SendKeys("Виктор Салманов");
        Console.WriteLine("yes");
        Thread.Sleep(3000);
        inputForSearch.SendKeys(Keys.Enter);
        Thread.Sleep(3000);
        IWebElement chooseFirst =
      driver.FindElements(By.CssSelector("button[data-tid='ComboBoxMenu__item']")).First(el => el.Displayed);
        chooseFirst.Click();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/profile/eb2fe5a6-85ae-4f7b-bc91-d42f3a39fd8d");
        // IWebElement inputForSearch = driver.FindElement(By.CssSelector("div[title='Виктор Салманов']"));
        // inputForSearch.SendKeys("Виктор Салманов");
              
    }
    




    [TearDown] 
    public void TearDown()
    { driver.Quit();}

}


public class TestStaffWithNoMaximizedWindow
{
    public WebDriver driver;

    [SetUp]
    public void Authorize()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        IWebElement login = driver.FindElement(By.Id("Username"));
        login.SendKeys("oooethalon@yandex.ru");
        IWebElement pass = driver.FindElement(By.Id("Password"));
        pass.SendKeys("PerfectDays2!");
        IWebElement enter = driver.FindElement(By.Name("button"));
        enter.Click();
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
    [TearDown] 
    public void TearDown()
    { driver.Quit();}
}

// IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
// IWebElement element = wait.Until(ExpectedConditions. ElementToBeClickable(By.CssSelector("input[name='Username']")));
// IWebElement communityButton =
//       driver.FindElements(By.CssSelector("a[data-tid='Community']")).First(el => el.Displayed);



//driver.FindElements(By.CssSelector("[data-tid='Input']")) ищет на странице все элементы, и возвращает массив, да, а там с нуля
// driver.FindElements(By.CssSelector("[data-tid='Input']"))[2];
// private bool IsElementPresent(By by)   
// {
//     try
//     {
//         driver.FindElement(by);
//         return true;
//     }
//     catch (NoSuchElementException)
//     {
//         return false;
//     }
// }