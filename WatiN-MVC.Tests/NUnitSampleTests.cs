using NUnit.Framework;
using WatiN.Core;
using System.Web.Mvc;
using WatiN_MVC.SampleSite.Models;
using System.Linq.Expressions;
using System;

namespace WatiN_MVC.Tests {

    public class WatinMvcBase<TModel> where TModel : class
    {
        protected void TypeInTextBox<TProperty>(IE browser, Expression<Func<TModel, TProperty>> expression, string value)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            browser.TextField(Find.ByName(name)).TypeText(value);
        }

        protected string TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression) 
        {
            return ExpressionHelper.GetExpressionText(expression);
        }

    }


    public class NUnitSampleTests : WatinMvcBase<Profile> {

        string url = "http://localhost/WatiN-MVC/Profile/Edit/1";

        [Test]
        public void SomePassingTest() {
            Assert.AreEqual(5, 5);
        }

        [Test]
        public void SomeFailingTest() {
            Assert.Greater(5, 7);
        }

        [Test]
        public void SearchForWatiNOnGoogle()
        {
            using (var browser = new IE(url))
            {
                TypeInTextBox(browser, model => model.Email, "Dette var jo morro");

                Assert.IsTrue(browser.ContainsText("Email"));
            }

        }




    }
}
