using System.Net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using FluentAssertions;

namespace Seleniumtests_salmanov;

public class SeleniumTestsForPractice
{
    private WebDriver driver;
    private WebDriverWait wait;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        SetUp();
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities?activeTab=isAdministrator");
        try
        {
            var lookingForCommunity = driver.FindElement(By.CssSelector("div[data-tid='Feed']")).FindElements(By.CssSelector("a[data-tid='Link']"));
        }
        catch (Exception e)
        {
            throw;
        }

        var allMyCommunity = driver.FindElement(By.CssSelector("div[data-tid='Feed']")).FindElements(By.CssSelector("a[data-tid='Link']"));
        int countMyCommunity = allMyCommunity.Count-1;
        for (int i = 0; i < countMyCommunity; i++)
        {
                GoToSettingsMyCommunity();
                DeleteCommunityInSettings(); 
                CheckNews();
        }
        driver.Close();
        driver.Quit();
    }


    [SetUp]
    public void SetUp()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArguments("--no-sandbox",
            "--start-maximized", "--disable-extensions");
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        wait=new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        Authorize();

    }
    
    public void CheckNews()
    {
        IWebElement current = driver.FindElement(By.CssSelector("div[data-tid='Feed']"));
        current.Should().NotBeNull();
    }
    public void DeleteCommunityInSettings()
    {
            IWebElement deleteCommunity = driver.FindElement(By.CssSelector("button[data-tid='DeleteButton']"));
            deleteCommunity.Click();
            IWebElement confirmDeleteCommunity = driver.FindElement(By.CssSelector("span[data-tid='DeleteButton']"));
            confirmDeleteCommunity.Click();
            CheckNews();
    }
    
    public void Authorize()
    {
        string loginText = "oooethalon@yandex.ru";
        string passwordText = "PerfectDays2!";
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        IWebElement login = driver.FindElement(By.Id("Username"));
        login.SendKeys(loginText);
        IWebElement pass = driver.FindElement(By.Id("Password"));
        pass.SendKeys(passwordText);
        IWebElement enter = driver.FindElement(By.Name("button"));
        enter.Click();
        wait.Until(ExpectedConditions. ElementIsVisible(By.CssSelector("div[data-tid='Feed']")));
    }
    
    [Test] //Авторизация
    public void Authorization()
    {
        CheckNews();
    }
    [Test] //Поиск на странице "Новости" по имени и фамилии
    public void SearchAccounts()
    {
        IWebElement panelForSearch = driver.FindElement(By.CssSelector("[data-tid='SearchBar']"));
        panelForSearch.Click();
        IWebElement inputForSearch = driver.FindElement(By.CssSelector("[placeholder='Поиск сотрудника, подразделения, сообщества, мероприятия']"));  
        string fullName = "Виктор Салманов";
        inputForSearch.SendKeys(fullName);
        string chooseFirst = driver.FindElement(By.CssSelector("div[data-tid='ScrollContainer__inner']")).FindElement(By.XPath("//*[contains(text(),fullName)]")).Text;
        chooseFirst.Should().Contain(fullName);
        
    }
   
    [Test] //Попасть в сообщества через боковое меню
    public void GetCommunities()
    {
        IWebElement communityButton=
            driver.FindElements(By.CssSelector("a[data-tid='Community']"))[0];
        communityButton.Click();
        IWebElement communityHeader = driver.FindElement(By.CssSelector("section[data-tid='PageHeader']"));
        communityHeader.Should().NotBeNull();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/communities");
    }

    [Test] //Создать сообщество
    public void CreateCommunity()
    {
        GetCommunities();            
        IWebElement createCommunity = driver.FindElement(By.CssSelector("section[data-tid='PageHeader']")).FindElement(By.TagName("button"));
        createCommunity.Click();
        IWebElement nameCommunity = driver.FindElement(By.CssSelector("label[data-tid='Name']"));
        string name = "1name";
        nameCommunity.SendKeys(name);
        IWebElement descriptionCommunity = driver.FindElement(By.CssSelector("label[data-tid='Message']"));
        descriptionCommunity.SendKeys("name1");
        IWebElement confirmCreation = driver.FindElement(By.CssSelector("span[data-tid='CreateButton']"));
        confirmCreation.Click();
        IWebElement getToNewCommunity = driver.FindElement(By.CssSelector("span[data-tid='Title']")).FindElement(By.TagName("a"));
        getToNewCommunity.Click();
        string checkName = driver.FindElement(By.CssSelector("div[data-tid='Title']")).Text;
        checkName.Should().Be(name);
    }
    [Test] //Перейти в свое сообщество через вкладку "Я модератор"
    public void GoToMyLastCommunity()
    {
        CheckNews();
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities?activeTab=isAdministrator");
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1[data-tid='Title']")));
        IWebElement chooseFirstCommunity = driver.FindElements(By.CssSelector("a[data-tid='Link']"))[0];
        chooseFirstCommunity.Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-tid='Title']")));
        IWebElement checkCommunity = driver.FindElement(By.CssSelector("div[data-tid='Title']"));
        checkCommunity.Should().NotBeNull();
    }
    [Test] //Перейти в настройки своего сообщества
    public void GoToSettingsMyCommunity()
    {
        GoToMyLastCommunity();
        IWebElement dropdownButton = driver.FindElements(By.CssSelector("div[data-tid='DropdownButton']"))[1];
        dropdownButton.Click();
        IWebElement settings = driver.FindElement(By.CssSelector("span[data-tid='Settings']"));
        settings.Click();
        IWebElement checkSettings = driver.FindElement(By.CssSelector("div[data-tid='SettingsTabWrapper']"));
        checkSettings.Should().NotBeNull();
    }
    

    [Test] //Перейти в редактирование своего профиля
    public void GetToEditProfile()
    {
        IWebElement dropDownMenu = driver.FindElement(By.CssSelector("div[data-tid='Avatar']"));
        dropDownMenu.Click();
        IWebElement editProfile = driver.FindElement(By.CssSelector("span[data-tid='ProfileEdit']"));
        editProfile.Click();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/profile/settings/edit");
    }

    [Test] //Изменить дополнительный Email
    public void ChangeSecondEmail()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/profile/settings/edit");
        IWebElement secondEmail =driver.FindElement(By.CssSelector("div[data-tid='AdditionalEmail']")).
            FindElement(By.CssSelector("label[data-tid='Input']"));
        secondEmail.SendKeys(Keys.Control+"a");
        secondEmail.SendKeys(Keys.Backspace);
        secondEmail.SendKeys("second@SecondEmail.ru");
        IWebElement confirmChange = driver.FindElement(By.XPath("//*[contains(text(),'Сохранить')]")); 
        confirmChange.Click();
        IWebElement checkProfileName = driver.FindElement(By.CssSelector("div[data-tid='EmployeeName']"));
        checkProfileName.Should().NotBeNull();
    }
    
    [TearDown] 
    public void TearDown()
    {
        driver.Close();
        driver.Quit();
    }
}
public class TestStaffWithNoMaximizedWindow
{
    private WebDriver driver;
    private WebDriverWait wait;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        wait=new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Authorize();
    }

    public void Authorize()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        IWebElement login = driver.FindElement(By.Id("Username"));
        login.SendKeys("oooethalon@yandex.ru");
        IWebElement pass = driver.FindElement(By.Id("Password"));
        pass.SendKeys("PerfectDays2!");
        IWebElement enter = driver.FindElement(By.Name("button"));
        enter.Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-tid='Feed']")));
    }

    [Test] // Перейти в сообщества через боковое меню в том случае, если оно скрыто
    public void GetCommunity()
    {
        IWebElement sideMenu = driver.FindElement(By.CssSelector("button[data-tid='SidebarMenuButton']"));
        sideMenu.Click();
        IWebElement communityButton=
            driver.FindElements(By.CssSelector("a[data-tid='Community']"))[1];
        communityButton.Click();
        IWebElement communityHeader = driver.FindElement(By.CssSelector("section[data-tid='PageHeader']"));
        communityHeader.Should().NotBeNull();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/communities");
    }
    [TearDown] 
    public void TearDown()
    {
        driver.Close();
        driver.Quit();
    }
}
