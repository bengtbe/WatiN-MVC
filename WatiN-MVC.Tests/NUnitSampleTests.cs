using NUnit.Framework;
using WatiN.Core;
using System.Web.Mvc;
using WatiN_MVC.SampleSite.Models;
using System.Linq.Expressions;
using System;

namespace WatiN_MVC.Tests {

    public class NUnitSampleTests {

        string url = "http://localhost/WatiN-MVC/Profile/Edit/1";

        [Test]
        public void Should_be_able_to_type_values_into_textboxes()
        {
            using (var browser = new WatinMvc<Profile>(url))
            {
                browser.TypeInTextBox(model => model.Name, "Model_Name");
                browser.TypeInTextBox(model => model.Email, "Model_Email");
                browser.TypeInTextBox(model => model.Age, "30");
                browser.TypeInTextBox(model => model.Company.Name, "Model_Company_Name");
                browser.AssertContainsText("Namesadfasdf");
            }

        }

        [Test]
        public void Should_be_detect_validation_errors()
        {
            using (var browser = new WatinMvc<Profile>(url))
            {
                browser.TypeInTextBox(model => model.Age, "NOT A AGE");
                browser.HasValidationError(model => model.Email);
            }

        }

    }


    public class WatinMvc<TModel> : IDisposable where TModel : class
    {
        public IE Watin { get; set; }

        public WatinMvc(string url)
        {
            Watin = new IE(url);
        }


        public void TypeInTextBox<TProperty>(Expression<Func<TModel, TProperty>> expression, string value)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            Watin.TextField(Find.ByName(name)).TypeText(value);
        }

        public void HasValidationError<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            string name = ExpressionHelper.GetExpressionText(expression);

            if(!Watin.Element(Find.ByName(name)).ClassName.Contains("input-validation-error"))
                throw new AssertionException("BANG!");
        }

        public void AssertContainsText(string value)
        {
            if (!Watin.ContainsText("value"))
                throw new AssertionException("BANG!");
        }
      

        public void Dispose()
        {
            Watin.Dispose();
        }
    }
}
