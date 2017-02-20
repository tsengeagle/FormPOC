using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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

        [When(@"表單名稱：""(.*)""，按下新增表單")]
        public void When表單名稱按下新增表單(string formName)
        {
            var target = new FormFactory();
            var result = target.GetPeriopForm(formName);
            ScenarioContext.Current.Set(result, "result");
        }

        [Then(@"得到空白表單，名稱：""(.*)""，欄位明細")]
        public void Then得到空白表單名稱欄位明細(string expectedFormName, Table expectedContents)
        {
            var expectedForm = new FormBase();
            expectedForm.Name = expectedFormName;
            expectedForm.Contents = expectedContents.CreateSet<ColumnBase>();

            var actualForm = ScenarioContext.Current.Get<FormBase>("result");

            expectedForm.ToExpectedObject().ShouldEqual(actualForm);
        }

    }

    public class FormFactory
    {
        public FormBase GetPeriopForm()
        {
            return new FormBase();
        }

        public FormBase GetPeriopForm(string formName)
        {
            return new FormBase() { Name = formName, Contents = new List<ColumnBase>() { new ColumnBase() { Name = "" } }.AsEnumerable() };
        }
    }

    public class FormBase
    {
        public string Name { get; set; }
        public IEnumerable<ColumnBase> Contents { get; set; }
    }

    public class ColumnBase
    {
        public string Name { get; set; }
    }
}
