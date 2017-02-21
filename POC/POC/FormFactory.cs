using System.Collections.Generic;
using System.Linq;

namespace POC
{
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
                var column = new ColumnBase();
                column = GetColumn(item);

                columnResult.Add(column);
            }

            formResult.Contents = columnResult.AsEnumerable();

            return formResult;
        }

        private ColumnBase GetColumn(ColumnDefine item)
        {
            ColumnBase columnResult = null;

            switch (item.ColumnType)
            {
                case "StringColumn":
                    columnResult = new StringColumn() { Name = item.Name, Content = item.DefaultContent };
                    break;
                case "CheckColumn":
                    columnResult = new CheckColumn(item);
                    break;
                case "":
                    break;
            }

            return columnResult;
        }
    }
}