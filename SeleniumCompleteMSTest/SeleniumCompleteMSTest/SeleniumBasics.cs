using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;

namespace SeleniumCompleteMSTest
{
    [TestClass]
    public class SeleniumBasics
    {
        [TestMethod]
        public void LaunchBrowser()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "Https://www.sqamanual.com";
            driver.Quit();

        }

        [TestMethod]
        public void getPageTitle()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://www.sqamanual.com";
            Console.WriteLine(driver.Title);
            driver.Url = "http://uitestpractice.com";
            Console.WriteLine(driver.Title);
            Console.WriteLine(driver.PageSource);
            driver.Quit();
        }
        [TestMethod]
        public void ManageBrowserWindow()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "Https://www.sqamanual.com";
            Thread.Sleep(2000);
            driver.Manage().Window.FullScreen();
            Thread.Sleep(2000);
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);
            driver.Manage().Window.Minimize();
            Thread.Sleep(2000);
            driver.Manage().Window.Position = new Point(400, 200);
            Thread.Sleep(2000);
            Point point = driver.Manage().Window.Position;
            Console.WriteLine(point);

            driver.Manage().Window.Size = new Size(400, 200);
            Thread.Sleep(2000);
            Size size = driver.Manage().Window.Size;
            Console.WriteLine(point);
            driver.Quit();

        }

        [TestMethod]
        public void BrowserNavigation()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "Https://www.sqamanual.com";
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://www.google.com");
            Thread.Sleep(2000);
            driver.Navigate().Back();
            Thread.Sleep(2000);
            driver.Navigate().Forward();
            driver.Navigate().Refresh();
            driver.Quit();
        }

        [TestMethod]
        public void LocateBy() //Chapter 7
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://ankpro.com/Account/Login";
            driver.FindElement(By.Id("Email")).SendKeys("a@b.com");
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            driver.FindElement(By.Name("Email")).SendKeys("base@c.com");
            Thread.Sleep(3000);
            driver.Navigate().GoToUrl("http://ankpro.com");
            String str = driver.FindElement(By.ClassName("jumbotron")).Text;
            Console.WriteLine(str);
            var paragraph = driver.FindElements(By.TagName("p"));
            Console.WriteLine(paragraph);
            Console.WriteLine("Count of p" + paragraph.Count);
            foreach( var item in paragraph)
            {
                Console.WriteLine(item.Text);
                Console.WriteLine(item);
            }

            //LinkText
            driver.FindElement(By.LinkText("Register")).Click();
            driver.FindElement(By.PartialLinkText("Log")).Click();
            Thread.Sleep(2000);
            driver.Quit();
        }

        [TestMethod]
        public void LocatebyCSS()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://ankpro.com/Account/Login";
            int count = driver.FindElements(By.CssSelector("*")).Count; //wild card *, select all elements
            Console.WriteLine("Number of elements found "+ count);
            count = driver.FindElements(By.CssSelector("#Email")).Count; //# select Id
            Console.WriteLine("Number of #Email found " + count);
            driver.FindElement(By.CssSelector("#Email")).SendKeys("a@a.com");
            count = driver.FindElements(By.CssSelector(".form-control")).Count;
            var formControl = driver.FindElements(By.CssSelector(".form-control"));
            Console.WriteLine("Number of form-control found " + count);
            foreach (var item in formControl)
            {
                Console.WriteLine(item.TagName);
                Console.WriteLine(item);
            }
            driver.FindElements(By.CssSelector(".form-control"))[1].SendKeys("1234"); //class
            driver.FindElement(By.CssSelector("input[name='Email']")).SendKeys("aadi"); //using tag name
            driver.FindElement(By.CssSelector("a[href^='/Home']")).Click(); // using ^ starts with
            driver.FindElement(By.CssSelector("a[href$='/Contact']")).Click(); // using $ ends with
            String str = driver.FindElement(By.CssSelector("footer p")).Text; // element element selector
            Console.WriteLine("footer :: " + str);
            driver.Navigate().GoToUrl("http://ankpro.com");
            str = driver.FindElement(By.CssSelector("h1+p")).Text; // element + element selector
            Console.WriteLine("footer :: " + str);
            driver.Url = "http://ankpro.com/Account/Login";
            str = driver.FindElement(By.CssSelector("form[role='form']")).Text;            // Element attribute CSS selector
            Console.WriteLine("form role :: " + str);
            driver.FindElement(By.Id("RememberMe")).Click();
            bool selectedCheckbox = driver.FindElement(By.CssSelector("input[type='checkbox']:checked")).Selected;            // Element checked CSS selector
            Console.WriteLine("checked :: " + selectedCheckbox);

            driver.FindElement(By.Id("RememberMe")).Click(); 
            selectedCheckbox = driver.FindElement(By.Id("RememberMe")).Selected;         
            Console.WriteLine("checked :: " + selectedCheckbox);
            Thread.Sleep(2000);
            driver.Quit();
        } //Chapter 8

        [TestMethod]
        public void LocateByXpath()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://ankpro.com/Account/Login";
            String str = driver.FindElement(By.XPath("/html/body/div[2]/footer/p")).Text; //absolute path
            Console.WriteLine(str);

            int count = driver.FindElements(By.XPath("//*")).Count; //get all elements
            Console.WriteLine("All elements count "+ count);
            count = driver.FindElements(By.XPath(".//input")).Count; //Tag name .//TagName
            Console.WriteLine("input count " + count);
            //Using attribute TagName[@attribute=‘Value’]
            //*[@id="Email"] || .//input[@id='Email']
           driver.FindElement(By.XPath("//*[@id='Email']")).SendKeys("a");

            //XPath using multiple attributes :
            //Syntax://tagname[@attribute1=’value1’] [@attribute2=’value2’]’ 
            //usage: driver.FindElement(By.XPath(“.//input[@id=‘Email’][@name=‘Email’]”))
            //This XPath will select an element whose tagname is input, attribute is id is Email and name attribute is also Email
            driver.FindElement(By.XPath(".//input[@id='Email'][@name='Email']")).SendKeys("ab");


            //XPath using and :
            //Syntax://tagname[@attribute1=’value1’and                 @attribute2=’value2’]’ 
            //usage: driver.FindElement(By.XPath(“.//input[@id=‘Email’ and @name=‘Email’]”))
            //This XPath will select an element whose tagname is input, attribute is id is Email and name attribute is Email
            driver.FindElement(By.XPath(".//input[@id='Email' and @name='Email']")).SendKeys("abcd");

            //XPath using or:
            //Syntax://tagname[@attribute1=’value1’ or               @attribute2=’value2’]’ 
            //usage: driver.FindElement(By.XPath(“.//input[@id=‘Email’ or @name=‘mailid’]”))
            //This XPath will select an element whose tagname is input, attribute id should be Email or name attribute should be mailid
            driver.FindElement(By.XPath(".//input[@id='Email' or @name='1Email']")).SendKeys("abcde");


            //XPath using index[] :
            //Syntax://tagname[number]
            //usage: driver.FindElement(By.XPath(“//tr[2]/td[2]”))
            driver.Navigate().GoToUrl("http://ankpro.com/Home/Training");
            Console.WriteLine(driver.FindElement(By.XPath("//tr[2]/td[2]")).Text);

            Thread.Sleep(2000);
            driver.Quit();
        } //Chapter 9

        [TestMethod]
        public void XpathFunctions() //Chapter 10
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://ankpro.com";
            //XPath using text() function :
            //Syntax:.//*[text()=’VisibleText’]
            //usage : driver.FindElement(By.XPath(“.//*[text()='Our People'] ”))
            //This XPath will select an element which contains the text “Our People”.
            String str = driver.FindElement(By.XPath("//h2[text()='Our People']")).Text;
            Console.WriteLine(str);

            //XPath using starts-with() function:
            //Syntax://tagname[starts-with(@attribute-name,’value’)]
            //usage : driver.FindElement(By.XPath(“.//input[starts-with(@id, 'Rem')]”))
            //This XPath will select an element whose tagname is input, attribute is id  and value starts with Rem
            driver.Navigate().GoToUrl("http://ankpro.com/Account/Login");
            driver.FindElement(By.XPath(".//input[starts-with(@id,'Rem')]")).Click();
            
            //XPath using contains() function : 
            //Syntax://tagname[contains(@attribute,’value’)]
            //usage : driver.FindElement(By.XPath(“.//input[contains(@id,’Me’)]”))
            //This XPath will select an element whose tagname is input, attribute is id  and value that contains Me
            driver.FindElement(By.XPath(".//input[contains(@id,'Rem')]")).Click();
           
            //XPath using not() function :
            //Usage : driver.FindElement(By.XPath(“.//input[@type='checkbox' and  not(@checked)]”)) 
            //This XPath will select checkboxes which do not have the attribute checked.
            driver.FindElement(By.XPath(".//input[@type='checkbox' and not(@checked)]")).Click();

            //XPath using last() function:
            //Syntax: (//tagname[last()])
            //usage : driver.FindElement(By.XPath(“.//tbody/tr[last()]”))
            //This XPath will select last element in the table.
            driver.Navigate().GoToUrl("http://ankpro.com/Home/Training");
            str = driver.FindElement(By.XPath("//tbody/tr[last()]")).Text;
            Console.WriteLine(str);

            //XPath using last()-1 function :
            //Syntax: (//tagname([last()-1])
            //Usage : driver.FindElement(By.xpath(“.//tbody/tr[last()-1]”))
            //This XPath will select last but one element in the table.
            str = driver.FindElement(By.XPath("//tbody/tr[last()-1]")).Text;
            Console.WriteLine(str);

            //XPath using position() function:
            //Syntax: XPath("(//tagname[position()=2]")
            //Usage : driver.FindElement(By.XPath("(.//input[@type='text'])[position()=2]"))
            driver.Navigate().GoToUrl("http://ankpro.com/Account/Register");
            driver.FindElement(By.XPath("(.//input[@type='password'])[position()=2]")).SendKeys("a");
            Thread.Sleep(2000);
            driver.Quit();
        }
        [TestMethod]
        public void XpathAxis() //Chapter 11
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://ankpro.com";
            //XPath using parent filter:
            //Parent filter selects the parent element of the current element
            //Syntax:.//tagname/parent::tagname
            //usage : driver.FindElement(By.XPath(“//h1/parent::div”))
            //This XPath will select parent div element of h1 tag
            String str = driver.FindElement(By.XPath("//h1/parent::div")).Text;
            Console.WriteLine(str);
            //XPath using child filter:
            //Child filter selects the child element of the current element
            //Syntax:.//tagname/child::tagname
            //Usage : driver.FindElement(By.Xpath(“//footer/child::p”))
            //This XPath will select child p element of footer tag
            str = driver.FindElement(By.XPath("//footer/child::p")).Text;
            Console.WriteLine(str);
            //XPath using following-sibling :
            //Following-sibling filter selects all siblings elements after the current element
            //Syntax:.//tagname/following-sibling::tagname
            //Usage : driver.FindElement(By.Xpath(“//tr[6]/following-sibling::tr”))
            //This XPath will select all tr elements of same parent after sixth tr tag 
            driver.Navigate().GoToUrl("http://ankpro.com/Home/Training");
            str = driver.FindElement(By.XPath("//tr[6]/following-sibling::tr")).Text;
            int count = driver.FindElements(By.XPath("//tr[6]/following-sibling::tr")).Count;
            Console.WriteLine(str + " : : " + count);
            //XPath using following:
            //Following filter selects all elements after the current element
            //Syntax:.//tagname/following::tagname
            //Usage : driver.FindElement(By.Xpath(“//tr[6]/following::*”))
            //This XPath will select all elements after sixth tr tag 
            str = driver.FindElement(By.XPath("//tr[6]/following::tr")).Text;
            count = driver.FindElements(By.XPath("//tr[6]/following::*")).Count;
            Console.WriteLine(str + " : : " + count);
            //XPath using preceding-sibling:
            //Preceding-sibling filter selects all siblings elements before the current element
            //Syntax:.//tagname/preceding-sibling::tagname
            //Usage : driver.FindElement(By.XPath(“//tr[6]/preceding-sibling::tr”))
            //This XPath will select all tr elements of same parent before sixth tr tag 
            str = driver.FindElement(By.XPath("//tr[6]/preceding-sibling::tr")).Text;
            count = driver.FindElements(By.XPath("//tr[6]/preceding-sibling::*")).Count;
            Console.WriteLine(str + " : : " + count);
            //XPath using preceding :
            //Preceding filter selects all elements before the current element
            //Syntax:.//tagname/preceding::tagname
            //Usage : driver.FindElement(By.Xpath(“//tr[6]/ preceding ::*”))
            //This XPath will select all elements before sixth tr tag 
            str = driver.FindElement(By.XPath("//tr[6]/preceding::tr")).Text;
            count = driver.FindElements(By.XPath("//tr[6]/preceding::*")).Count;
            Console.WriteLine(str + " : : " + count);
            //XPath using ancestor filter:
            //Ancestor filter selects all ancestors (parent, grand parent, great grand parent etc.) of the current element
            //usage : driver.FindElement(By.XPath(“//table/ancestor::div”))
            //This XPath will select all parent div tags of table tag.
            str = driver.FindElement(By.XPath("//table/ancestor::div")).Text;
            count = driver.FindElements(By.XPath("//table/ancestor::div")).Count;
            Console.WriteLine(str + " : : " + count);
            //XPath using descendant filter :
            //Descendant filter selects all descendants (children, grand children, great grand children etc.) of the current element
            //usage : driver.FindElement(By.XPath(“//table/descendant::td”))
            str = driver.FindElement(By.XPath("//table/descendant::td")).Text;
            count = driver.FindElements(By.XPath("//table/descendant::td")).Count;
            Console.WriteLine(str + " : : " + count);
            Thread.Sleep(2000);
            driver.Quit();
        }

        [TestMethod]
        public void DifferenceBetweenFindElementAndElements() //Chapter 16
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://ankpro.com/Home/Training";
           // IWebElement element = driver.FindElement(By.Id("sample"));
          //  Console.WriteLine(element);

            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.Id("sample"));
            Console.WriteLine(elements.Count);
            IWebElement element = driver.FindElement(By.TagName("h2"));
           Console.WriteLine(element.Text);

            elements = driver.FindElements(By.TagName("h2"));
            Console.WriteLine(elements.Count);
            element = driver.FindElement(By.TagName("tr"));
            Console.WriteLine(element.Text);
            elements = driver.FindElements(By.TagName("tr"));
            Console.WriteLine(elements.Count);
            foreach (var item in elements)
            {
                Console.WriteLine(item.Text);
            }
            Thread.Sleep(2000);
            driver.Quit();

        }

        [TestMethod]
        public void CountCheckUnchecked() //Chapter 17

        {
            IWebDriver driver = new ChromeDriver();
            //Usage:driver.FindElement(By.XPath("//input[@type='checkbox']")).Selected;
            driver.Url = "http://uitestpractice.com/Students/Form";
            driver.FindElement(By.XPath("//input[@value='dance']")).Click();
            driver.FindElement(By.XPath("//input[@value='cricket']")).Click();
            ReadOnlyCollection <IWebElement> webElements= driver.FindElements(By.XPath("//input[@type='checkbox']"));
            int checkedCount= 0;
            int uncheckedCount= 0;
            foreach(var item in webElements)
            {
                if (item.Selected == true)
                    checkedCount++;
                else
                    uncheckedCount++;
            }
            Console.WriteLine("Checked " + checkedCount);
            Console.WriteLine("UnChecked " + uncheckedCount);
            Thread.Sleep(2000);
            driver.Quit();
        }

        [TestMethod]
        public void NewLineSendKeys() //Chapter 18

        {
            IWebDriver driver = new ChromeDriver();
            
            driver.Url = "http://uitestpractice.com/Students/Form";
            driver.FindElement(By.Id("comment")).SendKeys("Good \n hello \t morning");

            Thread.Sleep(2000);
            driver.Quit();
        }

        [TestMethod]
        public void SelectDropDown() //Chapter 20
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://uitestpractice.com/Students/Select";
            IWebElement element= driver.FindElement(By.Id("countriesSingle"));
            SelectElement selectElement = new SelectElement(element);
            IList<IWebElement> elements= selectElement.Options;
            Console.WriteLine("Single dropdown "+ selectElement.IsMultiple);
            selectElement.SelectByText("India");
            foreach( var item in elements)
            {
                Console.WriteLine(item.Text);

            }
            element = driver.FindElement(By.Id("countriesMultiple"));
            selectElement = new SelectElement(element);
            Console.WriteLine("Multi select :"+ selectElement.IsMultiple);
            selectElement.SelectByIndex(2);
            Thread.Sleep(2000);
            driver.Quit();
        }

        [TestMethod]
        public void SelectDropDownBootStrap() //Chapter 21
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://uitestpractice.com/Students/Select";
            driver.FindElement(By.Id("dropdownMenu1")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a[text()='India']")).Click();
            Thread.Sleep(2000);
            String str = driver.FindElement(By.Id("dropdownMenu1")).Text;
            Thread.Sleep(2000);
            Console.WriteLine(str);
            Thread.Sleep(2000);
            driver.Quit();
        }

        [TestMethod]
        public void ActionsMoveToElement() //Chapter 23
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "http://uitestpractice.com/Students/Actions";
            
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.Id("div2"))).Build().Perform();  // build and perform is must in actions

            action.MoveToElement(driver.FindElement(By.Id("div2")),20,20).ContextClick().Build().Perform();

            action.MoveToElement(driver.FindElement(By.Id("div2")), 20, 20,MoveToElementOffsetOrigin.Center).ContextClick().Build().Perform();
            Thread.Sleep(2000);
            driver.Quit();
        }

        [TestMethod]
        public void ActionsClick() //Chapter 24
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "http://uitestpractice.com/Students/Actions";
            //driver.FindElement(By.Name("click")).Click();
           Actions action = new Actions(driver);
            /* action.MoveToElement(driver.FindElement(By.Name("click")))
                  .Click()
                  .Build()
                  .Perform();*/

            //Another Way
            action.Click(driver.FindElement(By.Name("click")))
               .Build()
               .Perform();

            Thread.Sleep(2000);
            driver.Quit();
        }
    }
}
