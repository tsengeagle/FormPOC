using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace POC
{
    [Binding]
    public class 產生空白表單Steps
    {
        [Given(@"表單定義")]
        public void Given表單定義(Table formDefineTable)
        {
            var formDefine = formDefineTable.CreateSet<FormDefine>();
            ScenarioContext.Current.Set(formDefine, "formDefine");
        }

        [Given(@"欄位定義")]
        public void Given欄位定義(Table colDefineTable)
        {
            var colDefine = colDefineTable.CreateSet<ColumnDefine>();
            ScenarioContext.Current.Set(colDefine, "colDefine");
        }

        [When(@"取得表單組件：""(.*)""")]
        public void When取得表單組件(string formName)
        {
            var formDefine = ScenarioContext.Current.Get<IEnumerable<FormDefine>>("formDefine").First(w => w.Name == formName);
            var colDefine = ScenarioContext.Current.Get<IEnumerable<ColumnDefine>>("colDefine").Where(w => w.FormName == formName);
            formDefine.Columns = colDefine;

            var dao = new Mock<IFormDao>();
            dao.Setup(s => s.GetFormDefine(formName)).Returns(formDefine);

            var target = new FormFactory(dao.Object);
            var resultForm = target.GetForm(formName);

            ScenarioContext.Current.Set(resultForm, "resultForm");
        }

        [Then(@"得到表單組件：""(.*)""，欄位：")]
        public void Then得到表單組件欄位(string expectedFormName, Table table)
        {
            var actual = ScenarioContext.Current.Get<FormBase>("resultForm");
            Assert.AreEqual(expectedFormName, actual.Name);
            table.CompareToSet(actual.Contents);
        }
    }

    public class FormFactory
    {
        private IFormDao _formDao;

        public FormFactory(IFormDao formDAO)
        {
            _formDao = formDAO;
        }

        public FormBase GetForm(string formName)
        {
            var formDefine = _formDao.GetFormDefine(formName);
            var resultForm = new FormBase();

            resultForm.Name = formDefine.Name;
            var tempColumns = new List<ColumnBase>();
            foreach (var formDefineColumn in formDefine.Columns)
            {
                tempColumns.Add(new ColumnBase() { Name = formDefineColumn.Name, Value = formDefineColumn.DefaultValue });
            }
            resultForm.Contents = tempColumns.AsEnumerable();

            return resultForm;
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
        public string Value { get; set; }
    }

    public interface IFormDao
    {
        FormDefine GetFormDefine(string formName);
    }

    public class ColumnDefine
    {
        public string FormName { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
    }

    public class FormDefine
    {
        public string Name { get; set; }
        public IEnumerable<ColumnDefine> Columns { get; set; }
    }
}
