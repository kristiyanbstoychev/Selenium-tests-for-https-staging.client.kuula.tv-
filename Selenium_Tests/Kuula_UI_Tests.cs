using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Kuula_Tests
{
    
    public class Kuula_UI_Tests
    {
        IWebDriver driver;


        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Create_new_account_from_the_SingUp_button_Valid_Data()
        {
            driver.Url = "https://staging.client.kuula.tv/";
            Thread.Sleep(1000);
            var buttonSignUp = driver.FindElement(By.XPath("//button[contains(.,'Sign Up')]"));
            buttonSignUp.Click();
            Thread.Sleep(3000);
            var nameField = driver.FindElement(By.Id("name"));
            Random rnd = new Random();
            int random = rnd.Next(0, 5000);
            var userName = "testUser" + random;
            nameField.SendKeys(userName);
            var emailField = driver.FindElement(By.Id("email"));
            var userEmail = "testUser" + random + "@test.com";
            emailField.SendKeys(userEmail);
            var emailConfrimField = driver.FindElement(By.Id("emailConfirm"));
            emailConfrimField.SendKeys(userEmail);
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("123456");

            var buttonRegister = driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Register my account')]"));
            buttonRegister.Click();
            Thread.Sleep(4000);
            var yourProfilePageTitle = driver.Title;
            /*var myAccountMenu = driver.FindElement(By.ClassName("active"));*/
            var paymentsMenu = driver.FindElement(By.XPath("//a[@href='/profile/payments'][contains(.,'Payments')]"));
            var activityMenu = driver.FindElement(By.XPath("//a[@href='/profile/activity'][contains(.,'Activity')]"));
            var bookingsMenu = driver.FindElement(By.XPath("//a[@href='/profile/bookings'][contains(.,'Bookings')]"));
            var billingMenu = driver.FindElement(By.XPath("//a[@href='/profile/subscriptions'][contains(.,'Billing')]"));
            var myProfileMenu = driver.FindElement(By.XPath("/html/body/div[1]/header/div/div[2]/button"));
            var upcomingClasses = driver.FindElement(By.ClassName("select-box"));
            var welcomeMsg = driver.FindElement(By.XPath("/html/body/div[1]/main/div/section[1]"));

            Assert.AreEqual("Your Profile - Kuula", yourProfilePageTitle);
            Assert.IsTrue(myProfileMenu.Displayed);
            Assert.IsTrue(upcomingClasses.Displayed);
            /*Assert.IsTrue(myAccountMenu.Displayed);*/
            Assert.IsTrue(paymentsMenu.Displayed);
            Assert.IsTrue(activityMenu.Displayed);
            Assert.IsTrue(bookingsMenu.Displayed);
            Assert.IsTrue(billingMenu.Displayed);
            Assert.IsTrue(welcomeMsg.Displayed);

            myProfileMenu.Click();
            Thread.Sleep(1000);
            var logOutButton = driver.FindElement(By.XPath("//div[@role='menuitem'][contains(.,'Sign Out')]"));
            logOutButton.Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void Try_to_register_with_an_existing_email_address()
        {
            driver.Url = "https://staging.client.kuula.tv/";
            Thread.Sleep(1000);
            var buttonSignUp = driver.FindElement(By.XPath("//button[contains(.,'Sign Up')]"));
            buttonSignUp.Click();
            Thread.Sleep(3000);

            var nameField = driver.FindElement(By.Id("name"));
            Random rnd = new Random();
            int random = rnd.Next(0, 5000);
            var userName = "testUser" + random;
            nameField.SendKeys(userName);
            var emailField = driver.FindElement(By.Id("email"));
            var userEmail = "testUser" + random + "@test.com";
            emailField.SendKeys(userEmail);
            var emailConfrimField = driver.FindElement(By.Id("emailConfirm"));
            emailConfrimField.SendKeys(userEmail);
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("12345");
            var buttonRegister = driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Register my account')]"));
            buttonRegister.Click();
            Thread.Sleep(2000);


            var errorMsg = driver.FindElement(By.XPath("(//div[contains(.,'Must be 6 characters or more')])[6]"));

            Assert.IsTrue(errorMsg.Displayed);
            Assert.AreEqual("Must be 6 characters or more", errorMsg.Text);

        }

        [Test]
        public void Try_to_register_with_a_5_symbol_password()
        {
            driver.Url = "https://staging.client.kuula.tv/";
            Thread.Sleep(1000);
            var buttonSignUp = driver.FindElement(By.XPath("//button[contains(.,'Sign Up')]"));
            buttonSignUp.Click();
            Thread.Sleep(3000);

            var nameField = driver.FindElement(By.Id("name"));
            Random rnd = new Random();
            int random = rnd.Next(0, 5000);
            var userName = "testUser" + random;
            nameField.SendKeys(userName);
            var emailField = driver.FindElement(By.Id("email"));
            emailField.SendKeys("testUser@test.com");
            var emailConfrimField = driver.FindElement(By.Id("emailConfirm"));
            emailConfrimField.SendKeys("testUser@test.com");
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("123456");
            var buttonRegister = driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Register my account')]"));
            buttonRegister.Click();
            Thread.Sleep(2000);

            var errorMsg = driver.FindElement(By.XPath("//div[contains(@id,'formError')]"));

            Assert.IsTrue(errorMsg.Displayed);
            Assert.AreEqual("A user with this Email address already exists", errorMsg.Text);

        }

        [Test]
        public void Try_to_logIn_Valid_User_Credentials()
        {
            driver.Url = "https://staging.client.kuula.tv/";
            Thread.Sleep(1000);
            var buttonlogIn = driver.FindElement(By.XPath("//button[contains(.,'Log In')]"));
            buttonlogIn.Click();
            Thread.Sleep(3000);


            var emailField = driver.FindElement(By.Id("email"));
            emailField.SendKeys("testUser@test.com");
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("123456");
            var buttonSubmit = driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Log In')]"));
            buttonSubmit.Click();
            Thread.Sleep(2000);

            var myProfileMenu = driver.FindElement(By.XPath("/html/body/div[1]/header/div/div[2]/button"));

            myProfileMenu.Click();
            var myProfileLink = driver.FindElement(By.XPath("//a[@href='/profile']"));
            myProfileLink.Click();
            Thread.Sleep(3000);

            var yourProfilePageTitle = driver.Title;
            var myAccountMenu = driver.FindElement(By.ClassName("active"));
            var paymentsMenu = driver.FindElement(By.XPath("//a[@href='/profile/payments'][contains(.,'Payments')]"));
            var activityMenu = driver.FindElement(By.XPath("//a[@href='/profile/activity'][contains(.,'Activity')]"));
            var bookingsMenu = driver.FindElement(By.XPath("//a[@href='/profile/bookings'][contains(.,'Bookings')]"));
            var billingMenu = driver.FindElement(By.XPath("//a[@href='/profile/subscriptions'][contains(.,'Billing')]"));
            var welcomeMsg = driver.FindElement(By.XPath("/html/body/div[1]/main/div/section[1]"));


            Assert.AreEqual("Your Profile - Kuula", yourProfilePageTitle);
            Assert.IsTrue(myAccountMenu.Displayed);
            Assert.IsTrue(paymentsMenu.Displayed);
            Assert.IsTrue(activityMenu.Displayed);
            Assert.IsTrue(bookingsMenu.Displayed);
            Assert.IsTrue(billingMenu.Displayed);
            Assert.IsTrue(welcomeMsg.Displayed);

            myProfileMenu.Click();
            var logOutButton = driver.FindElement(By.XPath("//div[@role='menuitem'][contains(.,'Sign Out')]"));
            logOutButton.Click();
            Thread.Sleep(3000);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}