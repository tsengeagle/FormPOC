using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Moq;
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

        [Given(@"表單定義")]
        public void Given表單定義(Table givenFormDefine)
        {
            ScenarioContext.Current.Set(givenFormDefine.CreateSet<FormDefine>(), "formDefine");
        }

        [Given(@"欄位定義")]
        public void Given欄位定義(Table givenColumnDefine)
        {
            ScenarioContext.Current.Set(givenColumnDefine.CreateSet<ColumnDefine>(), "columnDefine");
        }

        [When(@"表單名稱：""(.*)""，按下新增表單")]
        public void When表單名稱按下新增表單(string formName)
        {
            FormDefine formDefine = null;
            if (ScenarioContext.Current.ContainsKey("formDefine"))
            {
                formDefine = ScenarioContext.Current.Get<IEnumerable<FormDefine>>("formDefine").First(f => f.Name == formName);
                if (ScenarioContext.Current.ContainsKey("columnDefine"))
                {
                    if (formDefine != null)
                        formDefine.ColumnDefine = ScenarioContext.Current.Get<IEnumerable<ColumnDefine>>("columnDefine").Where(w => w.FormName == formName);
                }
            }

            var daoMock = new Mock<IFormDAO>();

            daoMock.Setup<FormDefine>(s => s.GetDefine(formName)).Returns(formDefine);

            var target = new FormFactory(daoMock.Object);
            var result = target.GetPeriopForm(formName);
            ScenarioContext.Current.Set(result, "result");
        }

        [Then(@"得到表單內容")]
        public void Then得到表單內容(Table table)
        {
            var actualForm = ScenarioContext.Current.Get<FormBase>("result");
            table.CompareToInstance(actualForm);
        }

        [Then(@"得到表單欄位")]
        public void Then得到表單欄位明細(Table table)
        {
            var actualContents = ScenarioContext.Current.Get<FormBase>("result").Contents;
            table.CompareToSet(actualContents);
        }

        [Then(@"表單欄位型別")]
        public void Then表單欄位型別(Table table)
        {
            ScenarioContext.Current.Pending();
            
            var actualContents = ScenarioContext.Current.Get<FormBase>("result").Contents;
            var expectedTypes = table.CreateSet<string>();
            foreach (var typeName in expectedTypes)
            {

            }
        }

    }
}
