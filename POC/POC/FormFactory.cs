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
                var column = new ColumnBase { Name = item.Name };

                columnResult.Add(column);
            }

            formResult.Contents = columnResult.AsEnumerable();

            return formResult;
        }
    }
}