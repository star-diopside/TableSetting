using System.Windows.Forms;

namespace TableSetting.Forms
{
    public partial class EditConnectionStringDialog : Form
    {
        public EditConnectionStringDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 設定を行うキーを取得または設定します。
        /// </summary>
        public string Key
        {
            get => textKey.Text;
            set => textKey.Text = value;
        }

        /// <summary>
        /// 設定を行う値を取得または設定します。
        /// </summary>
        public string Value
        {
            get => textValue.Text;
            set => textValue.Text = value;
        }

        /// <summary>
        /// 設定したキーと値を有効にするかどうかを示す値を取得または設定します。
        /// </summary>
        public bool EnableItem
        {
            get => checkEnableItem.Checked;
            set => checkEnableItem.Checked = value;
        }
    }
}
