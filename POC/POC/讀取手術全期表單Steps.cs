using System;
using ExpectedObjects;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace POC
{
    [Binding]
    public class 讀取手術全期表單Steps
    {
        [When(@"按下新增表單")]
        public void When按下新增表單()
        {
            var target = new FormFactory();
            var result = target.GetPeriopForm();
            ScenarioContext.Current.Set(result, "result");
        }

        [Then(@"得到空白表單")]
        public void Then得到空白表單()
        {
            var expected = new FormBase();
            var actual = ScenarioContext.Current.Get<FormBase>("result");
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [Then(@"得到空白表單，名稱 (.*)")]
        public void Then得到空白表單名稱(string expectedFormName)
        {
            var expectedForm=new FormBase();
            expectedForm.Name = expectedFormName;

            var actualForm=ScenarioContext.Current.Get<FormBase>("result");

            expectedForm.ToExpectedObject().ShouldEqual(actualForm);
            Assert.AreEqual(expectedForm.Name,actualForm.Name);
        }

        [When(@"表單名稱 (.*)，按下新增表單")]
        public void When表單名稱按下新增表單(string formName)
        {
            var target = new FormFactory();
            var result = target.GetPeriopForm(formName);
            ScenarioContext.Current.Set(result, "result");
        }

    }

    public class FormFactory
    {
        public FormBase GetPeriopForm()
        {
            return new FormBase();
        }

        internal object GetPeriopForm(string formName)
        {
            return new FormBase() {Name = formName};
        }
    }

    public class FormBase
    {
        public string Name { get; set; }
    }
}
