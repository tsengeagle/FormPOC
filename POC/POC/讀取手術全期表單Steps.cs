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

    public interface IFormDAO
    {
        FormDefine GetDefine(string formName);
    }

    public class ColumnDefine
    {
        public string Name { get; set; }
        public string FormName { get; set; }
    }

    public class FormDefine
    {
        public string Name { get; set; }

        public IEnumerable<ColumnDefine> ColumnDefine { get; internal set; }
    }

    public class FormFactory
    {
        private readonly IFormDAO _formDAO;

        public FormFactory()
        {
        }

        public FormFactory(IFormDAO formDao)
        {
            _formDAO = formDao;
        }

        public FormBase GetPeriopForm()
        {
            return new FormBase();
        }

        public FormBase GetPeriopForm(string formName)
        {
            var formDefine = _formDAO.GetDefine(formName);

            var formResult = new FormBase { Name = formDefine.Name };

            var columnResult = new List<ColumnBase>();
            foreach (var item in formDefine.ColumnDefine)
            {
                var column = new ColumnBase { Name = item.Name };

                columnResult.Add(column);
            }

            formResult.Contents = columnResult.AsEnumerable();

            return formResult;
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
