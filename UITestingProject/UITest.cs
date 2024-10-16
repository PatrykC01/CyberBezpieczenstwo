using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Xunit;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace UITestingProject
{
    public class UITest : IDisposable
    {
        private readonly IWebDriver driver;
        public UITest()
        {
            var driverPath = @"C:\Users\20pat\OneDrive\Pulpit\Szkoła\PdIwtA\chromedriver-win64";
            driver = new ChromeDriver(driverPath);
        }

        [Fact]
        public void Create_GET_ReturnsCreateView()
        {
            driver.Navigate().GoToUrl("https://localhost:7029/Trips/Create");

            IWebElement emailField = driver.FindElement(By.Name("Input.Email"));
            emailField.SendKeys("adminTravelAgency@gmail.com");

            IWebElement passwordField = driver.FindElement(By.Name("Input.Password"));
            passwordField.SendKeys("#ScisleT4jne");

            IWebElement loginButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            loginButton.Click();

            System.Threading.Thread.Sleep(5000);

            Assert.Equal("Create - Travel Agency", driver.Title);
            Assert.Contains("Create", driver.PageSource);
            Assert.Contains("Trip", driver.PageSource);
        }

        [Fact]
        public void TextSearching_in_TripsView()
        {

            driver.Navigate().GoToUrl("https://localhost:7029/Trips");

            IWebElement passwordField = driver.FindElement(By.Name("SearchString"));
            passwordField.SendKeys("jez");

            IWebElement loginButton = driver.FindElement(By.ClassName("searchBtn"));
            loginButton.Click();

            System.Threading.Thread.Sleep(5000);

            Assert.Equal("Index - Travel Agency", driver.Title);
            Assert.Contains("jez", driver.PageSource);
        }

        [Fact]
        public void HomePageView()
        {

            driver.Navigate().GoToUrl("https://localhost:7029");

            Assert.Equal("Home Page - Travel Agency", driver.Title);

            IWebElement tripField = driver.FindElement(By.ClassName("tripInfo"));

            tripField.Click();

            System.Threading.Thread.Sleep(5000);

            Assert.Contains("Delfiny i snorkling przy wyspie Mnemba, żółwie wodne i zachód słońca", driver.PageSource);

            Assert.Equal("https://localhost:7029/Home/Detail/1", driver.Url);
        }

        [Fact]
        public void CustomerDetailAndIndexView()
        {

            driver.Navigate().GoToUrl("https://localhost:7029/Customers");

            IWebElement emailField = driver.FindElement(By.Name("Input.Email"));
            emailField.SendKeys("adminTravelAgency@gmail.com");

            IWebElement passwordField = driver.FindElement(By.Name("Input.Password"));
            passwordField.SendKeys("#ScisleT4jne");

            IWebElement loginButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            loginButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            IWebElement firstName1 = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[1]"));
            var fn1= firstName1.Text;
            IWebElement lastName1 = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[2]"));
            var ln1 = lastName1.Text;
            IWebElement phoneNumber1 = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[3]"));
            var pn1 = phoneNumber1.Text;
            IWebElement detailsBtn = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[4]/a[2]"));

            detailsBtn.Click();

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement firstName2 = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[1]/dl/dd[1]"));
            var fn2 = firstName2.Text;
            IWebElement lastName2 = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[1]/dl/dd[2]"));
            var ln2 = lastName2.Text;
            IWebElement phoneNumber2 = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[1]/dl/dd[3]"));
            var pn2 = phoneNumber2.Text;

            Assert.Equal(fn1, fn2);
            Assert.Equal(ln1, ln2);
            Assert.Equal(pn1, pn2);

        }

        [Fact]
        public void ReservationCreateView()
        {
            driver.Navigate().GoToUrl("https://localhost:7029/Reservations/create");

            IWebElement emailField = driver.FindElement(By.Name("Input.Email"));
            emailField.SendKeys("adminTravelAgency@gmail.com");

            IWebElement passwordField = driver.FindElement(By.Name("Input.Password"));
            passwordField.SendKeys("#ScisleT4jne");

            IWebElement loginButton = driver.FindElement(By.XPath("//button[@type='submit']")); 
            loginButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            IWebElement TripID = driver.FindElement(By.Name("TripID"));
            TripID.Click();
            IWebElement option1 = driver.FindElement(By.XPath("//*[@id=\"TripID\"]/option[1]"));
            option1.Click();
            
            IWebElement CustomerID = driver.FindElement(By.Name("CustomerID"));
            IWebElement option1_2 = driver.FindElement(By.XPath("//*[@id=\"CustomerID\"]/option[1]"));
            option1_2.Click();

            IWebElement ReservationDate = driver.FindElement(By.Name("ReservationDate"));
            string dateTimeValue = "2024-06-15T14:30";
            ReservationDate.SendKeys(dateTimeValue);
            
            IWebElement DateOfDeparture = driver.FindElement(By.Name("ReservationDate"));
            string dateTimeValue2 = "2024-06-20T14:30";
            DateOfDeparture.SendKeys(dateTimeValue2);
            
            IWebElement DateOfReturn = driver.FindElement(By.Name("ReservationDate"));
            string dateTimeValue3 = "2024-06-27T14:30";
            DateOfReturn.SendKeys(dateTimeValue3);

            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[1]/div/form/button/span[2]"));


            submitButton.Click();
        }

        [Fact]
        public void ReservationEditAndIndexView()
        {

            driver.Navigate().GoToUrl("https://localhost:7029/Reservations");

            IWebElement emailField = driver.FindElement(By.Name("Input.Email"));
            emailField.SendKeys("adminTravelAgency@gmail.com");

            IWebElement passwordField = driver.FindElement(By.Name("Input.Password"));
            passwordField.SendKeys("#ScisleT4jne");

            IWebElement loginButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            loginButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            IWebElement reservationDate1 = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[1]"));
            var rd1 = reservationDate1.Text;
            string formattedReservationDate1 = FormatDateTimeFromIndex(rd1);

            IWebElement dateOfDeparture1 = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[2]"));
            var dod1 = dateOfDeparture1.Text;
            string formattedDateOfDeparture1 = FormatDateTimeFromIndex(dod1);

            IWebElement dateOfReturn1 = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[3]"));
            var dor1 = dateOfReturn1.Text;
            string formattedDateOfReturn1 = FormatDateTimeFromIndex(dor1);

            IWebElement editBtn = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[6]/a[1]"));
            editBtn.Click();

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement reservationDateElement2 = driver.FindElement(By.Id("ReservationDate"));
            string rd2 = reservationDateElement2.GetAttribute("value");
            string formattedReservationDate2 = FormatDateTimeFromEdit(rd2);

            IWebElement dateOfDepartureElement2 = driver.FindElement(By.Id("DateOfDeparture"));
            string dod2 = dateOfDepartureElement2.GetAttribute("value");
            string formattedDateOfDeparture2 = FormatDateTimeFromEdit(dod2);

            IWebElement dateOfReturnElement2 = driver.FindElement(By.Id("DateOfReturn"));
            string dor2 = dateOfReturnElement2.GetAttribute("value");
            string formattedDateOfReturn2 = FormatDateTimeFromEdit(dor2);

            Assert.Equal(formattedReservationDate1, formattedReservationDate2);
            Assert.Equal(formattedDateOfDeparture1, formattedDateOfDeparture2);
            Assert.Equal(formattedDateOfReturn1, formattedDateOfReturn2);

        }


        private string FormatDateTimeFromIndex(string dateTimeString)
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(dateTimeString, "dd.MM.yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            }
            else
            {
                throw new FormatException("Invalid date format: " + dateTimeString);
            }
        }

        private string FormatDateTimeFromEdit(string dateTimeString)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeString, out dateTime))
            {
                return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            }
            else
            {
                throw new FormatException("Invalid date format: " + dateTimeString);
            }
        }

        [Fact]
        public void TripsDeleteView()
        {

            driver.Navigate().GoToUrl("https://localhost:7029/Identity/Account/Login");

            IWebElement emailField = driver.FindElement(By.Name("Input.Email"));
            emailField.SendKeys("adminTravelAgency@gmail.com");

            IWebElement passwordField = driver.FindElement(By.Name("Input.Password"));
            passwordField.SendKeys("#ScisleT4jne");

            IWebElement loginButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            loginButton.Click();


            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));

            driver.Navigate().GoToUrl("https://localhost:7029/Trips");

            IWebElement title1 = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[1]"));
            var t1 = title1.Text;

            IWebElement deleteBtn = driver.FindElement(By.XPath("//*[@id=\"dataTable\"]/tbody/tr[1]/td[6]/a[3]"));

            deleteBtn.Click();


            Assert.Contains("Are you sure you want to delete this?", driver.PageSource);

            IWebElement title2 = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[1]/dl/dd[1]"));
            var t2 = title2.Text;

            Assert.Equal(t1, t2);
        }

        [Fact]
        public void RegisterView()
        {

            driver.Navigate().GoToUrl("https://localhost:7029/Identity/Account/Register");

            IWebElement emailField = driver.FindElement(By.Name("Input.Email"));
            emailField.SendKeys("abc");

            IWebElement passwordField = driver.FindElement(By.Name("Input.Password"));
            passwordField.SendKeys("Qwerty123.");
            
            IWebElement passwordConfirmField = driver.FindElement(By.Name("Input.ConfirmPassword"));
            passwordConfirmField.SendKeys("Qwerty123,");

            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            Assert.Contains("The Email field is not a valid e-mail address.", driver.PageSource);
            Assert.Contains("The password and confirmation password do not match.", driver.PageSource);

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            emailField.Clear();
            passwordField.Clear();
            passwordConfirmField.Clear();

            emailField.SendKeys("abc3@gmail.com");

            passwordField.SendKeys("Qwerty123.");

            passwordConfirmField.SendKeys("Qwerty123.");

            IWebElement registerButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            registerButton.Click();

            WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            Assert.Contains("Hello abc3@gmail.com!", driver.PageSource);
            Assert.Contains("Home Page - Travel Agency", driver.Title);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
