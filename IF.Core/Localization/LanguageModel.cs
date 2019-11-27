using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Persistence.EF.Localization
{
    public class LanguageGridModel
    {
        public LanguageGridModel()
        {
            this.Rows = new List<LanguageGridRowModel>();
            this.Columns = new List<string>();
        }
        public List<LanguageGridRowModel> Rows { get; set; }

        public List<string> Columns { get; set; }
    }

    public class LanguageGridRowModel
    {
        public LanguageGridRowModel()
        {
            this.Data = new List<string>();
        }
        public List<string> Data { get; set; }

    }

    public class LanguageFormModel
    {
        public LanguageFormModel()
        {
            this.Languages = new List<LanguageViewModel>();
        }

        public object Id { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

    }

    public class LanguageViewModel
    {
        public LanguageViewModel()
        {
            this.Items = new List<LanguageItemModel>();
        }

        public int Id { get; set; }
        public int Index { get; set; }
        public int LanguageId { get; set; }
        public List<LanguageItemModel> Items { get; set; }
    }

    public class LanguageItemModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
